using UnityEngine;
using System;

public class ServerConnection : Process
{
	private NetworkChannel server;

	public ServerConnection (string ip, int port)
	{
		this.server = new NetworkChannel (ip, port);
		this.server.SetNotifier (this);
		this.server.Start ();
	}

	public string GetMessage()
	{
		ProcessEvent ev = this.PeekEvent ();
		NetworkPacket np = ev as NetworkPacket;
		if (np != null) {
			return np.GetMessage();
		}
		return null;
	}

	public void SendMessage(string msg)
	{
		this.server.SendMessage (msg);
	}

	public void Quit ()
	{
		this.server.Close();
		this.server.WaitUntillDone ();
	}

	public override void Life() {
	}
}


