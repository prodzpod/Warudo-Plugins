using System;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "ca39e32b-c220-4aa0-96c9-e5ffe4950234",
    Title = "On WebSocket Disconnected",
    Category = "CATEGORY_EXTERNAL_INTEGRATION",
    Width = 1f
)]
public class OnWebSocketClose : Node
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
            if (from != -1) Context.EventBus.Unsubscribe<WebSocketCloseEvent>(subscriptionId);
            subscriptionId = Context.EventBus.Subscribe<WebSocketCloseEvent>((e) =>
            {
                if (e.ID != ID) return;
                InvokeFlow(nameof(Exit));
            });
        });
    }
}