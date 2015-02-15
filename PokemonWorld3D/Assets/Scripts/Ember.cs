using UnityEngine;
using System.Collections;

public class Ember : MonoBehaviour
{

	public Move this_move = new Move();
	public float lifeTime;
	public float speed;
	public PokemonTypes.Types moveType;

	public int thisPokemonsLevel;
	public int thisPokemonsSPATK;
	public int targetPokemonsSPDEF;
	public int thisPokemonsBaseSPD;
	public PokemonTypes.Types thisPokemonsType1;
	public PokemonTypes.Types thisPokemonsType2;
	public PokemonTypes.Types targetPokemonsType1;
	public PokemonTypes.Types targetPokemonsType2;

	public int damage;

	private DamageCalculation dmgCalc = new DamageCalculation();

	void Start(){
//		thisPokemonsLevel = gameObject.transform.parent.GetComponentInParent<NEWPokemon>().level;
//		thisPokemonsSPATK = gameObject.transform.parent.GetComponentInParent<NEWPokemon>().curSPATK;
//		thisPokemonsBaseSPD = gameObject.transform.parent.GetComponentInParent<NEWPokemon>().baseSPD;
//		thisPokemonsType1 = gameObject.transform.parent.GetComponentInParent<NEWPokemon>().type01;
//		thisPokemonsType2 = gameObject.transform.parent.GetComponentInParent<NEWPokemon>().type02;
	}

	void Update(){
		Vector3 AddPos = Vector3.forward;
		AddPos = rigidbody.rotation * AddPos;
		rigidbody.velocity = AddPos * speed;
		lifeTime -= Time.deltaTime;
		if(lifeTime < 0f){
			Destroy(this.gameObject);
		}
	}

	public void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Pokemon"){
			targetPokemonsSPDEF = col.gameObject.GetComponent<NEWPokemon>().cur_spdef;
			targetPokemonsType1 = col.gameObject.GetComponent<NEWPokemon>().type_one;
			targetPokemonsType2 = col.gameObject.GetComponent<NEWPokemon>().type_two;
			damage = dmgCalc.CalculateSpecialAttackDamage(this_move.power, this_move.high_crit_chance, this_move.type, thisPokemonsLevel, thisPokemonsSPATK,
			                                              targetPokemonsSPDEF, thisPokemonsType1, thisPokemonsType2, targetPokemonsType1, targetPokemonsType2,
			                                              thisPokemonsBaseSPD);
			col.gameObject.SendMessage("AdjustCurrentHP", -damage);
			Destroy(this.gameObject);
		}
	}

}
