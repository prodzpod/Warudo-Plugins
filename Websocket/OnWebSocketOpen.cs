using System;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "e491bf5e-87de-42ed-8a13-d4f1c1ca75d9",
    Title = "On WebSocket Connected",
    Category = "Websocket",
    Width = 1f
)]
public class OnWebSocketOpen : Node
{
    private Guid subscriptionId;
    [DataInput]
    public int ID = -1;

    [FlowOutput]
    public Continuation Exit;
    
    protected override void OnCreate()
    {
        Watch<int>(nameof(ID), (from, to) =>
        {
            if (from != -1) Context.EventBus.Unsubscribe<WebSocketOpenEvent>(subscriptionId);
            subscriptionId = Context.EventBus.Subscribe<WebSocketOpenEvent>((e) =>
            {
                if (e.ID != ID) return;
                InvokeFlow(nameof(Exit));
            });
        });
    }
}