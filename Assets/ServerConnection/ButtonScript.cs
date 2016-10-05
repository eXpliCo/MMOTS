using UnityEngine;
using UnityEngine.UI;
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
    
	public void RegisterButtonPressed(InputField email, InputField username, InputField password)
    {
		JSONNode response = HttpsClient.register(email.text, username.text, password.text);
        if (response["result"].Equals("true")) 
        {
            authToken = response["authToken"];
        }
        else
        {
            Debug.Log(response["error"]);
        }
    }

	public void LoginButtonPressed(InputField email, InputField password)
    {
		JSONNode response = HttpsClient.login(email.text, password.text);
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
