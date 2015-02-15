using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonInput : MonoBehaviour
{
	public NEWPokemon this_pokemon;
	public float speed = 10.0f;
	public float rotate_speed = 10.0f;
	public float gravity = 10.0f;
	public float max_velocity_change = 10.0f;
	public bool can_jump = true;
	public float jump_height = 2.0f;
	public bool grounded = false;
	public float land_speed;

	public GameObject target;
	public List<GameObject> targets;
	public Transform my_transform;

	private Collider terrain_collider;
	private Flight flight;
	private Animator anim;
	private float horizontal;
	private float vertical;
	private float rotation;
	private float terrain_height;
	private Vector3 spot_over_ground;
	private Vector3 velocity;
	
	void Awake()
	{
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	void Start()
	{
		targets = new List<GameObject>();
		target = null;
		my_transform = transform;
		flight = GetComponent<Flight>();
		terrain_collider = GameObject.FindGameObjectWithTag("Terrain").GetComponent<TerrainCollider>();
		anim = GetComponent<Animator>();
		velocity = rigidbody.velocity;
		AddAllTargets();
	}
	void Update()
	{
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		rotation = Input.GetAxis("Rotation");
		velocity = rigidbody.velocity;
		terrain_height = Terrain.activeTerrain.SampleHeight (my_transform.position);
		spot_over_ground = new Vector3(my_transform.position.x, terrain_height, my_transform.transform.position.z);
		float altitude = Vector3.Distance(my_transform.position, spot_over_ground);
		if(Input.GetKeyUp(KeyCode.Tab))
		{
			TargetPokemon();
		}
		if(Input.GetButtonDown("TakeFlight") && grounded)
		{
			flight.enabled = true;
			StartCoroutine(flight.TakeOff());
		}
		if(Input.GetKey(KeyCode.LeftShift))
		{
			speed = 1.0f;

		}
		else
		{
			speed = 10.0f;
		}
		if (can_jump && Input.GetButtonDown("Jump"))
		{
			Jump();
		}
		if(Input.GetKeyDown(KeyCode.LeftControl))
		{
			SwapToPlayer();
		}
		if(altitude > 0.5f)
		{
			anim.SetBool("Falling", true);
		}
		else
		{
			anim.SetBool("Falling", false);
		}
	}
	void FixedUpdate ()
	{
		if (grounded)
		{
			// Calculates the slope of the ground beneath the object and matches the "lean" of the object to the angle of the slope.
			RaycastHit hit;
			Ray ray = new Ray(my_transform.position, Vector3.down);
			if (terrain_collider.Raycast(ray, out hit, 1000.0f))
			{
				my_transform.rotation = Quaternion.FromToRotation(my_transform.up, hit.normal) * my_transform.rotation;
			}

			// Calculate how fast we should be moving
			Vector3 target_velocity = new Vector3(horizontal, 0, vertical);
			target_velocity = transform.TransformDirection(target_velocity);
			target_velocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity_change = (target_velocity - velocity);
			velocity_change.x = Mathf.Clamp(velocity_change.x, -max_velocity_change, max_velocity_change);
			velocity_change.z = Mathf.Clamp(velocity_change.z, -max_velocity_change, max_velocity_change);
			velocity_change.y = 0;
			rigidbody.AddForce(velocity_change, ForceMode.VelocityChange);
			// Rotates on the Y axis.
			transform.Rotate(0, rotation * rotate_speed, 0);

			land_speed = vertical * speed;
			anim.SetFloat("LandSpeed", land_speed);
		}

		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	void OnCollisionStay(Collision col)
	{
		grounded = true;
	}
	
	private float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
		return Mathf.Sqrt(2 * jump_height * gravity);
	}
	private void Jump()
	{
		anim.SetTrigger("Jump");
		rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
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
		}
	}
	/*public int SortByDistance(GameObject go)
	{
		return Vector3.Distance(this.transform.position, my_transform.position).CompareTo(Vector3.Distance(go.transform.position, my_transform.position));
	}*/
}