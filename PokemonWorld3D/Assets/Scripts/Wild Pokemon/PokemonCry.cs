using UnityEngine;
using System.Collections;

public class PokemonCry : MonoBehaviour {

	public float cryTime;

	private float cryTimer;

	void Start () {
	
	}

	void Update () {
		cryTimer += Time.deltaTime;
		if(cryTimer > cryTime){
			audio.Play();
			cryTimer = 0f;
		}
	}
}
