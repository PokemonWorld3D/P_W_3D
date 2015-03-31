using UnityEngine;
using System.Collections;

public class _FlyingPokemon : MonoBehaviour {

	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;
	
	private Rigidbody pokemonRigidbody;

	void Awake () {
		pokemonRigidbody = GetComponent<Rigidbody>();
	}

	void Update () {

	}

	void FixedUpdate(){
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, hoverHeight)){
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			pokemonRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	}
}
