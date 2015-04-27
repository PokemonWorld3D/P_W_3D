using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttraction : MonoBehaviour
{
    private ParticleSystem system;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
    public Transform target;

    void Update()
    {
        if (system == null) system = GetComponent<ParticleSystem>();

        var count = system.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            var particle = particles[i];

            float distance = Vector3.Distance(target.position, particle.position);

            if (distance > 0.1f)
            {
                particle.position = Vector3.Lerp(particle.position, target.position, Time.deltaTime * distance);

                particles[i] = particle;
            }
        }

        system.SetParticles(particles, count);
    }
}