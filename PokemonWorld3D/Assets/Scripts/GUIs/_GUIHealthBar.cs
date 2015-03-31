using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _GUIHealthBar : MonoBehaviour
{
	public Canvas canvas;
	public Transform target;
	public Image hp;
	public Text pokemon_name;
	public Pokemon this_pokemon;
	public float cur_hp;
	public float cur_max_hp;

	void Start()
	{
		target = Camera.main.transform;
		this_pokemon = GetComponentInParent<Pokemon>();
		cur_hp = (float)this_pokemon.curHP;
		cur_max_hp = (float)this_pokemon.curMaxHP;
		if(this_pokemon.isCaptured)
		{
			if(this_pokemon.nickName != "")
			{
				pokemon_name.text = this_pokemon.trainersName.ToString().ToUpper() + "\n" + this_pokemon.nickName.ToString();
			}
			else
			{
				pokemon_name.text = this_pokemon.trainersName.ToString().ToUpper() + "\n" + this_pokemon.pokemonName.ToString();
			}
		}
		else
		{
			pokemon_name.text = this_pokemon.pokemonName.ToString().ToUpper();
		}
	}

	void Update()
	{
		cur_hp = (float)this_pokemon.curHP;
		cur_max_hp = (float)this_pokemon.curMaxHP;
		canvas.transform.LookAt(target);
		hp.fillAmount = cur_hp / cur_max_hp;
	}
}
