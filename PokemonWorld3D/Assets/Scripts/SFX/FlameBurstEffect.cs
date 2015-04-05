using UnityEngine;
using System.Collections;

public class FlameBurstEffect : MonoBehaviour
{
	public GameObject target;
	private float lifeTime = 5.0f;
	private float timer = 0.0f;
	
	void Update()
	{
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
			PhotonNetwork.Instantiate("Large_Explosion", target.transform.position, target.transform.rotation, 0);
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
