using UnityEngine;
using System.Collections;

public class EmberEffect : MonoBehaviour
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
			PhotonNetwork.Instantiate("Small_Explosion", target.transform.position, Quaternion.identity, 0);
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
