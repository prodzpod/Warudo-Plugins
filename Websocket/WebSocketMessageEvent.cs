using Warudo.Core.Events;

public class WebSocketMessageEvent : Event
{
    public int ID { get; }
    public string Message { get; }
    public WebSocketMessageEvent(int id, string message)
    {
        //UnityEngine.Debug.Log("Websocket Message: " + id + ": " + message);
        ID = id;
        Message = message;
    }
}