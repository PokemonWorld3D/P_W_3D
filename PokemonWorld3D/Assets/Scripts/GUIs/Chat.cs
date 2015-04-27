using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chat : MonoBehaviour
{
	public Text thisText;
	public InputField chatInput;
	public GameObject chatParent;

	private string oldText = "";
	private string chatText;
	private PhotonView photonView;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
	}
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Return))
		{
			chatInput.ActivateInputField();
			chatInput.Select();
		}
		if(chatInput.text != "" && Input.GetKeyDown(KeyCode.Return))
		{
			SendMessage(chatParent);
		}
	}

	[RPC]
	public void ChatSend(string text)
	{
		thisText.text = text;
	}
	public void SendMessage(GameObject inputField)
	{
		chatText = chatInput.text;
		oldText = thisText.text;
		chatInput.text = "";
		photonView.RPC("ChatSend", PhotonTargets.All, oldText + chatText + "\n");
	}
}
