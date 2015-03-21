using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
[XmlRoot("PokemonRoster")]
public class _PlayerPokemonRoster
{
	[XmlArray("Roster")]
	[XmlArrayItem("Pokemon")]
	public List<_PlayerPokemonData> pokemonRoster = new List<_PlayerPokemonData>();

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(_PlayerPokemonRoster));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static _PlayerPokemonRoster Load(string path)
	{
		var serializer = new XmlSerializer(typeof(_PlayerPokemonRoster));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as _PlayerPokemonRoster;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static _PlayerPokemonRoster LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(_PlayerPokemonRoster));
		return serializer.Deserialize(new StringReader(text)) as _PlayerPokemonRoster;
	}
}
