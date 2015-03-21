using UnityEngine;
using System.Collections;

public class _NetworkManager : MonoBehaviour
{
	public GameObject hud;

	void Start()
	{
		Connect();
	}
	void OnJoinedLobby()
	{
		RoomOptions options = new RoomOptions() { isVisible = false, isOpen = true, maxPlayers = 0, cleanupCacheOnLeave = true }; 
		PhotonNetwork.JoinOrCreateRoom("Kanto", options, TypedLobby.Default);
	}
	void OnJoinedRoom()
	{
		SpawnMyPlayer();
	}

	private void Connect()
	{
		PhotonNetwork.ConnectUsingSettings("0.1.000");
	}
	private void SpawnMyPlayer()
	{
		GameObject myPlayer = PhotonNetwork.Instantiate("_Trainer_Male", Vector3.zero, Quaternion.identity, 0) as GameObject;
		myPlayer.GetComponent<_PlayerInput>().enabled = true;
		myPlayer.GetComponent<AudioListener>().enabled = true;
		myPlayer.GetComponent<_PlayerCharacter>().hud = hud.GetComponent<_HUD>();
		myPlayer.GetComponent<_PlayerCharacter>().Load();
		GameObject myCamera = myPlayer.transform.Find("Camera").gameObject;
		myCamera.transform.parent = null;
		myCamera.SetActive(true);
		hud.GetComponent<_HUD>().owner = myPlayer;
		//GameObject myHUD = myPlayer.transform.Find("HUD").gameObject;
		//myHUD.transform.parent = null;
		//myHUD.SetActive(true);
	}



	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
