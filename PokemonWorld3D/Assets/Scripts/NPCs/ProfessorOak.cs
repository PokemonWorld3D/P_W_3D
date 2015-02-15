using UnityEngine;
using System.Collections;

public class ProfessorOak : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		GameObject player = other.gameObject;
		StartCoroutine(Dialogue(player));
	}
	
	private IEnumerator Dialogue(GameObject player){
		player.GetComponent<Movement>().enabled = false;
		yield return null;
	}
}
