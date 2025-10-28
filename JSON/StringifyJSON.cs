using Newtonsoft.Json.Linq;
using System.Xml;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "f6cea874-8f76-4dea-92b7-00e59de9a7e9",
    Title = "Stringify JSON",
    Category = "JSON",
    Width = 1f
)]
public class StringifyJSON : Node
{
    [DataInput]
    public object Input = null;

    public string _output = "";
    [DataOutput]
    public string Output() => _output;

    protected override void OnCreate()
    {
        base.OnCreate();
        Watch<object>(nameof(Input), (_, obj) => {
            if (Input == null) { _output = ""; return; }
            if (obj.GetType() == typeof(JArray)) _output = (obj as JArray).ToString();
            else _output = (obj as JObject).ToString();
        });
    }
}