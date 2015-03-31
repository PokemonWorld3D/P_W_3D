using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReSpawner : MonoBehaviour 
{
	public float spawnDistance = 50.0f;
	public float minSpawnDensity = 6f;
	public float minSpawnDistance = 20f;
	public int respawnDelay = 10;
	public static List<Pokemon> deadPokemon = new List<Pokemon>();

	private Vector3 spawnPoint;
	private Vector3 lastSpawnPoint = Vector3.zero;

	void Update()
	{
		for(var i = 0; i < deadPokemon.Count; i++)
		{
			Pokemon pokemon = deadPokemon[i];
			
			float time = Time.time - pokemon.timeOfDeath;
			
			if(time > respawnDelay)
			{
				pokemon.isAlive = true;
				pokemon.gameObject.rigidbody.WakeUp();
				pokemon.gameObject.GetComponent<Animator>().enabled = true;
				pokemon.gameObject.GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Init;
				spawnPoint = new Vector3(Random.Range(0, 2000), Random.Range(0, 2000), Random.Range(0, 2000));
				spawnPoint.y = TerrainHeight(spawnPoint);
				if(!IsInvalidSpawnPoint(spawnPoint, lastSpawnPoint))
				{
					NavMeshHit closestHit;
					if(NavMesh.SamplePosition(spawnPoint, out closestHit, 500, 1))
					{
						spawnPoint = closestHit.position;
					}
					else
					{
						Debug.Log("...");
					}
					pokemon.gameObject.transform.position = spawnPoint;
					pokemon.SetupPokemonFirstTime();
					pokemon.gameObject.SetActive(true);
					deadPokemon.RemoveAt(i);
					i--;
				}
			}
		}
	}

	private bool IsInvalidSpawnPoint(Vector3 spawnPoint,Vector3 lastSpawnPoint)
	{
		if(spawnPoint.y == Mathf.Infinity || (spawnPoint - lastSpawnPoint).magnitude <= minSpawnDensity)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	private float TerrainHeight(Vector3 spawnPoint)
	{
		Ray rayUp = new Ray(spawnPoint, Vector3.up);
		Ray rayDown = new Ray(spawnPoint, Vector3.down);
		RaycastHit hitPoint;
		if(Physics.Raycast(rayUp, out hitPoint, Mathf.Infinity))
		{
			return hitPoint.point.y;
		}
		else if(Physics.Raycast(rayDown, out hitPoint, Mathf.Infinity))
		{
			return hitPoint.point.y;
		}
		else
		{
			return Mathf.Infinity;
		}
	}
}
