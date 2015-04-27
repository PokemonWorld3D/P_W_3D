using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WildPokemonSpawner : MonoBehaviour
{
	public string pokemonToSpawn;
	public float spawnDistance = 50.0f;
	public int spawnThisMany = 10;
	public int respawnDelay = 10;
	public int minLevel;
	public int maxLevel;
	public int maxSpawnRange;
	
	private Vector3 spawnPoint;
	private Vector3 lastSpawnPoint = Vector3.zero;
	
	void OnJoinedRoom()
	{
		if(PhotonNetwork.isMasterClient)
			SpawnPokemon(pokemonToSpawn, spawnThisMany);
	}
	
	private void SpawnPokemon(string pokemon, int numberOfPokemon)
	{
		for(int pokemonCount = 0; pokemonCount < numberOfPokemon; pokemonCount++){
			spawnPoint = new Vector3(Random.Range(0, maxSpawnRange), Random.Range(0, maxSpawnRange), Random.Range(0, maxSpawnRange));
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
				Quaternion wayToFace = Quaternion.Euler(0, Random.Range(0, 360), 0);
				GameObject wildPokemon = PhotonNetwork.Instantiate(pokemon, spawnPoint, wayToFace, 0) as GameObject;
				wildPokemon.GetComponent<Pokemon>().level = Random.Range(minLevel, maxLevel);
				wildPokemon.GetComponent<WildPokemonAI>().enabled = true;
				lastSpawnPoint = spawnPoint;
			}
		}
	}
	private bool IsInvalidSpawnPoint(Vector3 spawnPoint,Vector3 lastSpawnPoint)
	{
		if(spawnPoint.y == Mathf.Infinity)
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
