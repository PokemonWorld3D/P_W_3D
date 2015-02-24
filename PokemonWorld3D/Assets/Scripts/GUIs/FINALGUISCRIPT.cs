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
	public GameObject player_pokemon_portait;
	public GameObject this_pokemon;
	public Pokemon active_pokemon;
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
	public Pokemon target;
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
	public GameObject move_one;
	public Image move_one_icon;
	public Image move_one_timer;
	public Text move_one_pp;
	public GameObject move_two;
	public Image move_two_icon;
	public Image move_two_timer;
	public Text move_two_pp;
	public GameObject move_three;
	public Image move_three_icon;
	public Image move_three_timer;
	public Text move_three_pp;
	public GameObject move_four;
	public Image move_four_icon;
	public Image move_four_timer;
	public Text move_four_pp;
	public GameObject move_five;
	public Image move_five_icon;
	public Image move_five_timer;
	public Text move_five_pp;
	public GameObject move_six;
	public Image move_six_icon;
	public Image move_six_timer;
	public Text move_six_pp;
	public GameObject move_seven;
	public Image move_seven_icon;
	public Image move_seven_timer;
	public Text move_seven_pp;
	public GameObject move_eight;
	public Image move_eight_icon;
	public Image move_eight_timer;
	public Text move_eight_pp;
	public GameObject move_nine;
	public Image move_nine_icon;
	public Image move_nine_timer;
	public Text move_nine_pp;
	public GameObject move_ten;
	public Image move_ten_icon;
	public Image move_ten_timer;
	public Text move_ten_pp;
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
		if(player_pokemon_portait.activeInHierarchy)
		{
			HandlePlayerPokemonGUI();
		}
		else
		{

		}
		if(target_pokemon_portrait.activeInHierarchy == true)
			HandleTargetGUI();
	}

	public void SetActivePokemon(Pokemon players_active_pokemon)
	{
		active_pokemon = players_active_pokemon;
		this_pokemon = owner.GetComponent<PlayerCharacter>().players_active_pokemon;
		player_pokemon_portait.SetActive(true);
	}
	public void RemoveActivePokemon()
	{
		active_pokemon = null;
		this_pokemon = null;
		player_pokemon_portait.SetActive(false);
	}
	public void SetTarget(Pokemon players_target)
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
		Color inactive_color = new Color32(255, 255, 255, 0);
		Color icon_color = new Color32(255, 255, 255, 255);
		if(active_pokemon.known_moves.Count >= 1)
		{
			move_one.SetActive(true);
			move_one_icon.color = icon_color;
			move_one_icon.sprite = active_pokemon.known_moves[0].icon;
			move_one_timer.fillAmount = active_pokemon.known_moves[0].cooling_down / active_pokemon.known_moves[0].cool_down;
			move_one_pp.text = active_pokemon.known_moves[0].pp_cost.ToString();
		}
		else
		{
			move_one_icon.color = inactive_color;
			move_one.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 2)
		{
			move_two.SetActive(true);
			move_two_icon.color = icon_color;
			move_two_icon.sprite = active_pokemon.known_moves[1].icon;
			move_two_timer.fillAmount = active_pokemon.known_moves[1].cooling_down / active_pokemon.known_moves[1].cool_down;
			move_two_pp.text = active_pokemon.known_moves[1].pp_cost.ToString();
		}
		else
		{
			move_two_icon.color = inactive_color;
			move_two.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 3)
		{
			move_three.SetActive(true);
			move_three_icon.color = icon_color;
			move_three_icon.sprite = active_pokemon.known_moves[2].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_three_pp.text = active_pokemon.known_moves[2].pp_cost.ToString();
		}
		else
		{
			move_three_icon.color = inactive_color;
			move_three.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 4)
		{
			move_four.SetActive(true);
			move_four_icon.color = icon_color;
			move_four_icon.sprite = active_pokemon.known_moves[3].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_four_pp.text = active_pokemon.known_moves[3].pp_cost.ToString();
		}
		else
		{
			move_four_icon.color = inactive_color;
			move_four.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 5)
		{
			move_five.SetActive(true);
			move_five_icon.color = icon_color;
			move_five_icon.sprite = active_pokemon.known_moves[4].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_five_pp.text = active_pokemon.known_moves[4].pp_cost.ToString();
		}
		else
		{
			move_five_icon.color = inactive_color;
			move_five.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 6)
		{
			move_six.SetActive(true);
			move_six_icon.color = icon_color;
			move_six_icon.sprite = active_pokemon.known_moves[5].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_six_pp.text = active_pokemon.known_moves[5].pp_cost.ToString();
		}
		else
		{
			move_six_icon.color = inactive_color;
			move_six.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 7)
		{
			move_seven.SetActive(true);
			move_seven_icon.color = icon_color;
			move_seven_icon.sprite = active_pokemon.known_moves[6].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_seven_pp.text = active_pokemon.known_moves[6].pp_cost.ToString();
		}
		else
		{
			move_seven_icon.color = inactive_color;
			move_seven.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 8)
		{
			move_eight.SetActive(true);
			move_eight_icon.color = icon_color;
			move_eight_icon.sprite = active_pokemon.known_moves[7].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_eight_pp.text = active_pokemon.known_moves[7].pp_cost.ToString();
		}
		else
		{
			move_eight_icon.color = inactive_color;
			move_eight.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 9)
		{
			move_nine.SetActive(true);
			move_nine_icon.color = icon_color;
			move_nine_icon.sprite = active_pokemon.known_moves[8].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_nine_pp.text = active_pokemon.known_moves[8].pp_cost.ToString();
		}
		else
		{
			move_nine_icon.color = inactive_color;
			move_nine.SetActive(false);
		}
		if(active_pokemon.known_moves.Count >= 10)
		{
			move_ten.SetActive(true);
			move_ten_icon.color = icon_color;
			move_ten_icon.sprite = active_pokemon.known_moves[9].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_ten_pp.text = active_pokemon.known_moves[9].pp_cost.ToString();
		}
		else
		{
			move_ten_icon.color = inactive_color;
			move_ten.SetActive(false);
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
