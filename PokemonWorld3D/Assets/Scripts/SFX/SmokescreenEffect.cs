using UnityEngine;
using System.Collections;

public class SmokescreenEffect : MonoBehaviour
{
	private ParticleSystem smoke;
	private ParticleSystem.Particle[] particles;
	private int particlesRemaining = 100;

	void Start()
	{
		smoke = GetComponent<ParticleSystem>();
	}
	void Update()
	{
		if(smoke.isStopped)
		{
			if(particlesRemaining == 0)
			{
				PhotonNetwork.Destroy(gameObject);
			}
			particlesRemaining = smoke.particleCount;
		}
	}
}
