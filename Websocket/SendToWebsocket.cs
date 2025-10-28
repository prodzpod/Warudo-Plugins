using System;
using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(
    Id = "07f78c96-e2b6-4415-a0b0-2e0b77f9800b",
    Title = "Send to Websocket",
    Category = "CATEGORY_EXTERNAL_INTEGRATION",
    Width = 1f
)]
public class SendToWebsocket : Node
{
    [DataInput]
    public int ID = -1;

    [DataInput]
    public string Message = "";

    [FlowInput]
    public Continuation Enter()
    {
        if (ID == -1) return Exit;
        WebsocketManager.Send(ID, Message);
        return Exit;
    }

    [FlowOutput]
    public Continuation Exit;
}