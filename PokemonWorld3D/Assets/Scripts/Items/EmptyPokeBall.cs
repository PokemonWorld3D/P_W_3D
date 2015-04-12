using UnityEngine;
using System.Collections;

public class EmptyPokeBall : MonoBehaviour
{
	public AudioClip captureAttempt;
	public AudioClip attemptingCapture;
	public AudioClip captureSuccess;
	public AudioClip captureFail;
	public _PokeBall.PokeBallTypes thisPokeBallType;
	public PlayerCharacter thisPlayer;

	private RaycastHit hit;
	private float distanceToGround;
	private CalculateCapture calculateCaptureScript = new CalculateCapture();

	void Update()
	{
		if(Physics.Raycast(transform.position, -Vector3.up, out hit)){
			distanceToGround = hit.distance;
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Pokemon")
		{
			Pokemon thisPokemon = col.gameObject.GetComponent<Pokemon>();
			if(!thisPokemon.isCaptured)
			{
				audio.PlayOneShot(captureAttempt);
				col.gameObject.GetComponent<Animation>().enabled = false;
				//col.collider.enabled = false;
				// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				// Code goes here to change the Pokemon to red.
				StartCoroutine(Capture(col));
			}
			else
			{
				
			}
		}
		else
		{
			PhotonNetwork.Destroy(gameObject);
		}
	}

	private IEnumerator Capture(Collision col)
	{
		rigidbody.useGravity = false;
		yield return StartCoroutine(MovePokeBall(col));
		rigidbody.WakeUp();
		rigidbody.useGravity = true;
		while(distanceToGround > 0.2f)
		{
			yield return null;
		}
		rigidbody.isKinematic = true;
		yield return StartCoroutine(TryToCatch(col));
	}
	private IEnumerator MovePokeBall(Collision col)
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.Sleep();
		float offset = 1.5f;
		Vector3 moveTo = new Vector3(transform.position.x - offset, col.contacts[0].point.y + offset, transform.position.z - offset);
		while(Vector3.Distance(transform.position, moveTo) > 0.01f)
		{
			transform.LookAt(col.transform.position);
			transform.position = Vector3.Lerp(transform.position, moveTo, 5f * Time.deltaTime);
			yield return null;
		}
		animation["Open"].speed = 5;
		animation.Play("Open");
		GameObject orb = PhotonNetwork.Instantiate("Capture_Orb", col.gameObject.GetComponentInChildren<Renderer>().renderer.bounds.center,
		                                           Quaternion.identity, 0) as GameObject;
		// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		// Need to include some code here that makes the Pokemon shrink and until it's smaller than the capture orb and then disappear.
		col.gameObject.SetActive(false);
		while(Vector3.Distance(transform.position, orb.transform.position) > 0.01f)
		{
			orb.transform.position = Vector3.Lerp(orb.transform.position, transform.position, 2.7f * Time.deltaTime);
			yield return null;
		}
		Destroy(orb);
		//orb.transform.parent = my_transform;
		animation["Close"].speed = -5f;
		animation["Close"].time = animation["Close"].length;
		animation.Play("Close");
		Vector3 flatFwd = new Vector3(transform.forward.x, 0, transform.forward.z);
		Quaternion fwdRotation = Quaternion.LookRotation(flatFwd, Vector3.up);
		float angle = Quaternion.Angle(transform.rotation, fwdRotation);
		while(angle > 1.0f)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, fwdRotation, Time.deltaTime * 5.0f);
			angle = Quaternion.Angle(transform.rotation, fwdRotation);
			yield return null;
		}
		yield return null;
	}
	private IEnumerator TryToCatch(Collision col)
	{
		Pokemon thisPokemon = col.gameObject.GetComponent<Pokemon>();
		audio.PlayOneShot(attemptingCapture);
		animation["Trying_To_Catch"].speed = 2;
		animation.Play("Trying_To_Catch");
		yield return new WaitForSeconds(attemptingCapture.length+1);
		bool tryToCapture = calculateCaptureScript.AttemptCapture(thisPokemon, thisPokeBallType);
		if(tryToCapture){
			thisPokemon.isCaptured = true;
			thisPokemon.trainersName = thisPlayer.playersName;
			Pokemon temp = thisPokemon;
			PlayerPokemonData dataHolderPokemon = new PlayerPokemonData(temp.isCaptured, temp.pokemonName, temp.nickName,
			                                                              temp.isFromTrade, temp.level, temp.gender, temp.nature, temp.curHP, temp.hpEV, temp.atkEV,
			                                                              temp.defEV, temp.spatkEV, temp.spdefEV, temp.spdEV, temp.hpIV, temp.atkIV, temp.defIV,
			                                                              temp.spatkIV, temp.spdefIV, temp.spdIV, temp.currentEXP,
			                                                              temp.MovesToLearnNames, temp.KnownMovesNames,
			                                                              temp.equippedItem.name, temp.origin);
			if(thisPlayer.pokemonRoster.pokemonRoster.Count < 6)
			{
				thisPlayer.pokemonRoster.pokemonRoster.Add(dataHolderPokemon);
			}
			else
			{
				thisPlayer.pokemonInventory.pokemonInventory.Add(dataHolderPokemon);
			}
			thisPokemon.isCaptured = false;
			thisPokemon.SetDead();
			audio.PlayOneShot(captureSuccess);
			yield return new WaitForSeconds(captureSuccess.length);
		}
		else
		{
			animation["Open_Top"].speed = 5;
			animation.Play("Open_Top");
			audio.PlayOneShot(captureFail);
			yield return new WaitForSeconds(animation["Open_Top"].length);
			// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			// Code goes here to change the Pokemon back from red.
			col.gameObject.SetActive(true);
			col.gameObject.GetComponent<Animation>().enabled = true;
			animation["Close_Top"].speed = -5f;
			animation["Close_Top"].time = animation["Close_Top"].length;
			animation.Play("Close_Top");
			yield return new WaitForSeconds(captureFail.length);
		}
		PhotonNetwork.Destroy(gameObject);
		yield return null;
	}
}
