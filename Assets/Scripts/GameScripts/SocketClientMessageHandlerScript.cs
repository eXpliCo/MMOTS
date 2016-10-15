using UnityEngine;
using System.Collections;
using SimpleJSON;

public class SocketClientMessageHandlerScript : MonoBehaviour {
    
	void Start () {
	
	}
	
	void Update () {
        if (SocketClient.HasBeenInitiated())
        {
            string msg = SocketClient.GetMessage();
            if (msg != null)
            {
                JSONNode message = JSON.Parse(msg);
                if (message["method"].Equals("Authentication"))
                {
                    SocketClient.HandleAuthenticationMessage(message);
                }
                else if (message["method"].Equals("Ping"))
                {
                    
                }
                else
                {
                    Debug.Log("Unexpected message from server: " + msg);
                }
            }
        }
	}
}
