using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _LogIn : MonoBehaviour
{
	public NetworkManager network_manager;
	public InputField username;
	public InputField password;
	public GameObject start_server;
	public Button start_server_button;
	public Button login;
	public GameObject kanto;

	public string admin_username = "";
	public string admin_password = "";

	void Update()
	{
		if(username.text == admin_username && password.text == admin_password)
		{
			start_server.SetActive(true);
		}
		if(!Network.isClient && !Network.isServer)
		{
			start_server_button.enabled = true;
		}
		if(username.text != "")
		{
			login.interactable = true;
		}
		else
		{
			login.interactable = false;
		}
		/*if(network_manager.hostList != null)
		{
			kanto.SetActive(true);
		}*/
	}

	public void Quit()
	{
		Application.Quit();
	}
}
