using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class _SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 1.5f;

	private bool sceneStarting = false;
	private float invisible = 0f;
	private float visible = 1f;
	private Image image;
	private Text lsPercent;
	private Image lsPercentBar;

	void Awake(){
		DontDestroyOnLoad(this);
		image = this.gameObject.GetComponent<Image>();
		lsPercent = this.gameObject.GetComponentInChildren<Text>();
		lsPercentBar = this.gameObject.GetComponentInChildren<Image>();
	}

	void Start(){
		StartScene();
	}

	void Update(){
		if(sceneStarting)
			StartScene();
	}

	private IEnumerator FadeLoadScreenIn(float alphaLevel, float duration, string zoneToLoad){
		this.gameObject.GetComponent<Canvas>().enabled = true;
		float alpha = image.color.a;
		float percentLoaded;
		for(float t = 0f; t < 1f; t += Time.deltaTime / duration){
			Color newAlpha = new Color(1, 1, 1, Mathf.Lerp(alpha, alphaLevel, t));
			image.color = newAlpha;
			yield return null;
		}
		while(!Application.CanStreamedLevelBeLoaded(zoneToLoad)){
			percentLoaded = Application.GetStreamProgressForLevel(zoneToLoad);
			if(Application.GetStreamProgressForLevel(zoneToLoad) == 1)
				percentLoaded = 1f;
			lsPercentBar.GetComponent<Image>().fillAmount = percentLoaded;
			lsPercent.GetComponent<Text>().text = "Loading " + percentLoaded * 100 + "%...";
			yield return null;
		}
		Application.LoadLevel(zoneToLoad);
		sceneStarting = true;
	}

	private IEnumerator FadeLoadScreenOut(float alphaLevel, float duration){
		float alpha = image.color.a;
//		float percentLoaded;
		for(float t = 0f; t < 1f; t += Time.deltaTime / duration){
			Color newAlpha = new Color(1, 1, 1, Mathf.Lerp(alpha, alphaLevel, t));
			image.color = newAlpha;
			yield return null;
		}
		sceneStarting = false;
		this.gameObject.GetComponent<Canvas>().enabled = false;
	}

	private void StartScene(){
		StartCoroutine(FadeLoadScreenOut(invisible, 2f));
	}

	public void EndScene(string zoneToLoad){
		StartCoroutine(FadeLoadScreenIn(visible, 2f, zoneToLoad));
	}

}
