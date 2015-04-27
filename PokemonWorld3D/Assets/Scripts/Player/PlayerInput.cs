using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
	public GameObject myCamera;
	public HUD hud;
	public bool grounded;
	public float walkSpeed = 1.0f;
	public float runMultiplier = 2.0f;
	public float max_velocity_change = 10.0f;
	public bool jumping;									//make private later
	public bool hasJumped;									//make private later
	public float jumpPower = 5.0f;
	public float gravity = 10.0f;
	public bool falling;
	public bool throwingPokeBall;							//make private later
	public bool returningPokemon;
	public GameObject target;								//Make private later.

	private float horizontal;
	private float vertical;
	private Vector3 forward;
	private Vector3 right;
	private PlayerCharacter thisPlayer;
	private ThrowPokeBall throwPokeBall;
	private Animator anim;

	void Start()
	{
		thisPlayer = GetComponent<PlayerCharacter>();
		hud = thisPlayer.hud;
		throwPokeBall = GetComponent<ThrowPokeBall>();
		anim = GetComponent<Animator>();
	}
	void Update()
	{
		if(!grounded && rigidbody.velocity.y > -0.01f && rigidbody.velocity.y < 0.01f)
		{
			falling = false;
			anim.SetBool("Falling", false);
			grounded = true;
		}
		if(Mathf.Abs(rigidbody.velocity.y) > jumpPower * 0.75f)
		{
			falling = true;
			anim.SetBool("Falling", true);
		}
		forward = myCamera.camera.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		right = new Vector3(forward.z, 0f, -forward.x);
		float speed = rigidbody.velocity.normalized.magnitude;
		if(Input.GetButton("Walk"))
		{
			speed *= 1.0f;
		}
		else
		{
			speed *= 2.0f;
		}
		anim.SetFloat("Speed", speed);
		if (Input.GetButtonDown("Jump") && grounded) {
			jumping = true;
			anim.SetBool("Jumping", true);
		}
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPokemon();
		}
		if(Input.GetKeyDown(KeyCode.C) && target != null)
		{
			StartCoroutine(GetComponent<ThrowPokeBall>().PokeBallGo());
		}
		if(Input.GetKeyDown(KeyCode.M))
		{
			GameObject menu = hud.menu;
			if(menu.activeInHierarchy)
			{
				menu.SetActive(false);
			}
			else
			{
				menu.SetActive(true);
			}
		}
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray);
			foreach(RaycastHit hit in hits) //Loop through all the hits
			{
				if(hit.transform.gameObject.CompareTag("Player"))
				{
					GameObject clickedTarget = hit.transform.gameObject;
					if(clickedTarget != gameObject)
					{
						target = clickedTarget;
						hud.targetTrainer = target.GetComponent<PlayerCharacter>();
						hud.otherTrainerPanel.SetActive(true);
						hud.otherTrainerPanel.transform.position = Input.mousePosition;
						break; //Break out because we don't need to check anymore
					}
				}
			}
		}
		SummonPokemon();
		if(Input.GetKeyDown(KeyCode.R) && thisPlayer.activePokemon != null && !returningPokemon && thisPlayer.pokemonRoster.pokemonRoster.Count >= 1)
		{
			returningPokemon = true;
			StartCoroutine(throwPokeBall.PokemonReturn());
			GetComponent<PlayerCharacter>().RemoveActivePokemon();
		}
	}
	void FixedUpdate()
	{
		if(grounded && !throwingPokeBall)
		{
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");
			float speed;
			if(Input.GetButton("Walk"))
			{
				speed = walkSpeed;
			}
			else
			{
				speed = walkSpeed * runMultiplier;
			}
			Vector3 targetVelocity = (horizontal * right + vertical * forward) * speed;
			if (targetVelocity != Vector3.zero)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetVelocity), 10f * Time.smoothDeltaTime);
				transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
			}
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -max_velocity_change, max_velocity_change);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -max_velocity_change, max_velocity_change);
			velocityChange.y = 0;
			if(hasJumped){
				velocityChange.y = CalculateJumpVerticalSpeed();
				grounded = false;
				hasJumped = false;
				jumping = false;
			}
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
	}

	public void Jumping()
	{
		hasJumped = true;
		anim.SetBool("Jumping", false);
	}


	private float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpPower * gravity);
	}
	private void SwapToPokemon()
	{
		if(thisPlayer.activePokemon != null)
		{
			//DISABLE THE POKEMON AI.
			//ENABLE THE PLAYER AI.
			thisPlayer.activePokemon.GetComponent<PokemonInput>().enabled = true;
			myCamera.GetComponent<CameraController>().SetTarget(thisPlayer.activePokemon.transform);
			GetComponent<AudioListener>().enabled = false;
			thisPlayer.activePokemon.GetComponent<AudioListener>().enabled = true;
			this.enabled = false;
		}
	}
	private void SummonPokemon(){
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha1) && thisPlayer.activePokemon == null
		   && !throwingPokeBall && thisPlayer.pokemonRoster.pokemonRoster.Count >= 1){
			throwingPokeBall = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[0]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha2) && thisPlayer.activePokemon == null
		   && !throwingPokeBall && thisPlayer.pokemonRoster.pokemonRoster.Count >= 2){
			throwingPokeBall = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[1]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha3) && thisPlayer.activePokemon == null
		   && !throwingPokeBall && thisPlayer.pokemonRoster.pokemonRoster.Count >= 3){
			throwingPokeBall = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[2]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha4) && thisPlayer.activePokemon == null
		   && !throwingPokeBall && thisPlayer.pokemonRoster.pokemonRoster.Count >= 4){
			throwingPokeBall = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[3]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha5) && thisPlayer.activePokemon == null
		   && !throwingPokeBall && thisPlayer.pokemonRoster.pokemonRoster.Count >= 5){
			throwingPokeBall = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[4]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha6) && thisPlayer.activePokemon == null
		   && !throwingPokeBall && thisPlayer.pokemonRoster.pokemonRoster.Count == 6){
			throwingPokeBall = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[5]));
		}
	}
}
