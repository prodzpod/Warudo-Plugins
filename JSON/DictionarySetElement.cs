using Newtonsoft.Json.Linq;
using System;
using System.Xml;
using UnityEngine;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "e64c8193-0794-43ba-bbff-6affe9723542",
    Title = "Dictionary Set Element",
    Category = "JSON",
    Width = 1f
)]
public class DictionarySetElement : Node
{
    [DataInput]
    public object Dictionary = null;
    [DataInput]
    public string Index = "";
    [DataInput]
    public object Value = null;

    public object _output = null;
    [DataOutput]
    public object Output() => _output;

    protected override void OnCreate()
    {
        base.OnCreate();
        _output = Dictionary;
        Watch<object>(nameof(Dictionary), (_, __) => OnChange());
        Watch<string>(nameof(Index), (_, __) => OnChange());
        Watch<object>(nameof(Value), (_, __) => OnChange());
    }

    public void OnChange()
    {
        if (Dictionary == null) { _output = null; return; }
        var ret = (Dictionary as JObject).DeepClone() as JObject;
        if (Value == null) { ret[Index] = new JValue((object)null); return; }
        switch (Value.GetType().Name)
        {
            case nameof(JObject):
            case nameof(JArray):
                ret[Index] = Value as JToken;
                break;
            case nameof(Int32):
                ret[Index] = new JValue((int)Value);
                break;
            case nameof(Int32) + "[]":
                ret[Index] = new JArray(Value as int[]);
                break;
            case nameof(Int64):
                ret[Index] = new JValue((long)Value);
                break;
            case nameof(Int64) + "[]":
                ret[Index] = new JArray(Value as long[]);
                break;
            case nameof(Single):
                ret[Index] = new JValue((float)Value);
                break;
            case nameof(Single) + "[]":
                ret[Index] = new JArray(Value as float[]);
                break;
            case nameof(Double):
                ret[Index] = new JValue((double)Value);
                break;
            case nameof(Double) + "[]":
                ret[Index] = new JArray(Value as double[]);
                break;
            case nameof(String):
                ret[Index] = new JValue((string)Value);
                break;
            case nameof(String) + "[]":
                ret[Index] = new JArray(Value as string[]);
                break;
            case nameof(Boolean):
                ret[Index] = new JValue((bool)Value);
                break;
            case nameof(Boolean) + "[]":
                ret[Index] = new JArray(Value as bool[]);
                break;
            case nameof(Vector2):
            case nameof(Vector3):
            case nameof(Vector4):
                ret[Index] = new JValue(Value);
                break;
            case nameof(Vector3) + "[]":
                ret[Index] = new JArray(Value as object[]);
                break;
        }
        _output = ret;
    }
}