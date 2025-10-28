using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "2badf022-fd7d-4483-b948-5d90c3f351a3",
    Title = "Cast Variant List",
    Category = "JSON",
    Width = 1f
)]
public class CastVariantList : Node
{
    [DataInput]
    public object List = null;
    public enum BuiltInListTypes
    {
        [Label("Auto")]
        Auto,
        [Label("Integer List")]
        IntegerList,
        [Label("Float List")]
        FloatList,
        [Label("Boolean List")]
        BooleanList,
        [Label("String List")]
        StringList
    }

    [DataInput]
    public BuiltInListTypes TargetType = BuiltInListTypes.Auto;

    public object _output = null;
    [DataOutput]
    public object Output() => _output;

    protected override void OnCreate()
    {
        base.OnCreate();
        Watch<object>(nameof(List), (_, __) => OnChange());
        Watch<BuiltInListTypes>(nameof(TargetType), (_, __) => OnChange());
    }

    public void OnChange()
    {
        if (List == null) { _output = null; return; }
        var arr = List as JArray;
        if (arr == null) { _output = null; return; }
        BuiltInListTypes type;
        if (TargetType == BuiltInListTypes.Auto)
        {
            if (arr.Count == 0 || arr[0].GetType() != typeof(JValue) || (arr[0] as JValue).Value == null) { _output = null; return; }
            switch ((arr[0] as JValue).Value.GetType().Name)
            {
                case nameof(Int32):
                case nameof(Int64): 
                    type = BuiltInListTypes.IntegerList; break;
                case nameof(Single): 
                case nameof(Double): 
                    type = BuiltInListTypes.FloatList; break;
                case nameof(Boolean): 
                    type = BuiltInListTypes.BooleanList; break;
                case nameof(String): 
                    type = BuiltInListTypes.StringList; break;
                default: _output = null; return;
            }
        }
        else type = TargetType;
        List<object> ret = new();
        for (var i = 0; i < arr.Count; i++)
        {
            if (arr[i].GetType() != typeof(JValue)) { _output = null; return; }
            var value = (arr[i] as JValue).Value;
            if (arr[i] == null) { _output = null; return; };
            switch (type)
            {
                case BuiltInListTypes.IntegerList:
                    switch (value.GetType().Name)
                    {
                        case nameof(Int32):
                            ret.Add((int)value);
                            break;
                        case nameof(Int64):
                            ret.Add((int)(long)value);
                            break;
                        case nameof(Single):
                            ret.Add((int)(float)value);
                            break;
                        case nameof(Double):
                            ret.Add((int)(double)value);
                            break;
                        case nameof(Boolean):
                            ret.Add((bool)value ? 1 : 0);
                            break;
                        default: _output = null; return;
                    }
                    break;
                case BuiltInListTypes.FloatList:
                    switch (value.GetType().Name)
                    {
                        case nameof(Int32):
                            ret.Add((float)(int)value);
                            break;
                        case nameof(Int64):
                            ret.Add((float)(long)value);
                            break;
                        case nameof(Single):
                            ret.Add((float)value);
                            break;
                        case nameof(Double):
                            ret.Add((float)(double)value);
                            break;
                        case nameof(Boolean):
                            ret.Add((bool)value ? 1f : 0f);
                            break;
                        default: _output = null; return;
                    }
                    break;
                case BuiltInListTypes.BooleanList:
                    switch (value.GetType().Name)
                    {
                        case nameof(Int32):
                            ret.Add((int)value > 0);
                            break;
                        case nameof(Int64):
                            ret.Add((long)value > 0);
                            break;
                        case nameof(Single):
                            ret.Add((float)value > 0);
                            break;
                        case nameof(Double):
                            ret.Add((double)value > 0);
                            break;
                        case nameof(Boolean):
                            ret.Add((bool)value ? 1f : 0f);
                            break;
                        default: 
                            ret.Add(value != null); 
                            break;
                    }
                    break;
                case BuiltInListTypes.StringList:
                    ret.Add(value.ToString());
                    break;
            }
        }
        switch (type)
        {
            case BuiltInListTypes.IntegerList:
                _output = ret.Select(x => (int)x).ToArray();
                break;
            case BuiltInListTypes.FloatList:
                _output = ret.Select(x => (float)x).ToArray();
                break;
            case BuiltInListTypes.BooleanList:
                _output = ret.Select(x => (bool)x).ToArray();
                break;
            case BuiltInListTypes.StringList:
                _output = ret.Select(x => (string)x).ToArray();
                break;
        }
    }
}