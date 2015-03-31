using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class _ItemDatabase : MonoBehaviour {

	public List<_Item> items = new List<_Item>();
	public List<_Medicine> medicines = new List<_Medicine>();
	public List<_PokeBall> pokeballs = new List<_PokeBall>();

}
