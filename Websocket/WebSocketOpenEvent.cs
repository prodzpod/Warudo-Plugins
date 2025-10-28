using Warudo.Core.Events;

public class WebSocketOpenEvent : Event
{
    public int ID { get; }
    public WebSocketOpenEvent(int id)
    {
        //UnityEngine.Debug.Log("Websocket Opened: " + id);
        ID = id;
    }
}