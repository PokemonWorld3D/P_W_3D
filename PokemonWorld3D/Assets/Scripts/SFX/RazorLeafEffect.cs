using UnityEngine;
using System.Collections;

public class RazorLeafEffect : MonoBehaviour
{
	public GameObject target;
	private float lifeTime = 5.0f;
	private float timer = 0.0f;
	
	void Update()
	{
		transform.Rotate(0, 1000, 0);
		timer += Time.deltaTime;
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
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
