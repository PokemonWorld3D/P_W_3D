using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIHealthBar : MonoBehaviour
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
		cur_hp = (float)this_pokemon.cur_hp;
		cur_max_hp = (float)this_pokemon.cur_max_hp;
		if(this_pokemon.trainers_name != string.Empty)
		{
			pokemon_name.text = this_pokemon.trainers_name.ToString().ToUpper();
		}
		else
		{
			pokemon_name.text = this_pokemon.pokemon_name.ToString().ToUpper();
		}
	}

	void Update()
	{
		cur_hp = (float)this_pokemon.cur_hp;
		cur_max_hp = (float)this_pokemon.cur_max_hp;
		canvas.transform.LookAt(target);
		hp.fillAmount = cur_hp / cur_max_hp;
	}
}
