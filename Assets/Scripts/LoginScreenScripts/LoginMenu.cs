using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class LoginMenu : MonoBehaviour {
	public string loginServerAddress;
	public InputField loginEmail;
	public InputField loginPassword;

	public InputField registerUsername;
	public InputField registerEmail;
	public InputField registerPassword;

	private const float CAMERA_TRANSITION_SPEED = 3.0f;
    
	private Transform cameraTransform;
	private Transform cameraDesiredLookAt;
    
    private ServerConnection server;

    private void Start()
	{
		HttpsClient.base_url = loginServerAddress;
		cameraTransform = Camera.main.transform;
	}

	private void Update() 
	{
		if (cameraDesiredLookAt != null) 
		{
			cameraTransform.rotation = Quaternion.Slerp (cameraTransform.rotation, cameraDesiredLookAt.rotation, CAMERA_TRANSITION_SPEED * Time.deltaTime);
		}
        if(this.server != null)
        {
            string msg = server.GetMessage();
            if (msg != null)
            {
                Debug.Log("Received message from server: " + msg);
            }
        }
	}

	public void LookAtMenu(Transform menuTransform)
	{
		cameraDesiredLookAt = menuTransform;
	}

	public void RegisterButtonPressed()
	{
		JSONNode response = HttpsClient.register(registerEmail.text, registerUsername.text, registerPassword.text);
		if (response["result"].AsBool.Equals(true)) 
		{
			PlayerPrefs.SetString("authToken", response["authToken"]);
			SceneManager.LoadScene ("GameWorld");
		}
		else
		{
			Debug.Log(response["error"]);
		}
	}

	public void LoginButtonPressed()
	{
		JSONNode response = HttpsClient.login(loginEmail.text, loginPassword.text);
		if (response["result"].AsBool.Equals(true))
        {
            this.server = new ServerConnection("malow.mooo.com", 7001);
            this.server.SendMessage("test message");
            PlayerPrefs.SetString("authToken", response["authToken"]);
			//SceneManager.LoadScene ("GameWorld");
        }
		else
		{
			Debug.Log(response["error"]);
		}
	}
}
