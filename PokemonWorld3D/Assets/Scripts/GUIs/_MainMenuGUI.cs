using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _MainMenuGUI : MonoBehaviour {

	public const string version = "0.0.003";
	public bool clearPrefs = false;

	private Text versionText;
	private GameObject loadingScreen;
	private _SceneFadeInOut loadScreen;
	private bool hasCharacter = false;

	void Start () {
		versionText = transform.FindChild("Version").GetComponent<Text>();
		versionText.text = "version " + version;
		loadingScreen = GameObject.Find("Loading Screen");
		loadScreen = loadingScreen.GetComponent<_SceneFadeInOut>();


		if(clearPrefs)
			PlayerPrefs.DeleteAll();
		
		if(PlayerPrefs.HasKey("ver")){
			Debug.Log("There is a version key.");
			if(PlayerPrefs.GetString("ver") != version){
				Debug.Log("Saved version is not the same as current version.");
			}else{
				Debug.Log("Saved version is the same as current version.");
				if(PlayerPrefs.HasKey("Player Name")){
					Debug.Log("There is a player name tag.");
					if(PlayerPrefs.GetString("Player Name") == "" ){
						Debug.Log("The player name key does not have anything in it.");
						PlayerPrefs.DeleteAll();
						//zoneToLoad = characterGeneration;
					}else{
						Debug.Log("The player name key has a value.");
						hasCharacter = true;
					}
				}else{
					Debug.Log("There is no player name key.");
					PlayerPrefs.DeleteAll();
					PlayerPrefs.SetString("ver", version);
					//zoneToLoad = characterGeneration;
				}
			}
		}else{
			Debug.Log("There is no version key.");
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetString("ver", version);
			//zoneToLoad = characterGeneration;
		}
	}

	void Update () {
		if(!hasCharacter){
			transform.FindChild("Load Game").GetComponent<Button>().interactable = false;
		}
	}

	public void NewGame(){
		PlayerPrefs.DeleteAll();
		PlayerPrefs.SetString("ver", version);
		this.gameObject.GetComponent<Canvas>().enabled = false;
		loadScreen.EndScene("CharacterGeneration");
	}

	public void LoadGame(){
		this.gameObject.GetComponent<Canvas>().enabled = false;
		loadScreen.EndScene(PlayerPrefs.GetString("Last Zone", "PalletTown"));
	}

	public void Quit(){
		Application.Quit();
	}
}
