using UnityEngine;
using System.Collections;

public class SocketConnection : Process
{
    private NetworkChannel server;

    public SocketConnection(string ip, int port)
    {
        this.server = new NetworkChannel(ip, port);
        this.server.SetNotifier(this);
        this.server.Start();
    }

    public string GetMessage()
    {
        ProcessEvent ev = this.PeekEvent();
        NetworkPacket np = ev as NetworkPacket;
        if (np != null)
        {
            return np.GetMessage();
        }
        return null;
    }

    public void SendMessage(string msg)
    {
        this.server.SendMessage(msg);
    }

    public override void Life()
    {
    }

    public bool IsAlive()
    {
        return this.server.GetState() == Process.RUNNING;
    }

    public void Quit()
    {
        this.server.Close();
        this.server.WaitUntillDone();
    }
}
