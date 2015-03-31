using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("PokemonRoster")]
public class PlayerPokemonRoster
{
	[XmlArray("Roster")]
	[XmlArrayItem("Pokemon")]
	public List<PlayerPokemonData> pokemonRoster = new List<PlayerPokemonData>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(PlayerPokemonRoster));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static PlayerPokemonRoster Load(string path)
	{
		var serializer = new XmlSerializer(typeof(PlayerPokemonRoster));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as PlayerPokemonRoster;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static PlayerPokemonRoster LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(PlayerPokemonRoster));
		return serializer.Deserialize(new StringReader(text)) as PlayerPokemonRoster;
	}
}
