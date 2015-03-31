using UnityEngine;
using System.Collections;

public class _Flight : MonoBehaviour
{
	private Transform my_transform;
	public Pokemon this_pokemon;
	public float bias = 0.96f;
	public float speed;
	public float minimum_speed = 5.0f;
	public float max_speed = 100.0f;
	public float accel_dampen = 5.0f;
	public float decel_dampen = 5f;
	private float terrain_height;
	public bool in_flight;
	public float in_flight_distance_behind = 10.0f;
	public float in_flight_height_above = 10.0f;
	public float take_off_power = 10.0f;
	public float take_off_radius = 1.0f;
	public float pitch;
	public float roll;
	public float yaw;
	public Transform mount_position;
	public GameObject mount_spot;
	public bool taking_off;
	public bool falling;
	public bool flying;
	public bool gliding;
	
	private Vector3 spot_over_ground;
//	private Collider terrain;
	private PokemonInput pokemon_input;

	void Start()
	{
		my_transform = transform;
		in_flight = false;
//		terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<TerrainCollider>();
		pokemon_input = GetComponent<PokemonInput>();
	}

	void Update()
	{
		if(in_flight)
		{
			if(!flying && !gliding)
			{
				Idle();
			}
			if(Input.GetMouseButton(0))
			{
				flying = true;
				Fly();
				speed = Mathf.Lerp(speed, max_speed, Time.deltaTime * accel_dampen);
			}
			else
			{
				flying = false;
				speed = Mathf.Lerp(speed, 0.0f, Time.deltaTime * decel_dampen);
			}
			if(!flying && speed > minimum_speed)
			{
				gliding = true;
				Glide();
			}
			else
			{
				gliding = false;
			}
			pitch = Input.GetAxis("Pitch");
			roll = Input.GetAxis("Roll");
			yaw = Input.GetAxis("Yaw");
			speed -= my_transform.forward.y * Time.deltaTime * 50.0f;
			if (speed < 2.0f)
					speed = 0.0f;
			terrain_height = Terrain.activeTerrain.SampleHeight (my_transform.position);
			spot_over_ground = new Vector3(my_transform.position.x, terrain_height, my_transform.transform.position.z);
			if(Input.GetKeyDown(KeyCode.Space))
				StartCoroutine(Landing());
		}
	}
	void FixedUpdate()
	{
		my_transform.position += my_transform.forward * Time.deltaTime * speed;
		my_transform.Rotate (0.0f, -yaw, 0.0f);
		if (speed > 10.0f)
			my_transform.Rotate (-pitch, -yaw, -roll);
		if(pitch == 0.0f && roll == 0.0f && yaw == 0.0f)
		{
			Vector3 flatFwd = new Vector3(my_transform.forward.x, 0, my_transform.forward.z);
			Quaternion fwdRotation = Quaternion.LookRotation(flatFwd, Vector3.up);
			my_transform.rotation = Quaternion.Slerp(my_transform.rotation, fwdRotation, Time.deltaTime * 2.0f);
		}
	}

	private void Idle()
	{
		animation.CrossFade("Idle_Air");
	}
	private void LiftOff()
	{
		taking_off = true;
		animation.CrossFade("Lift_Off");
	}
	private void Fly()
	{
		animation.CrossFade("Flying");
	}
	private void Glide()
	{
		animation.CrossFade("Glide");
	}
	private void Fall()
	{
		falling = true;
		animation.CrossFade("Falling");
	}
	private void Land()
	{
		falling = false;
		animation.CrossFade("Landing");
	}
	public void TakeOff()
	{
		rigidbody.AddForce(Vector3.up * take_off_power);
		//rigidbody.AddExplosionForce(take_off_power, my_transform.position, take_off_radius);
	}

	public IEnumerator Mount()
	{
		/*while(Vector3.Distance(this_pokemon.trainer.transform.position, mount_position.position) > 0.1f)
		{
			this_pokemon.trainer.transform.position = Vector3.Lerp(this_pokemon.trainer.transform.position, mount_position.position, Time.deltaTime * 5.0f);
			yield return null;
		}*/
		this_pokemon.trainer.rigidbody.Sleep();
		this_pokemon.trainer.collider.enabled = false;
		this_pokemon.trainer.transform.position = mount_position.position;
		this_pokemon.trainer.transform.rotation = mount_position.rotation;
		this_pokemon.trainer.GetComponent<Animator>().SetTrigger("Mount");
		yield return new WaitForSeconds(1.0f);
		this_pokemon.trainer.transform.parent = mount_spot.transform;
		pokemon_input.enabled = false;
		rigidbody.useGravity = false;
		LiftOff();
	}
	public IEnumerator TakingOff()
	{
		in_flight = true;
		//rigidbody.Sleep();
		Camera.main.GetComponent<CameraController>().enabled = false;
		Camera.main.transform.parent = my_transform;
		Vector3 cam_pos = new Vector3(0.0f, 3.0f, -5.0f);
		Camera.main.transform.localPosition = cam_pos;
		Camera.main.transform.LookAt(my_transform.position + my_transform.forward * 5.0f);
		yield return null;
	}
	private IEnumerator Landing()
	{
		in_flight = false;
		Fall();
		rigidbody.useGravity = true;
		Camera.main.transform.parent = null;
		Camera.main.GetComponent<CameraController>().enabled = true;
		while(my_transform.rotation != Quaternion.identity)
		{
			my_transform.rotation = Quaternion.Slerp(my_transform.rotation, Quaternion.identity, Time.deltaTime * 5.0f);
			yield return null;
		}
		//Vector3 spot_over_ground = new Vector3(my_transform.position.x, terrain_height, my_transform.transform.position.z);
		//my_transform.position = Vector3.Lerp(my_transform.position, spot_over_ground, Time.deltaTime * 5.0f);
		while(Vector3.Distance(spot_over_ground, my_transform.position) > 0.1f)
		{
			my_transform.position = Vector3.Lerp(my_transform.position, spot_over_ground, Time.deltaTime * 5.0f);
			yield return null;
		}
		rigidbody.WakeUp();
		yield return new WaitForSeconds(0.5f);
		this_pokemon.trainer.GetComponent<Animator>().SetTrigger("Dismount");
		yield return new WaitForSeconds(1.0f);
		this_pokemon.trainer.transform.parent = null;
		this_pokemon.trainer.transform.position = mount_position.position;
		this_pokemon.trainer.transform.rotation = mount_position.rotation;
		this_pokemon.trainer.collider.enabled = true;
		this_pokemon.trainer.rigidbody.WakeUp();
		pokemon_input.enabled = true;
		this.enabled = false;
		yield return null;
	}
}
