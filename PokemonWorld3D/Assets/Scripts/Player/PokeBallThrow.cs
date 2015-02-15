using UnityEngine;
using System.Collections;

public class PokeBallThrow : MonoBehaviour {

	public AudioClip pokeBallGrow;
	public float throwPower;
	public float maxAngVel;
	public GameObject parentBone;
	public AudioClip pokemonOut;
	public AudioClip pokemonReturn;
	public AudioClip pokeBallShrink;
	public float lineDrawSpeed = 12f;
	public Material laserColor;


	private GameObject pokemonPokeBallPrefab;
	private Animator anim;
	private Vector3 throwPos;
	private Quaternion throwRot;
	private GameObject pokeball;
	private LineRenderer line;
	private GameObject laser;
	private Rigidbody pokeBallRigidbody;
	private float counter;
	private float distance;
	private Vector3 pointB;
	private Vector3 target;
	private GameObject player;
	private PlayersPokemon roster;
	private GameObject pokemonToRelease;

	void Start () {
		pokemonPokeBallPrefab = (GameObject)Resources.Load("Prefabs/Pokemon PokeBall Prefab");
		anim = gameObject.GetComponent<Animator>();
		roster = gameObject.GetComponent<PlayersPokemon>();
	}

	void Update () {
		if(Input.GetButtonUp("Left Mouse Button")){
		//	pokeball = Instantiate(emptyPokeBallPrefab, parentBone.transform.position, parentBone.transform.rotation) as GameObject;
		//	pokeball.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwPower);
		//	pokeball.GetComponent<Rigidbody>().AddTorque(10, 0, 0);
		//	pokeball.GetComponent<Rigidbody>().maxAngularVelocity = maxAngVel;
			anim.SetBool("ThrowingPokeBall", true);
		}
		if(Input.GetButton("Left Mouse Button") && Input.GetButtonUp("Poke Slot 1")){
			StartCoroutine(ThrowPokeBall(roster.pokemonRoster[0]));
		}
	}

	private IEnumerator ThrowPokeBall(PlayerPokemonData pokemonData){
		audio.PlayOneShot(pokeBallGrow);
		yield return new WaitForSeconds(pokeBallGrow.length);
		pokeball = Instantiate(pokemonPokeBallPrefab, parentBone.transform.position, parentBone.transform.rotation) as GameObject;
		pokeball.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throwPower);
		pokeball.GetComponent<Rigidbody>().AddTorque(10, 0, 0);
		pokeball.GetComponent<Rigidbody>().maxAngularVelocity = maxAngVel;
		while(Vector3.Distance(gameObject.transform.position, pokeball.transform.position) < 10f){
			yield return null;
		}
		yield return StartCoroutine(ReleasePokemon(pokemonData));
	}

	private IEnumerator ReleasePokemon(PlayerPokemonData pokemonData){
		pokeball.collider.enabled = false;
		pokeball.rigidbody.Sleep();
		pokeball.transform.LookAt(transform.forward);
		pokeball.audio.PlayOneShot(pokemonOut);
		yield return new WaitForSeconds(pokemonOut.length);
		yield return StartCoroutine(CreatePokemon(pokemonData));
		yield return StartCoroutine(MovePokeBall());
	}
	
	private IEnumerator CreatePokemon(PlayerPokemonData pokemonData){
		pokemonToRelease = (GameObject)Resources.Load("Prefabs/" + pokemonData.pokemonName.ToString() + " Prefab");
		Instantiate(pokemonToRelease, pokeball.transform.position, pokeball.transform.rotation);
		yield return null;
	}
	
	private IEnumerator MovePokeBall(){
		Vector3 moveTo = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		while(Vector3.Distance(pokeball.transform.position, moveTo) > 1f){
			pokeball.transform.position = Vector3.Lerp(pokeball.transform.position, moveTo, 5f * Time.deltaTime);
			yield return null;
		}
		Destroy(pokeball);
		yield return null;
	}



	public void ThrowBall(){
		pokeball.transform.parent = null;
		pokeball.rigidbody.useGravity = true;
		pokeball.rigidbody.AddForce(transform.forward * 1000);
		anim.SetBool("ThrowingPokeBall", false);
	}
}
