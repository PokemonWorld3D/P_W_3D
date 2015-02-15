using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NEWPokemon : MonoBehaviour
{

#region Variables
	public bool is_alive = false;
	public bool is_setup = false;
	public float time_of_death;
	public bool is_captured = false;
	public GameObject trainer;
	public string trainers_name = string.Empty;
	public int pokemon_number;
	public string pokemon_name;
	public string nick_name = null;
	public string description;
	public bool is_from_trade = false;
	public int level;
	public int evolve_level;
	public PokemonTypes.Types type_one;
	public PokemonTypes.Types type_two;
	public BasePokemon.SexesList gender;
	public BasePokemon.NaturesList nature;
	public string ability_one;
	public string ability_two;
	public int base_hp;
	public int base_pp;
	public int base_atk;
	public int base_def;
	public int base_spatk;
	public int base_spdef;
	public int base_spd;
	public int max_hp;
	public int max_pp;
	public int max_atk;
	public int max_def;
	public int max_spatk;
	public int max_spdef;
	public int max_spd;
	public int cur_max_hp;
	public int cur_max_pp;
	public int cur_hp;
	public int cur_pp;
	public int cur_atk;
	public int cur_def;
	public int cur_spatk;
	public int cur_spdef;
	public int cur_spd;
	public float evasion;
	public float accuracy;
	public int atk_stage;
	public int def_stage;
	public int spatk_stage;
	public int spdef_stage;
	public int spd_stage;
	public int evasion_stage;
	public int accuracy_stage;
	public int hp_ev;
	public int atk_ev;
	public int def_ev;
	public int spatk_ev;
	public int spdef_ev;
	public int spd_ev;
	public int hp_iv;
	public int atk_iv;
	public int def_iv;
	public int spatk_iv;
	public int spdef_iv;
	public int spd_iv;
	public int base_exp_yield;
	public BasePokemon.LevelingRatesList leveling_rate;
	public int last_required_exp;
	public int current_exp;
	public int next_required_exp;
	public int hp_ev_yield;
	public int atk_ev_yield;
	public int def_ev_yield;
	public int spatk_ev_yield;
	public int spdef_ev_yield;
	public int spd_ev_yield;
	public int base_friendship;
	public int capture_rate;
	public BasePokemon.NonVolatileStatusConditionList status_condition;
	public float badly_poisoned_timer;
	public float sleep_timer;
	public bool confused;
	public float confuse_timer;
	public bool cursed;
	public bool embargoed;
	public float embargo_timer;
	public bool encored;
	public float encore_timer;
	public bool flinched;
	public bool heal_blocked;
	public float heal_block_timer;
	public bool identified;
	public bool infatuated;
	public bool nightmared;
	public bool partially_trapped;
	public float partially_trapped_timer;
	public bool perish_songed;
	public float perish_song_count_down;
	public bool seeded;
	public bool taunted;
	public float taunt_timer;
	public bool telekinectically_levitating;
	public float telekinetic_levitation_timer;
	public bool tormented;
	public bool trapped;
	public bool aqua_ringed;
	public bool bracing;
	public bool center_of_attention;
	public bool defense_curling;
	public bool glowing;
	public bool rooting;
	public bool magically_coated;
	public bool magnetically_levitating;
	public float magnetic_levitation_timer;
	public bool minimized;
	public bool protecting;
	public bool recharging;
	public bool semi_invulnerable;
	public bool has_a_substitute;
	public GameObject substitute;
	public bool taking_aim;
	public bool taking_in_sunlight;
	public bool withdrawing;
	public bool whipping_up_a_whirlwind;
	public List<Move> moves_to_learn = new List<Move>();
	public List<Move> known_moves = new List<Move>();
	public Move last_move_used;
	public Item equipped_item;
	public bool is_in_battle;
	public int origin;
	public int gender_ratio;
	public bool is_shiny = false;
	public Sprite avatar;
	public LensFlare evolve_flare;
	public float flare_grow_delay = 0.05f;
	public float flare_die_delay = 0.075f;
	public float evolution_flare_size;
	public GameObject evolves_into;
	public GameObject mesh;

	public enum Stats { HITPOINTS, POWERPOINTS, ATTACK, DEFENSE, SPECIALATTACK, SPECIALDEFENSE, SPEED }
	public enum AccEva { EVASION, ACCURACY }

	private StatCalculations stat_calculations_script = new StatCalculations();
	private CalculateXP calculate_exp_script = new CalculateXP();
	private Animator anim;
	private bool evolution_started;
#endregion

	void Start()
	{
		anim = GetComponent<Animator>();
		is_alive = true;
		if(!is_setup)
		{
			SetupPokemonFirstTime();
		}
		else
		{
			SetupSetupPokemon();
		}
	}
	void Update()
	{
		if(cur_hp == 0)
		{
			SetDead();
		}
		if(level == evolve_level && !evolution_started)
		{
			evolution_started = true;
			StartCoroutine(Evolve());
		}
	}

	private void SetupPokemonFirstTime(){
		SetupIV();
		SetupShininess();
		SetupGender();
		SetupNature();
		SetupStats();
		SetupMoves();
		is_setup = true;
	}
	private void SetupSetupPokemon()
	{
		SetupStats();
	}
	private void SetupIV(){
		hp_iv = UnityEngine.Random.Range(0,32);
		atk_iv = UnityEngine.Random.Range(0,32);
		def_iv = UnityEngine.Random.Range(0,32);
		spatk_iv = UnityEngine.Random.Range(0,32);
		spdef_iv = UnityEngine.Random.Range(0,32);
		spd_iv = UnityEngine.Random.Range(0,32);
	}
	private void SetupShininess(){
		if(def_iv == 10 && spd_iv == 10 && spatk_iv == 10){
			if(atk_iv == 2 || atk_iv == 3 || atk_iv == 6 || atk_iv == 7 || atk_iv == 10 || atk_iv == 11 || atk_iv == 14 || atk_iv == 15){
				is_shiny = true;
			}
		}
		if(is_shiny){
			Renderer[] rendererArray = GetComponentsInChildren<Renderer>();
			for(int r = 0; r < rendererArray.Length; r++){
				string materialName = rendererArray[r].material.name;
				materialName = materialName.Substring(0, materialName.Length - 11);
				rendererArray[r].material = Resources.Load<Material>("Models/Pokemon/Materials/" + materialName + "S");
			}
		}
	}
	private void SetupGender(){
		if(atk_iv > gender_ratio){
			gender = BasePokemon.SexesList.MALE;
		}else if(atk_iv <= gender_ratio){
			gender = BasePokemon.SexesList.FEMALE;
		}
	}
	private void SetupNature(){
		System.Array natures = System.Enum.GetValues (typeof(BasePokemon.NaturesList));
		nature = (BasePokemon.NaturesList)natures.GetValue (UnityEngine.Random.Range(0,24));
	}
	private void SetupStats(){
		max_hp = stat_calculations_script.CalculateHP (base_hp, level, hp_iv, hp_ev);
		cur_max_hp = max_hp;
		max_pp = stat_calculations_script.CalculatePP (base_pp, level);
		cur_max_pp = max_pp;
		max_atk = stat_calculations_script.CalculateStat (base_atk, level, atk_iv, atk_ev, nature, StatCalculations.StatTypes.ATTACK);
		max_def = stat_calculations_script.CalculateStat (base_def, level, def_iv, def_ev, nature, StatCalculations.StatTypes.DEFENSE);
		max_spatk = stat_calculations_script.CalculateStat (base_spatk, level, spatk_iv, spatk_ev, nature, StatCalculations.StatTypes.SPECIALATTACK);
		max_spdef = stat_calculations_script.CalculateStat (base_spdef, level, spdef_iv, spdef_ev, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
		max_spd = stat_calculations_script.CalculateStat (base_spd, level, spd_iv, spd_ev, nature, StatCalculations.StatTypes.SPEED);
		cur_hp = cur_max_hp;
		cur_pp = cur_max_pp;
		cur_atk = max_atk;
		cur_def = max_def;
		cur_spatk = max_spatk;
		cur_spdef = max_spdef;
		cur_spd = max_spd;
		evasion = 1.0f;
		accuracy = 1.0f;
		last_required_exp = calculate_exp_script.CalculateCurrentXP(level - 1, leveling_rate);
		current_exp = calculate_exp_script.CalculateCurrentXP(level, leveling_rate);
		next_required_exp = calculate_exp_script.CalculateRequiredXP(level, leveling_rate);
	}
	private void SetupMoves()
	{
//		List<Move> tempList = new List<Move>();
		foreach(Move move in moves_to_learn){
			if(level >= move.level_learned){
				if(!known_moves.Contains(move)){
					known_moves.Add(move);
//					tempList.Add(move);
				}
			}
		}
/*		foreach(Move move in tempList){
			if(moves_to_learn.Contains(move)){
				moves_to_learn.Remove(move);
			}
		}
*/	}

	public void AdjustCurrentMaxHP(int adj)
	{
		cur_max_hp += adj;
		if(cur_max_hp < 0){
			cur_max_hp = 0;
		}
		if(cur_max_hp > max_hp){
			cur_max_hp = max_hp;
		}
	}
	public void AdjustCurrentHP(int adj)
	{
		cur_hp += adj;
		if(cur_hp < 0){
			cur_hp = 0;
		}
		if(cur_hp > cur_max_hp){
			cur_hp = cur_max_hp;
		}
	}
	public void AdjustCurrentMaxPP(int adj)
	{
		cur_max_pp += adj;
		if(cur_max_pp < 0){
			cur_max_pp = 0;
		}
		if(cur_max_pp > max_pp){
			cur_max_pp = max_pp;
		}
	}
	public void AdjustCurrentPP(int adj)
	{
		cur_pp += adj;
		if(cur_pp < 0){
			cur_pp = 0;
		}
		if(cur_pp > cur_max_pp){
			cur_pp = cur_max_pp;
		}
	}
	public void AdjustCurrentStat(NEWPokemon.Stats stat, int adj)
	{
		if(stat == Stats.ATTACK){
			atk_stage += adj;
			if(atk_stage < -6){
				atk_stage = -6;
			}
			if(atk_stage > 6){
				atk_stage = 6;
			}
			atk_stage = ChangeStatTo(atk_stage, max_atk);
		}
		if(stat == Stats.DEFENSE){
			def_stage += adj;
			if(def_stage < -6){
				def_stage = -6;
			}
			if(def_stage > 6){
				def_stage = 6;
			}
			def_stage = ChangeStatTo(def_stage, max_def);
		}
		if(stat == Stats.SPECIALATTACK){
			spatk_stage += adj;
			if(spatk_stage < -6){
				spatk_stage = -6;
			}
			if(spatk_stage > 6){
				spatk_stage = 6;
			}
			spatk_stage = ChangeStatTo(spatk_stage, max_spatk);
		}
		if(stat == Stats.SPECIALDEFENSE){
			spdef_stage += adj;
			if(spdef_stage < -6){
				spdef_stage = -6;
			}
			if(spdef_stage > 6){
				spdef_stage = 6;
			}
			spdef_stage = ChangeStatTo(spdef_stage, max_spdef);
		}
		if(stat == Stats.SPEED){
			spd_stage += adj;
			if(spd_stage < -6){
				spd_stage = -6;
			}
			if(spd_stage > 6){
				spd_stage = 6;
			}
			spd_stage = ChangeStatTo(spd_stage, max_spd);
		}
	}
	public void AdjustCurrentAccEva(NEWPokemon.AccEva stat, int adj){
		if(stat == AccEva.ACCURACY){
			accuracy_stage += adj;
			if(accuracy_stage < -6){
				accuracy_stage = -6;
			}
			if(accuracy_stage > 6){
				accuracy_stage = 6;
			}
			accuracy = ChangeAccEvaTo(accuracy_stage);
		}
		if(stat == AccEva.EVASION){
			evasion_stage += adj;
			if(evasion_stage < -6){
				evasion_stage = -6;
			}
			if(evasion_stage > 6){
				evasion_stage = 6;
			}
			evasion = ChangeAccEvaTo(evasion_stage);
		}
	}
	public void AdjustCurrentEXP(int adj)
	{
		current_exp += adj;
		if(current_exp >= next_required_exp){
			level += 1;
			last_required_exp = next_required_exp;
			next_required_exp = calculate_exp_script.CalculateRequiredXP(level, leveling_rate);
			max_hp = stat_calculations_script.CalculateHP (base_hp, level, hp_iv, hp_ev);
			max_pp = stat_calculations_script.CalculatePP (base_pp, level);
			max_atk = stat_calculations_script.CalculateStat (base_atk, level, atk_iv, atk_ev, nature, StatCalculations.StatTypes.ATTACK);
			max_def = stat_calculations_script.CalculateStat (base_def, level, def_iv, def_ev, nature, StatCalculations.StatTypes.DEFENSE);
			max_spatk = stat_calculations_script.CalculateStat (base_spatk, level, spatk_iv, spatk_ev, nature,StatCalculations.StatTypes.SPECIALATTACK);
			max_spdef = stat_calculations_script.CalculateStat (base_spdef, level, spdef_iv, spdef_ev, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
			max_spd = stat_calculations_script.CalculateStat (base_spd, level, spd_iv, spd_ev, nature, StatCalculations.StatTypes.SPEED);
//			List<Move> tempList = new List<Move>();
			foreach(Move move in moves_to_learn){
				if(level >= move.level_learned){
					if(!known_moves.Contains(move)){
						known_moves.Add(move);
//						tempList.Add(move);
					}
				}
			}
/*			foreach(Move move in tempList){
				if(moves_to_learn.Contains(move)){
					moves_to_learn.Remove(move);
				}
			}
*/		}
	}
	public void SetDead(){
		if(trainers_name == null){
			is_alive = false;
			time_of_death = Time.time;
//			ReSpawner.deadPokemon.Add(this);
			this.gameObject.SetActive(false);
		}
	}

	private int ChangeStatTo(int statStage, int maxStat){
		if(statStage <= -6){
			return (int)((float)maxStat * 0.25f);
		}else if(statStage == -5){
			return (int)((float)maxStat * 0.2857142857f);
		}else if(statStage == -4){
			return (int)((float)maxStat * 0.3333333333f);
		}else if(statStage == -3){
			return (int)((float)maxStat * 0.4f);
		}else if(statStage == -2){
			return (int)((float)maxStat * 0.5f);
		}else if(statStage == -1){
			return (int)((float)maxStat * 0.6666666667f);
		}else if(statStage == 1){
			return (int)((float)maxStat * 1.5f);
		}else if(statStage == 2){
			return (int)((float)maxStat * 2f);
		}else if(statStage == 3){
			return (int)((float)maxStat * 2.5f);
		}else if(statStage == 4){
			return (int)((float)maxStat * 3f);
		}else if(statStage == 5){
			return (int)((float)maxStat * 3.5f);
		}else if(statStage >= 6){
			return (int)((float)maxStat * 4f);
		}else{
			return (int)((float)maxStat * 1f);
		}	
	}
	private float ChangeAccEvaTo(int statStage){
		if(statStage <= -6){
			return 0.3333333333f;
		}else if(statStage == -5){
			return 0.375f;
		}else if(statStage == -4){
			return 0.4285714286f;
		}else if(statStage == -3){
			return 0.5f;
		}else if(statStage == -2){
			return 0.6f;
		}else if(statStage == -1){
			return 0.75f;
		}else if(statStage == 1){
			return 1.3333333333f;
		}else if(statStage == 2){
			return 1.6666666667f;
		}else if(statStage == 3){
			return 2f;
		}else if(statStage == 4){
			return 2.3333333333f;
		}else if(statStage == 5){
			return 2.6666666667f;
		}else if(statStage == 6){
			return 3f;
		}else{
			return 1f;
		}
	}
	private void GiveStatsToEvolvedForm(bool this_is_setup, bool this_is_captured, GameObject this_trainer, string this_trainers_name, string this_nick_name,
	                                    bool this_is_from_trade, int this_level, BasePokemon.SexesList this_gender, BasePokemon.NaturesList this_nature,
	                                    int this_hp_iv, int this_atk_iv, int this_def_iv, int this_spatk_iv, int this_spdef_iv, int this_spd_iv, int this_hp_ev,
	                                    int this_atk_ev, int this_def_ev, int this_spatk_ev, int this_spdef_ev, int this_spd_ev, List<Move> this_known_moves,
	                                    Move this_last_move_used, Item this_equipped_item, bool this_is_in_battle, int this_origin, bool this_is_shiny)
	{
		is_setup = this_is_setup;
		is_captured = this_is_captured;
		trainer = this_trainer;
		trainers_name = this_trainers_name;
		nick_name = this_nick_name;
		is_from_trade = this_is_from_trade;
		level = this_level;
		gender = this_gender;
		nature = this_nature;
		hp_iv = this_hp_iv;
		atk_iv = this_atk_iv;
		def_iv = this_def_iv;
		spatk_iv = this_spatk_iv;
		spdef_iv = this_spdef_iv;
		spd_iv = this_spd_iv;
		hp_ev = this_hp_ev;
		atk_ev = this_atk_ev;
		def_ev = this_def_ev;
		spatk_ev = this_spatk_ev;
		spdef_ev = this_spdef_ev;
		spd_ev = this_spd_ev;
		known_moves = this_known_moves;
		last_move_used = this_last_move_used;
		equipped_item = this_equipped_item;
		is_in_battle = this_is_in_battle;
		origin = this_origin;
		is_shiny = this_is_shiny;
	}
	private IEnumerator Evolve()
	{
		GetComponent<PokemonInput>().enabled = false;
		rigidbody.Sleep();
		foreach(Material material in mesh.GetComponent<SkinnedMeshRenderer>().materials)
		{
			StartCoroutine(ChangeToWhite(material));
		}
		yield return new WaitForSeconds(1);
		//anim.enabled = false;
		animation.Play(pokemon_name + "_Evolve");
		while(this.animation.isPlaying)
		{
			yield return null;
		}
		StartCoroutine(IncreaseFlare());
		while(evolve_flare.brightness < evolution_flare_size - 1f)
		{
			yield return null;
		}
		GameObject evolved_form = Instantiate(evolves_into, transform.position, transform.rotation) as GameObject;
		evolved_form.rigidbody.Sleep();
		Material[] evolved_materials = evolved_form.GetComponentInChildren<SkinnedMeshRenderer>().materials;
		evolved_form.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		foreach(Material material in evolved_materials)
		{
			material.SetFloat("_Blend", 1f);
		}
		evolved_form.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		trainer.GetComponent<PlayerCharacter>().SetActivePokemon(evolved_form);
		Camera.main.GetComponent<CameraController>().SetTarget(evolved_form);
		SkinnedMeshRenderer componenets = GetComponentInChildren<SkinnedMeshRenderer>();
		componenets.enabled = false;
		while(evolve_flare.brightness > 0f){
			yield return null;
		}
		foreach(Material material in evolved_materials){
			StartCoroutine(ChangeToColor(material));
		}
		yield return new WaitForSeconds(1);
		evolved_form.GetComponent<NEWPokemon>().GiveStatsToEvolvedForm(is_setup, is_captured, trainer, trainers_name, nick_name, is_from_trade, level, gender,
		                                                               nature, hp_iv, atk_iv, def_iv, spatk_iv, spdef_iv, spd_iv, hp_ev, atk_ev, def_ev, spatk_ev,
		                                                               spdef_ev, spd_ev, known_moves, last_move_used, equipped_item, is_in_battle, origin, is_shiny);
		evolved_form.rigidbody.WakeUp();
		evolved_form.GetComponent<PokemonInput>().enabled = true;
		Destroy(this.gameObject);
		yield return null;
	}
	private IEnumerator ChangeToWhite(Material mat)
	{
		float counter = mat.GetFloat("_Blend");
		while(counter != 1f){
			float increase = mat.GetFloat("_Blend") + Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter += increase;
			yield return null;
		}
	}
	private IEnumerator IncreaseFlare()
	{
		float increase = flare_grow_delay + Time.deltaTime;
		while(evolve_flare.brightness < evolution_flare_size){
			evolve_flare.brightness = evolve_flare.brightness + increase;
			yield return null;
		}
		float decrease = flare_die_delay + Time.deltaTime;
		while(evolve_flare.brightness > 0f){
			evolve_flare.brightness = evolve_flare.brightness - decrease;
			yield return null;
		}
	}
	private IEnumerator ChangeToColor(Material mat)
	{
		float counter = mat.GetFloat("_Blend");
		while(counter != 0f){
			float increase = mat.GetFloat("_Blend") - Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter -= increase;
			yield return null;
		}
	}
	

}
