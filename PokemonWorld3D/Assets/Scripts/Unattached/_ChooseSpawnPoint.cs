using UnityEngine;
using System.Collections;

public class _ChooseSpawnPoint : MonoBehaviour {

	public static Vector3 PickSpawnPoint(string currentZone, string lastZone){
		if(currentZone == "ViridianForest"){
			return ViridianForest(lastZone);
		}else if(currentZone == "PalletTown"){
			return PalletTown(lastZone);
		}else if(currentZone == "PreAlpha"){
			return PreAlpha(lastZone);
		}else{
			return new Vector3(0, 0, 0);
		}
	}

	public static Vector3 PreAlpha(string lastZone){
		Vector3 spawnPoint = new Vector3(125, 10000, 120);
		spawnPoint.y = TerrainHeight(spawnPoint);
		return spawnPoint;
	}

	public static Vector3 PalletTown(string lastZone){
		Vector3 spawnPoint = new Vector3(125, 10000, 200);
		spawnPoint.y = TerrainHeight(spawnPoint);
		return spawnPoint;
	}

	public static Vector3 ViridianForest(string lastZone){
		Vector3 spawnPoint1 = new Vector3(1160, 10000, 28);
		Vector3 spawnPoint2 = new Vector3(322, 10000, 1963);
		Vector3 spawnPoint;
		if(lastZone == "PalletTown"){
			spawnPoint = spawnPoint1;
			spawnPoint.y = TerrainHeight(spawnPoint);
			return spawnPoint;
		}else if(lastZone == "ViridianCity"){
			spawnPoint = spawnPoint2;
			spawnPoint.y = TerrainHeight(spawnPoint);
			return spawnPoint;
		}else{
			spawnPoint = spawnPoint1;
			spawnPoint.y = TerrainHeight(spawnPoint);
			return spawnPoint;
		}
	}

	private static bool IsInvalidSpawnPoint(Vector3 spawnPoint){
		if(spawnPoint.y == Mathf.Infinity){
			return true;
		}else{
			return false;
		}
	}

	private static float TerrainHeight(Vector3 spawnPoint){
		Ray rayUp = new Ray(spawnPoint, Vector3.up);
		Ray rayDown = new Ray(spawnPoint, Vector3.down);
		RaycastHit hitPoint;
		if(Physics.Raycast(rayUp, out hitPoint, Mathf.Infinity)){
			return hitPoint.point.y;
		}
		else if(Physics.Raycast(rayDown, out hitPoint, Mathf.Infinity)){
			return hitPoint.point.y;
		}else{
			return Mathf.Infinity;
		}
	}
}
