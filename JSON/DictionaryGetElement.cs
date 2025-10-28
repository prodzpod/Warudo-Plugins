using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "b2dceeb6-c9f9-4b31-9cbb-d4c5ba18ce4b",
    Title = "Dictionary Get Element",
    Category = "JSON",
    Width = 1f
)]
public class DictionaryGetElement : Node
{
    [DataInput]
    public object Dictionary = null;
    [DataInput]
    public string Index = "";

    public object _output = null;
    [DataOutput]
    public object Output() => _output;

    protected override void OnCreate()
    {
        base.OnCreate();
        Watch<object>(nameof(Dictionary), (_, __) => OnChange());
        Watch<string>(nameof(Index), (_, __) => OnChange());
    }

    public void OnChange()
    {
        if (Dictionary == null) { _output = null; return; }
        if (!(Dictionary as JObject).ContainsKey(Index)) { _output = null; return; }
        var output = (Dictionary as JObject)[Index];
        if (output.GetType() == typeof(JValue))
        {
            var temp = (output as JValue).Value;
            if (temp.GetType() == typeof(long)) _output = (int)(long)temp;
            else if (temp.GetType() == typeof(double)) _output = (float)(double)temp;
            else _output = temp;
        }
        else if (output.GetType() == typeof(JArray))
        {
            _output = output;
        }
        else _output = output;
    }
}