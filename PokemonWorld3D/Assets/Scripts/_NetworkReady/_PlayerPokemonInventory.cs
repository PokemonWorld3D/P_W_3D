using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("PokemonInventory")]
public class _PlayerPokemonInventory
{
	[XmlArray("PInventory")]
	[XmlArrayItem("Pokemon")]
	public List<_PlayerPokemonData> pokemonInventory = new List<_PlayerPokemonData>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(_PlayerPokemonInventory));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static _PlayerPokemonInventory Load(string path)
	{
		var serializer = new XmlSerializer(typeof(_PlayerPokemonInventory));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as _PlayerPokemonInventory;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static _PlayerPokemonInventory LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(_PlayerPokemonInventory));
		return serializer.Deserialize(new StringReader(text)) as _PlayerPokemonInventory;
	}
}
