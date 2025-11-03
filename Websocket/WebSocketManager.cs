using System;
using System.Collections.Generic;
using System.Security.Authentication;
using UnityEngine;
using Warudo.Core;
using Warudo.Core.Events;
using WebSocketSharp;
public class WebsocketManager
{
    private static int ID = 0;
    private static Dictionary<ConnectToWebsocket, int> Nodes = new();
    private static Dictionary<int, WebSocket> WebSockets = new();
    public static int Connect(ConnectToWebsocket node)
    {
        WebSocket ws = new(node.URL);
        int target = -1;
        if (Nodes.ContainsKey(node))
        {
            WebSockets[Nodes[node]].Close();
            WebSockets.Remove(Nodes[node]);
            target = Nodes[node];
        }
        else
        {
            // register a new node
            Nodes.Add(node, ID);
            target = ID;
            ID++;
        }
        // events
        if (node.URL.StartsWith("wss")) ws.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
        ws.OnOpen += (_, __) => { EVENTS.Add(new WebSocketOpenEvent(target)); };
        ws.OnClose += (_, __) => { EVENTS.Add(new WebSocketCloseEvent(target)); };
        ws.OnMessage += (_, res) => { EVENTS.Add(new WebSocketMessageEvent(target, res.Data)); };
        WebSockets.Add(target, ws);
        ws.Connect();
        // UnityEngine.Debug.Log("Connection Attempted: " + target);
        return target;
    } 

    public static void Close(ConnectToWebsocket node)
    {
        if (node == null) return;
        if (Nodes.ContainsKey(node))
        {
            WebSockets[Nodes[node]].Close();
            WebSockets.Remove(Nodes[node]);
            Nodes.Remove(node);
        }
    }

    public static void Send(int id, string message)
    {
        if (!WebSockets.ContainsKey(id)) return;
        WebSockets[id].Send(message);
    }

    public static List<Event> EVENTS = new();
    public static void Update()
    {
        if (EVENTS.Count == 0) return;
        var e = EVENTS[0];
        if (e is WebSocketOpenEvent) Context.EventBus.Broadcast(e as WebSocketOpenEvent);
        if (e is WebSocketMessageEvent) Context.EventBus.Broadcast(e as WebSocketMessageEvent);
        if (e is WebSocketCloseEvent) Context.EventBus.Broadcast(e as WebSocketCloseEvent);
        EVENTS.RemoveAt(0);
    }
}