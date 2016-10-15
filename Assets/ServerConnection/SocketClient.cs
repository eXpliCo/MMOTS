using UnityEngine;
using System;
using SimpleJSON;

public class SocketClient
{
    private static string email;
    private static string authToken;
    private static string ip;
    private static int port;
    private static SocketConnection connection;

	public static void Init (string ip, int port, string email, string authToken)
    { 
        SocketClient.authToken = authToken;
        SocketClient.email = email;
        SocketClient.ip = ip;
        SocketClient.port = port;
        SocketClient.ConnectAndAuth();
    }

    public static string GetMessage()
    {
        if(!SocketClient.connection.IsAlive())
        {
            SocketClient.ConnectAndAuth();
        }
        return SocketClient.connection.GetMessage();
    }

    public static void SendMessage(string message)
    {
        if (!SocketClient.connection.IsAlive())
        {
            SocketClient.ConnectAndAuth();
        }
        SocketClient.connection.SendMessage(message);
    }

    private static void ConnectAndAuth()
    {
        SocketClient.connection = new SocketConnection(ip, port);
        var json = JSON.Parse("{}");
        json["method"] = "Authentication";
        json["email"] = SocketClient.email;
        json["authToken"] = SocketClient.authToken;
        SocketClient.connection.SendMessage(json.ToString());
    }
}


