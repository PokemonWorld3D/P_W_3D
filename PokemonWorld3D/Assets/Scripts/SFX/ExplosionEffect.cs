using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour
{
	private ParticleSystem explosion;
	private ParticleSystem.Particle[] particles;
	private int particlesRemaining = 100;
	
	void Start()
	{
		explosion = GetComponent<ParticleSystem>();
	}
	void Update()
	{
		if(explosion.isStopped)
		{
			if(particlesRemaining == 0)
			{
				PhotonNetwork.Destroy(gameObject);
			}
			particlesRemaining = explosion.particleCount;
		}
	}
}
