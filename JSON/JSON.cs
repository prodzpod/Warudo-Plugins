using UnityEngine;
using Warudo.Core.Attributes;
using Warudo.Core.Plugins;

[PluginType(
    Id = "prod.json",
    Name = "JSON",
    Description = "adds JSON capabilities.",
    Version = "1.0.0",
    Author = "prod",
    SupportUrl = "https://prod.kr",
    AssetTypes = new System.Type[] {},
    NodeTypes = new [] { 
        typeof(ParseJSON),
        typeof(StringifyJSON),
        typeof(DictionaryGetElement),
        typeof(DictionarySetElement),
        typeof(VariantListGetElement),
        typeof(CastVariantList),
    })]
public class JSONPlugin : Plugin {

    protected override void OnCreate() {
        base.OnCreate();
        Debug.Log("[JSON v1.0] initialized");
    }

}