using UnityEngine;
using Warudo.Core.Attributes;
using Warudo.Core.Plugins;

[PluginType(
    Id = "prod.websocket",
    Name = "WebSocket",
    Description = "adds advanced WebSocket capabilities. requires JSON plugin.",
    Version = "1.0.0",
    Author = "prod",
    SupportUrl = "https://prod.kr",
    AssetTypes = new System.Type[] {},
    NodeTypes = new [] { 
        typeof(ConnectToWebsocket),
        typeof(OnWebSocketOpen),
        typeof(OnWebSocketMessage),
        typeof(OnWebSocketClose),
        typeof(SendToWebsocket)
    })]
public class WebSocketPlugin : Plugin {

    protected override void OnCreate() {
        base.OnCreate();
        Debug.Log("[WS v1.0] initialized");
    }

}