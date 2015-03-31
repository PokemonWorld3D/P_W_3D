using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("PokemonInventory")]
public class PlayerPokemonInventory
{
	[XmlArray("PInventory")]
	[XmlArrayItem("Pokemon")]
	public List<PlayerPokemonData> pokemonInventory = new List<PlayerPokemonData>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(PlayerPokemonInventory));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static PlayerPokemonInventory Load(string path)
	{
		var serializer = new XmlSerializer(typeof(PlayerPokemonInventory));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as PlayerPokemonInventory;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static PlayerPokemonInventory LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(PlayerPokemonInventory));
		return serializer.Deserialize(new StringReader(text)) as PlayerPokemonInventory;
	}
}
