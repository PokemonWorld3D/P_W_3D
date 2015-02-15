using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayersInventory : MonoBehaviour {

	public List<PokeBall> playersPokeBalls = new List<PokeBall>();
	public List<Medicine> playersMedicines = new List<Medicine>();

}
