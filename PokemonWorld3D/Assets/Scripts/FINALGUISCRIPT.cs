using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FINALGUISCRIPT : MonoBehaviour
{
	public GameObject owner;
	public Text chat_output;
	public InputField chat_input;
#region Pokemon Canvas
	public GameObject pokemon_canvas;
	public NEWPokemon active_pokemon;
	public Image hp_bar;
	private RectTransform hp_bar_transform;
	private float hp_bar_cached_y;
	private float min_hp_bar_x_value;
	private float max_hp_bar_x_value;
	public Image pp_bar;
	private RectTransform pp_bar_transform;
	private float pp_bar_cached_y;
	private float min_pp_bar_x_value;
	private float max_pp_bar_x_value;
	public Image avatar;
	public Text hit_points;
	public Text power_points;
	public NEWPokemon target;
	public GameObject target_pokemon_portrait;
	public Image target_hp_bar;
	private RectTransform target_hp_bar_transform;
	private float target_hp_bar_cached_y;
	private float target_min_hp_bar_x_value;
	private float target_max_hp_bar_x_value;
	public Image target_avatar;
	public GameObject party_panel;
	public Text[] party_member_lvl_name;
	public Image[] party_member_hp;
	public Image[] party_membe_pp;
	public Text[] party_member_hit_points;
	public Text[] party_member_power_points;
	public Image[] moves;
#endregion

	void Start()
	{
		//owner = transform.parent.gameObject;
#region Pokemon Canvas
		hp_bar_transform = hp_bar.rectTransform;
		hp_bar_cached_y = hp_bar_transform.localPosition.y;
		max_hp_bar_x_value = hp_bar_transform.localPosition.x;
		min_hp_bar_x_value = hp_bar_transform.localPosition.x - hp_bar_transform.rect.width;
		pp_bar_transform = pp_bar.rectTransform;
		pp_bar_cached_y = pp_bar_transform.localPosition.y;
		max_pp_bar_x_value = pp_bar_transform.localPosition.x;
		min_pp_bar_x_value = pp_bar_transform.localPosition.x - pp_bar_transform.rect.width;
		target_hp_bar_transform = target_hp_bar.rectTransform;
		target_hp_bar_cached_y = target_hp_bar_transform.localPosition.y;
		target_max_hp_bar_x_value = target_hp_bar_transform.localPosition.x;
		target_min_hp_bar_x_value = target_hp_bar_transform.localPosition.x - target_hp_bar_transform.rect.width;
#endregion
	}
	void Update()
	{
		if(pokemon_canvas.activeInHierarchy)
		{
			Debug.Log("Pokemon canvas is active.");
			HandlePlayerPokemonGUI();
		}
		else
		{
			Debug.Log("Pokemon canvas is NOT active.");
		}
		if(target_pokemon_portrait.activeInHierarchy == true)
			HandleTargetGUI();
	}

	public void SetActivePokemon(NEWPokemon players_active_pokemon)
	{
		active_pokemon = players_active_pokemon;
		pokemon_canvas.SetActive(true);
	}
	public void RemoveActivePokemon()
	{
		active_pokemon = null;
		pokemon_canvas.SetActive(false);
	}
	public void SetTarget(NEWPokemon players_target)
	{
		target = players_target;
		target_pokemon_portrait.SetActive(true);
	}
	public void NoTarget()
	{
		target = null;
		target_pokemon_portrait.SetActive(false);
	}
	public void HandlePlayerPokemonGUI()
	{
		int current_hp = active_pokemon.cur_hp;
		int current_max_hp = active_pokemon.cur_max_hp;
		avatar.sprite = active_pokemon.avatar;
		hit_points.text = "HP : " + current_hp + " / " + current_max_hp;
		int current_pp = active_pokemon.cur_pp;
		int current_max_pp = active_pokemon.cur_max_pp;
		power_points.text = "PP : " + active_pokemon.cur_pp + " / " + active_pokemon.cur_max_pp;
		
		float cur_hp_x_val = hp_bar_transform.localPosition.x;
		float new_hp_x_val = CalculateValue(current_hp, 0f, current_max_hp, min_hp_bar_x_value, max_hp_bar_x_value);
		if(new_hp_x_val != cur_hp_x_val){
			hp_bar_transform.localPosition = new Vector3(Mathf.Lerp(cur_hp_x_val, new_hp_x_val, Time.deltaTime * 5f), hp_bar_cached_y);
		}
		if(current_hp > current_max_hp / 2){ //More than 50% health.
			hp_bar.color = new Color32((byte)CalculateValue(current_hp, current_max_hp / 2, current_max_hp, 255, 0), 255, 0, 255);
		}else{ //Less than 50% health.
			hp_bar.color = new Color32(255, (byte)CalculateValue(current_hp, 0, current_max_hp / 2, 0, 255), 0 , 255);
		}
		
		float cur_pp_x_val = pp_bar_transform.localPosition.x;
		float new_pp_x_val = CalculateValue(current_pp, 0f, current_max_pp, min_pp_bar_x_value, max_pp_bar_x_value);
		if(new_pp_x_val != cur_pp_x_val){
			pp_bar_transform.localPosition = new Vector3(Mathf.Lerp(cur_pp_x_val, new_pp_x_val, Time.deltaTime * 5f), pp_bar_cached_y);
		}
	}
	public void HandleTargetGUI()
	{
		int current_hp = target.cur_hp;
		int current_max_hp = target.cur_max_hp;
		target_avatar.sprite = target.avatar;
		
		float cur_hp_x_val = target_hp_bar_transform.localPosition.x;
		float new_hp_x_val = CalculateValue(current_hp, 0f, current_max_hp, target_min_hp_bar_x_value, target_max_hp_bar_x_value);
		if(new_hp_x_val != cur_hp_x_val){
			target_hp_bar_transform.localPosition = new Vector3(Mathf.Lerp(cur_hp_x_val, new_hp_x_val, Time.deltaTime * 5f), target_hp_bar_cached_y);
		}
		if(current_hp > current_max_hp / 2){ //More than 50% health.
			target_hp_bar.color = new Color32((byte)CalculateValue(current_hp, current_max_hp / 2, current_max_hp, 255, 0), 255, 0, 255);
		}else{ //Less than 50% health.
			target_hp_bar.color = new Color32(255, (byte)CalculateValue(current_hp, 0, current_max_hp / 2, 0, 255), 0 , 255);
		}
	}
	private float CalculateValue(float curValue, float minValue, float maxValue, float minXPos, float maxXPos)
	{
		return (curValue - minValue) * (maxXPos - minXPos) / (maxValue - minValue) + minXPos;
	}
	
}
