using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WildPokemonAI : MonoBehaviour
{
	public List<HateHolder> Hate_List;
	public Transform target;

	private Transform my_transform;

	void Start()
	{
		Hate_List = new List<HateHolder>();
		my_transform = transform;
	}
	void Update()
	{
		if(target)
		{
			Vector3 dir = (target.position - my_transform.position).normalized;
//			float direction = Vector3.Dot(dir, my_transform);

//			if(direction > 0.9f)
//			{
				//Target is in front of us, so move forward.
//			}
		}
	}

	public void IncreaseHate(GameObject pokemon, Pokemon this_pokemon, int hate_increase)
	{
		for(int i = 0; i < Hate_List.Count; i++)
		{
			if(Hate_List[i].pokemon = pokemon)
			{
				Hate_List[i].amount_of_hate += hate_increase;
				return;
			}
		}
		Hate_List.Add(new HateHolder(pokemon, this_pokemon, hate_increase));
		if(Hate_List.Count >= 1)
		{
			Hate_List.Sort(delegate(HateHolder x, HateHolder y) { return x.amount_of_hate.CompareTo(y.amount_of_hate); });
			target = Hate_List[0].pokemon.transform;
		}
	}
}
