using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _PlayerInput : MonoBehaviour
{
	public GameObject myCamera;
	public _HUD hud;
	public bool grounded;
	public float walkSpeed = 1.0f;
	public float runMultiplier = 2.0f;
	public float max_velocity_change = 10.0f;
	public bool jumping;									//make private later
	public bool hasJumped;									//make private later
	public float jumpPower = 5.0f;
	public float gravity = 10.0f;
	public bool falling;
	public bool throwCoroutineStarted;						//make private later
	public GameObject target;								//Make private later.
	public _Pokemon targetPokemon;							//Make private later.
	public List<GameObject> Targets;						//Make private later.

	private _PlayerCharacter thisPlayer;
	private _ThrowPokeBall throwPokeBall;
	private Animator anim;

	void Start()
	{
		thisPlayer = GetComponent<_PlayerCharacter>();
		hud = thisPlayer.hud;
		throwPokeBall = GetComponent<_ThrowPokeBall>();
		anim = GetComponent<Animator>();
	}
	void Update()
	{
		if(throwCoroutineStarted)
		{
			return;
		}
		if(jumping)
		{
			return;
		}
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
		if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			if(Input.GetButton("Walk"))
			{
				anim.SetFloat("Speed", 1.0f);
			}
			else
			{
				anim.SetFloat("Speed", 2.0f);
			}
		}
		else
		{
			anim.SetFloat("Speed", 0.0f);
			if(!falling && !jumping)
				Idle();
		}
		if (Input.GetButtonDown("Jump") && grounded) {
			jumping = true;
			anim.SetBool("Jumping", true);
		}
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPokemon();
		}
		if(Input.GetButtonDown("Targeting"))
		{
			AddAllTargets();
			TargetPokemon();
		}
		if(Input.GetKeyDown(KeyCode.C) && target != null)
		{
			StartCoroutine(GetComponent<_ThrowPokeBall>().PokeBallGo());
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
		KeepTrackOfTargets();
		SummonPokemon();
	}
	void FixedUpdate()
	{
		if(throwCoroutineStarted)
		{
			return;
		}
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 forward = myCamera.camera.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0f, -forward.x);
		if(grounded)
		{
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

	private void Idle()
	{
		if(thisPlayer.isInBattle)
		{
			anim.SetBool("InBattle", true);
		}
		else
		{
			anim.SetBool("InBattle", false);
		}
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
			thisPlayer.activePokemon.GetComponent<_PokemonInput>().enabled = true;
			myCamera.GetComponent<_CameraController>().SetTarget(thisPlayer.activePokemon.transform);
			GetComponent<AudioListener>().enabled = false;
			thisPlayer.activePokemon.GetComponent<AudioListener>().enabled = true;
			this.enabled = false;
		}
	}
	private void AddAllTargets()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Pokemon");
		
		foreach(GameObject g in go)
		{
			AddTarget(g);
		}
	}
	private void AddTarget(GameObject target)
	{
		//Add an if statement here if we want to filter the results from "AddAllTargets".
		Targets.Add(target);
	}
	private void RemoveTarget(GameObject aTarget)
	{
		if(target = aTarget)
		{
			target = null;
			hud.NoTarget();
		}
		Targets.Remove(aTarget);
	}
	private void KeepTrackOfTargets()
	{
		for(int i = 0; i < Targets.Count; i++)
		{
			if(Targets[i].GetComponent<_Pokemon>().curHP == 0)
			{
				Targets.Remove(Targets[i]);
			}
		}
	}
	private void SortTargetsByDistance()
	{
		Targets.Sort(delegate(GameObject c1, GameObject c2){
			return Vector3.Distance(this.transform.position, c1.transform.position).CompareTo
				((Vector3.Distance(this.transform.position, c2.transform.position)));   
		});
	}
	private void TargetPokemon()
	{
		if(target == null)
		{
			SortTargetsByDistance();
			target = Targets[0];
			targetPokemon = target.GetComponent<_Pokemon>();
			hud.SetTarget(targetPokemon);
		}
		else
		{
			int index = Targets.IndexOf(target);
			
			if(index < Targets.Count - 1)
			{
				index++;
			}
			else
			{
				index = 0;
			}
			target = Targets[index];
			targetPokemon = Targets[index].GetComponent<_Pokemon>();
			hud.SetTarget(targetPokemon);
		}
	}
	private void SummonPokemon(){
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha1) && thisPlayer.activePokemon == null
		   && !throwCoroutineStarted && thisPlayer.pokemonRoster.pokemonRoster.Count >= 1){
			throwCoroutineStarted = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[0]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha2) && thisPlayer.activePokemon == null
		   && !throwCoroutineStarted && thisPlayer.pokemonRoster.pokemonRoster.Count >= 2){
			throwCoroutineStarted = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[1]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha3) && thisPlayer.activePokemon == null
		   && !throwCoroutineStarted && thisPlayer.pokemonRoster.pokemonRoster.Count >= 3){
			throwCoroutineStarted = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[2]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha4) && thisPlayer.activePokemon == null
		   && !throwCoroutineStarted && thisPlayer.pokemonRoster.pokemonRoster.Count >= 4){
			throwCoroutineStarted = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[3]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha5) && thisPlayer.activePokemon == null
		   && !throwCoroutineStarted && thisPlayer.pokemonRoster.pokemonRoster.Count >= 5){
			throwCoroutineStarted = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[4]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha6) && thisPlayer.activePokemon == null
		   && !throwCoroutineStarted && thisPlayer.pokemonRoster.pokemonRoster.Count == 6){
			throwCoroutineStarted = true;
			StartCoroutine(throwPokeBall.PokemonGo(thisPlayer.pokemonRoster.pokemonRoster[5]));
		}
	}
}
