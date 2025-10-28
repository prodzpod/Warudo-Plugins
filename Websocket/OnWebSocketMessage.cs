using System;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "914d2d2c-db7b-4eaa-96f9-0775b3279c4d",
    Title = "On WebSocket Message",
    Category = "Websocket",
    Width = 1f
)]
public class OnWebSocketMessage : Node
{
    private Guid subscriptionId;
    [DataInput]
    public int ID = -1;

    [FlowOutput]
    public Continuation Exit;

    private string message = "";
    [DataOutput]
    public string Message() => message;
    
    protected override void OnCreate()
    {
        Watch<int>(nameof(ID), (from, to) =>
        {
            if (from != -1) Context.EventBus.Unsubscribe<WebSocketMessageEvent>(subscriptionId);
            subscriptionId = Context.EventBus.Subscribe<WebSocketMessageEvent>((e) =>
            {
                if (e.ID != ID) return;
                message = e.Message;
                InvokeFlow(nameof(Exit));
            });
        });
    }
}