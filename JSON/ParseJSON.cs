using Newtonsoft.Json.Linq;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "694c0e99-4fdd-4818-a362-f9389ebb874e",
    Title = "Parse JSON",
    Category = "CATEGORY_DATA",
    Width = 1f
)]
public class ParseJSON : Node
{
    [DataInput]
    public string Input = "";

    public object _output = null;
    [DataOutput]
    public object Output() => _output;

    protected override void OnCreate()
    {
        base.OnCreate();
        Watch<string>(nameof(Input), (_, str) => {
            str = str.Trim();
            if (str[0] == '[') _output = JArray.Parse(str);
            else _output = JObject.Parse(str);
        });
    }
}