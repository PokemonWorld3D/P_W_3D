using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PartyRequestPanel : MonoBehaviour
{
	public float duration;
	public Text requestText;

	private float timer;

	void Update()
	{
		timer += Time.deltaTime;
		if(timer >= duration)
		{
			timer = 0.0f;
			gameObject.SetActive(false);
		}
	}

	public void Setup(string trainerName)
	{
		requestText.text = trainerName + "\n has invited you to a party.";
	}
}
