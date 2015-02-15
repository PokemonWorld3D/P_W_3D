using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item>();
	public List<Medicine> medicines = new List<Medicine>();
	public List<PokeBall> pokeballs = new List<PokeBall>();

}
