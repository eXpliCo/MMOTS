using UnityEngine;
using System.Collections;
using SimpleJSON;


public class ButtonScript : MonoBehaviour {

    public static string authToken;

	// Use this for initialization
	void Start ()
    {
        HttpsClient.base_url = "https://127.0.0.1:7000";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void RegisterButtonPressed()
    {
        JSONNode response = HttpsClient.register("email@email.com", "username", "password");
        if (response["result"].Equals("true")) 
        {
            authToken = response["authToken"];
        }
        else
        {
            Debug.Log(response["error"]);
        }
    }

    public void LoginButtonPressed()
    {
        JSONNode response = HttpsClient.login("email@email.com", "password");
        if (response["result"].Equals("true"))
        {
            authToken = response["authToken"];
        }
        else
        {
            Debug.Log(response["error"]);
        }
    }

    public void SendPasswordResetTokenButtonPressed()
    {
        JSONNode response = HttpsClient.sendPasswordResetToken("email@email.com");
        if (response["result"].Equals("true"))
        {
        }
        else
        {
            Debug.Log(response["error"]);
        }
    }

    public void ResetPasswordButtonPressed()
    {
        JSONNode response = HttpsClient.resetPassword("email@email.com", "password", "ASDTOKEN");
        if (response["result"].Equals("true"))
        {
            authToken = response["authToken"];
        }
        else
        {
            Debug.Log(response["error"]);
        }
    }
}
