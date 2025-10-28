using Newtonsoft.Json.Linq;
using System;
using UnityEngine;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "33aed733-934c-4043-a2bb-addb8f9c7e09",
    Title = "Variant List Get Element",
    Category = "CATEGORY_ARITHMETIC",
    Width = 1f
)]
public class VariantListGetElement : Node
{
    [DataInput]
    public object Array = null;
    [DataInput]
    public int Index = 0;

    public object _output = null;
    [DataOutput]
    public object Output() => _output;

    protected override void OnCreate()
    {
        base.OnCreate();
        Watch<object>(nameof(Array), (_, __) => OnChange());
        Watch<int>(nameof(Index), (_, __) => OnChange());
    }

    public void OnChange()
    {
        if (Array == null) { _output = null; return; }
        if (0 > Index || Index >= (Array as JArray).Count) { _output = null; return; }
        var output = (Array as JArray)[Index];
        if (output == null) { _output = null; return; }
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