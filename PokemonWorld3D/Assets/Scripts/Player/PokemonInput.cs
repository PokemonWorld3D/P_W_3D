using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonInput : MonoBehaviour
{
	public float pokemon_speed;
	public float run_multiplier = 2.0f;
	public float gravity = 20.0f;
	public float fall_time = 0.5f;
	public float jump_height;
	public float jump_time = 1.5f;
	public Pokemon this_pokemon;
	public GameObject target;
	public Pokemon target_pokemon;
	public List<GameObject> targets;

	private Transform my_transform;
	private CharacterController controller;
	private Vector3 move_direction;
	private CollisionFlags collision_flags;
	private float air_time;
	public bool jumping;

	private FINALGUISCRIPT hud;

	public bool using_a_move;

	void Awake()
	{
		my_transform = transform;
		controller = GetComponent<CharacterController>();
 	}
	void Start()
	{
		this_pokemon = GetComponent<Pokemon>();
		move_direction = Vector3.zero;
		animation.Stop();
		targets = new List<GameObject>();
		target = null;
		hud = this_pokemon.trainer.GetComponent<PlayerCharacter>().players_hud;
	}
	void Update()
	{
		for(int i = 0; i < targets.Count; i++)
		{
			if(targets[i].GetComponent<Pokemon>().cur_hp == 0)
			{
				targets.Remove(targets[i]);
			}
		}
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPlayer();
		}
		if(Input.GetButtonUp("Targeting"))
		{
			AddAllTargets();
			TargetPokemon();
		}
		if(target_pokemon != null && target_pokemon.cur_hp == 0)
		{
			RemoveTarget(target);
		}
		Attack();
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0f, -forward.x);

		if(controller.isGrounded && !jumping && !using_a_move)
		{
			air_time = 0.0f;

			move_direction = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward);
			if (move_direction != Vector3.zero)
			{
				my_transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move_direction), 10f * Time.smoothDeltaTime);
				my_transform.eulerAngles = new Vector3(0f, my_transform.eulerAngles.y, 0f);
			}
			if (move_direction.sqrMagnitude > 1f)
			{
				move_direction = move_direction.normalized;
			}
			move_direction *= pokemon_speed;

			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
			{
				if(Input.GetButton("Walk"))
				{
					Walk();
				}
				else
				{
					move_direction *= run_multiplier;
					Run();
				}
			}
			else
			{
				IdleWorld();
			}

			if(Input.GetButton("Jump"))
			{
				if(air_time < jump_time)
				{
					Jump();
					jumping = true;
				}
			}
		}
		else
		{
			if((collision_flags & CollisionFlags.CollidedBelow) == 0)
			{
				air_time += Time.deltaTime;

				if(air_time > fall_time)
				{
					Fall();
				}
			}
		}

		move_direction.y -= gravity * Time.deltaTime;

		collision_flags = controller.Move(move_direction * Time.deltaTime);
	}

	public void ActualJump()
	{
		move_direction.y += jump_height;
	}
	public void NotJumping()
	{
		jumping = false;
	}
	public void UsingMove()
	{
		using_a_move = true;
	}
	public void NotUsingMove()
	{
		using_a_move = false;
	}

	private void IdleWorld()
	{
		animation.CrossFade("Idle_World");
	}
	private void IdleBattle()
	{
		animation.CrossFade("Idle_Battle");
	}
	private void Walk()
	{
		animation.CrossFade("Walk");
	}
	private void Run()
	{
		animation.CrossFade("Run");
	}
	private void Jump()
	{
		animation.CrossFade("Jump");
	}
	private void Fall()
	{
		animation.CrossFade("Falling");
	}
	private void SwapToPlayer()
	{
		if(!this_pokemon.is_in_battle)
		{
			//DISABLE THE PLAYER AI.
			//ENABLE THE POKEMON AI.
			this_pokemon.trainer.GetComponent<PlayerInput>().enabled = true;
			Camera.main.GetComponent<CameraController>().SetTarget(this_pokemon.trainer);
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
		if(target.gameObject != this.gameObject)
			targets.Add(target);
	}
	private void RemoveTarget(GameObject target)
	{
		targets.Remove(target);
		target = null;
		hud.NoTarget();
	}
	private void SortTargetsByDistance()
	{
		targets.Sort(delegate(GameObject c1, GameObject c2){
			return Vector3.Distance(this.transform.position, c1.transform.position).CompareTo
				((Vector3.Distance(this.transform.position, c2.transform.position)));   
		});
	}
	private void TargetPokemon()
	{
		if(target == null)
		{
			SortTargetsByDistance();
			target = targets[0];
			target_pokemon = target.GetComponent<Pokemon>();
			hud.SetTarget(target_pokemon);
		}
		else
		{
			int index = targets.IndexOf(target);
			
			if(index < targets.Count - 1)
			{
				index++;
			}
			else
			{
				index = 0;
			}
			target = targets[index];
			target_pokemon = targets[index].GetComponent<Pokemon>();
			hud.SetTarget(target_pokemon);
		}
	}
	private void Attack()
	{
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha1) && this_pokemon.known_moves.Count >= 1
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[0].range
		   && this_pokemon.known_moves[0].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[0].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[0].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha2) && this_pokemon.known_moves.Count >= 2
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[1].range
		   && this_pokemon.known_moves[1].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[1].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[1].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha3) && this_pokemon.known_moves.Count >= 3
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[2].range
		   && this_pokemon.known_moves[2].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[2].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[2].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha4) && this_pokemon.known_moves.Count >= 4
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[3].range
		   && this_pokemon.known_moves[3].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[3].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[3].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha5) && this_pokemon.known_moves.Count >= 5
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[4].range
		   && this_pokemon.known_moves[4].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[4].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[4].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha6) && this_pokemon.known_moves.Count >= 6
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[5].range
		   && this_pokemon.known_moves[5].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[5].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[5].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha7) && this_pokemon.known_moves.Count >= 7
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[6].range
		   && this_pokemon.known_moves[6].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[6].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[6].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha8) && this_pokemon.known_moves.Count >= 8
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[7].range
		   && this_pokemon.known_moves[7].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[7].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[7].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha9) && this_pokemon.known_moves.Count >= 9
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[8].range
		   && this_pokemon.known_moves[8].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[8].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[8].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && Input.GetKeyDown(KeyCode.Alpha0) && this_pokemon.known_moves.Count >= 10
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[9].range
		   && this_pokemon.known_moves[9].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[9].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[9].UseMove(gameObject, target, targets);
		}
	}
}
