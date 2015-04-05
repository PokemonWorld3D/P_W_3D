using UnityEngine;
using System.Collections;

public class FireSpinEffect : MonoBehaviour
{
	private float lifeTime = 5.0f;
	private float timer = 0.0f;

	void Update()
	{
		timer += Time.deltaTime;
		if(timer >= lifeTime)
			PhotonNetwork.Destroy(gameObject);
	}
}
