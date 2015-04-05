using UnityEngine;
using System.Collections;

public class DragonRageEffect : MonoBehaviour
{
	public GameObject target;
	public float scale = 0.0f;
	public float scaleSpeed = 2.0f;

	private float lifeTime = 10.0f;
	private float timer = 0.0f;
	
	void Update()
	{
		timer += Time.deltaTime;
		if(scale != 0.0f)
		{
			Vector3 desiredScale = new Vector3(scale, scale, scale);
			float current = transform.localScale.sqrMagnitude;
			float target = desiredScale.sqrMagnitude;
			if(current < target)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, timer / scaleSpeed);
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
