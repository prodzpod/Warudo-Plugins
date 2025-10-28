using Warudo.Core.Events;

public class WebSocketCloseEvent : Event
{
    public int ID { get; }
    public WebSocketCloseEvent(int id)
    {
        //UnityEngine.Debug.Log("Websocket Closed: " + id);
        ID = id;
    }
}