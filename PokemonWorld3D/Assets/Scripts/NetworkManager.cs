using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	public string version;
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
		PhotonNetwork.ConnectUsingSettings(version);
	}
	private void SpawnMyPlayer()
	{
		GameObject myPlayer = PhotonNetwork.Instantiate("Trainer_Male", Vector3.zero, Quaternion.identity, 0) as GameObject;
		myPlayer.GetComponent<PlayerInput>().enabled = true;
		myPlayer.GetComponent<AudioListener>().enabled = true;
		myPlayer.GetComponent<PlayerCharacter>().hud = hud.GetComponent<HUD>();
		myPlayer.GetComponent<PlayerCharacter>().Load();
		GameObject myCamera = myPlayer.transform.Find("Camera").gameObject;
		myCamera.transform.parent = null;
		myCamera.SetActive(true);
		hud.GetComponent<HUD>().owner = myPlayer;
	}



	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}