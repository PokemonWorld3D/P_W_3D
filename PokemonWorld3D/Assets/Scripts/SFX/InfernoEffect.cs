using UnityEngine;
using System.Collections;

public class InfernoEffect : MonoBehaviour
{
	private ParticleSystem fire;
	private ParticleSystem.Particle[] particles;
	private int particlesRemaining = 100;
	
	void Start()
	{
		fire = GetComponent<ParticleSystem>();
	}
	void Update()
	{
		if(fire.isStopped)
		{
			if(particlesRemaining == 0)
			{
				PhotonNetwork.Destroy(gameObject);
			}
			particlesRemaining = fire.particleCount;
		}
	}
}
