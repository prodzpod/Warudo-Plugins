using Warudo.Core;
using Warudo.Core.Attributes;
using Warudo.Core.Graphs;

[NodeType(Id = "73b91d73-aa9c-448f-84cd-c90d119645b7", 
    Title = "Connect to WebSocket",
    Category = "Websocket")]
public class ConnectToWebsocket : Node
{
    [DataInput]
    public string URL = "wss://";

    [FlowInput]
    public Continuation Enter()
    {
        _id = WebsocketManager.Connect(this);
        return Exit;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        WebsocketManager.Close(this);
    }

    private int _id = -1;
    [DataOutput]
    public int ID() => _id;

    [FlowOutput]
    public Continuation Exit;

}