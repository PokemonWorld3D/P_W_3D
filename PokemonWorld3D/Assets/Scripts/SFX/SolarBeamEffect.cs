using UnityEngine;
using System.Collections;

public class SolarBeamEffect : MonoBehaviour
{
	public GameObject target;
	public Vector3 scale = Vector3.zero;
	public float scaleSpeed = 2.0f;
	
	private float lifeTime = 10.0f;
	private float timer = 0.0f;
	
	void Update()
	{
		timer += Time.deltaTime;
		if(scale != Vector3.zero)
		{
			float current = transform.localScale.sqrMagnitude;
			float target = scale.sqrMagnitude;
			if(current < target)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, scale, timer / scaleSpeed);
			}
		}
		if(timer >= lifeTime)
			PhotonNetwork.Destroy(gameObject);
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject != target)
		{
			return;
		}
		else
		{
			//-------------Instantiate the explosion here.---------------------------------//
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
