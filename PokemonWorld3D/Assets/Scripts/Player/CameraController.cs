using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform target;							//Target to follow
	public float target_height = 1.7f;					//Vertical offset adjustment
	public float distance = 12.0f;						//Default distance
	public float offset_from_wall = 0.1f;				// Bring camera away from any colliding objects
	public float max_distance = 20.0f;					// Maximum zoom Distance
	public float min_distance = 0.6f;					// Minimum zoom Distance
	public float x_speed = 200.0f;						// Orbit speed (Left/Right)
	public float y_speed = 200.0f;						// Orbit speed (Up/Down)
	public float y_min_limit = -80.0f;					// Looking up limit
	public float y_max_limit = 80.0f;					// Looking down limit
	public float zoom_rate = 40.0f;						// Zoom Speed
	public float rotation_dampening = 3.0f;				// Auto Rotation speed (higher = faster)
	public float zoom_dampening = 5.0f;					// Auto Zoom speed (Higher = faster)
	public LayerMask collision_layers = -1;				// What the camera will collide with
	public bool lock_to_rear_of_target = false;			// Lock camera to rear of target
	public bool allow_mouse_input_x = true;				// Allow player to control camera angle on the X axis (Left/Right)
	public bool allow_mouse_input_y = true;				// Allow player to control camera angle on the Y axis (Up/Down)
	
	private float x_deg = 0.0f;
	private float y_deg = 0.0f;
	private float current_distance;
	private float desired_distance;
	private float corrected_distance;
	private bool rotate_behind = false;
	
	public float height;
	private Renderer target_renderer;

	void Start()
	{
		target_renderer = target.GetComponentInChildren<SkinnedMeshRenderer>();
		target_height =  target_renderer.bounds.extents.y;
		if(target_height < 1.0f)
			target_height = 1.0f;
		Vector3 angles = transform.eulerAngles;
		x_deg = angles.x;
		y_deg = angles.y;
		current_distance = distance;
		desired_distance = distance;
		corrected_distance = distance;
		
		if (lock_to_rear_of_target)
			rotate_behind = true;
	}
	void LateUpdate()
	{
		Vector3 v_target_offset;
			
			
		// If either mouse buttons are down, let the mouse govern camera position
		if (GUIUtility.hotControl == 0)
		{
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
			{
				//Check to see if mouse input is allowed on the axis
				if (allow_mouse_input_x)
					x_deg += Input.GetAxis ("Mouse X") * x_speed * 0.02f;
				else
				{
					RotateBehindTarget();
				}
				if (allow_mouse_input_y)
					y_deg -= Input.GetAxis ("Mouse Y") * y_speed * 0.02f;
				
				//Interrupt rotating behind if mouse wants to control rotation
				if (!lock_to_rear_of_target)
					rotate_behind = false;
			}
			
			// otherwise, ease behind the target if any of the directional keys are pressed
			else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || rotate_behind)
			{
				//RotateBehindTarget();
			}
		}
		y_deg = ClampAngle (y_deg, y_min_limit, y_max_limit);
		
		// Set camera rotation
		Quaternion rotation = Quaternion.Euler (y_deg, x_deg, 0.0f);
		
		// Calculate the desired distance
		desired_distance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoom_rate * Mathf.Abs (desired_distance);
		desired_distance = Mathf.Clamp (desired_distance, min_distance, max_distance);
		corrected_distance = desired_distance;
		
		// Calculate desired camera position
		v_target_offset = new Vector3(0.0f, -target_height, 0.0f);
		Vector3 position = target.position - (rotation * Vector3.forward * desired_distance + v_target_offset);
		
		// Check for collision using the true target's desired registration point as set by user using height
		RaycastHit collision_hit;
		Vector3 true_target_position = new Vector3(target.position.x, target.position.y + target_height, target.position.z);
		
		// If there was a collision, correct the camera position and calculate the corrected distance
		bool is_corrected = false;
		if (Physics.Linecast (true_target_position, position, out collision_hit, collision_layers))
		{
			// Calculate the distance from the original estimated position to the collision location,
			// subtracting out a safety "offset" distance from the object we hit.  The offset will help
			// keep the camera from being right on top of the surface we hit, which usually shows up as
			// the surface geometry getting partially clipped by the camera's front clipping plane.
			corrected_distance = Vector3.Distance(true_target_position, collision_hit.point) - offset_from_wall;
			is_corrected = true;
		}
		
		// For smoothing, lerp distance only if either distance wasn't corrected, or correctedDistance is more than currentDistance
		current_distance = !is_corrected || corrected_distance > current_distance ? Mathf.Lerp (current_distance, corrected_distance, Time.deltaTime * zoom_dampening) : corrected_distance;
		
		// Keep within limits
		current_distance = Mathf.Clamp (current_distance, min_distance, max_distance);
		
		// Recalculate position based on the new currentDistance
		position = target.position - (rotation * Vector3.forward * current_distance + v_target_offset);
		
		//Finally Set rotation and position of camera
		transform.rotation = rotation;
		transform.position = position;
	}

	[RPC]
	public void SetTarget(Transform new_target)
	{
		target = new_target;
		target_renderer = target.GetComponentInChildren<SkinnedMeshRenderer>();
		target_height =  target_renderer.bounds.extents.y;
		if(target_height < 1.0f)
			target_height = 1.0f;
	}

	[RPC]
	private void RotateBehindTarget()
	{
		float target_rotation_angle = target.eulerAngles.y;
		float current_rotation_angle = transform.eulerAngles.y;
		x_deg = Mathf.LerpAngle (current_rotation_angle, target_rotation_angle, rotation_dampening * Time.deltaTime);
		
		// Stop rotating behind if not completed
		if (target_rotation_angle == current_rotation_angle)
		{
			if (!lock_to_rear_of_target)
				rotate_behind = false;
		}
		else
			rotate_behind = true;
	}
	[RPC]
	private void MoveCam(Vector3 cam_pos, Quaternion cam_rot){
		transform.position = cam_pos;
		transform.rotation = cam_rot;
		networkView.RPC("UpdateCam", RPCMode.Server, transform.position, transform.rotation);
	}
	[RPC]
	private void UpdateCam(Vector3 cam_pos, Quaternion cam_rot){
		transform.position = cam_pos;
		transform.rotation = cam_rot;
	}

	private static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
