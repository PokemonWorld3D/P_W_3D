using UnityEngine;
using System;
using System.Collections;

public class Movement : MonoBehaviour {

	public float rotateSpeed = 250f;
	public float moveSpeed = 5f;
	public AudioClip pokeBallGrow;
	public AudioClip pokemonOut;
	public AudioClip pokemonReturn;
	public AudioClip pokeBallShrink;
	public float throwPower;
	public float maxAngVel;
	public GameObject parentBone;

	private GameObject pokemonPokeBallPrefab;
	private GameObject emptyPokeBallPrefab;
	private GameObject pokeball;
	private GameObject pokeBall;
	private Rigidbody pokeballRigidbody;
	private Rigidbody pokeBallRigidbody;
	private GameObject pokemonToRelease;
	private CharacterController controller;
	private Transform myTransform;
	private Animator anim;
	private GameObject playerPokemon;
	private PlayersPokemon roster;

	void Awake(){
		controller = GetComponent<CharacterController>();
		myTransform = transform;
	}

	void Start () {
		anim = GetComponent<Animator>();
		roster = gameObject.GetComponent<PlayersPokemon>();
		pokemonPokeBallPrefab = Resources.Load<GameObject>("Prefabs/Pokemon PokeBall Prefab");
		emptyPokeBallPrefab = Resources.Load<GameObject>("Prefabs/Empty PokeBall Prefab");
	}

	private void SummonPokemon(){
		if(Input.GetButtonUp("Poke Slot 1") && roster.pokemonRoster.Count >= 1){
			StartCoroutine(SummonPokemon(roster.pokemonRoster[0], pokeball.transform.position, gameObject.transform.position, setPokemon, 1));
		}
		if(Input.GetButtonUp("Poke Slot 2") && roster.pokemonRoster.Count >= 2){
			StartCoroutine(SummonPokemon(roster.pokemonRoster[1], pokeball.transform.position, gameObject.transform.position, setPokemon, 2));
		}
		if(Input.GetButtonUp("Poke Slot 3") && roster.pokemonRoster.Count >= 3){
			StartCoroutine(SummonPokemon(roster.pokemonRoster[2], pokeball.transform.position, gameObject.transform.position, setPokemon, 3));
		}
		if(Input.GetButtonUp("Poke Slot 4") && roster.pokemonRoster.Count >= 4){
			StartCoroutine(SummonPokemon(roster.pokemonRoster[3], pokeball.transform.position, gameObject.transform.position, setPokemon, 4));
		}
		if(Input.GetButtonUp("Poke Slot 5") && roster.pokemonRoster.Count >= 5){
			StartCoroutine(SummonPokemon(roster.pokemonRoster[4], pokeball.transform.position, gameObject.transform.position, setPokemon, 5));
		}
		if(Input.GetButtonUp("Poke Slot 6") && roster.pokemonRoster.Count == 6){
			StartCoroutine(SummonPokemon(roster.pokemonRoster[5], pokeball.transform.position, gameObject.transform.position, setPokemon, 6));
		}
	}


	public void setPokemon(GameObject newPokemon){

	}

	public IEnumerator SummonPokemon(PlayerPokemonData pokemonData, Vector3 spotForPokemon, Vector3 whereToLook, Action<GameObject> pokemonCallBack, int rosterSpot){
		GameObject pokemonToBe = new GameObject();
		anim.SetTrigger("ThrowingPokeBall");
		audio.PlayOneShot(pokeBallGrow);
		yield return new WaitForSeconds(pokeBallGrow.length);
		while(Vector3.Distance(pokeball.transform.position, gameObject.transform.position) < 5f){
			yield return null;
		}
		pokeball.collider.enabled = false;
		pokeball.rigidbody.Sleep();
		pokeball.transform.LookAt(transform.forward);
		pokeball.audio.PlayOneShot(pokemonOut);
		yield return new WaitForSeconds(pokemonOut.length);
		#region Pokemon
		pokemonToRelease = (GameObject)Resources.Load("Prefabs/" + pokemonData.pokemonName.ToString() + " Prefab");
		pokemonToBe = Instantiate(pokemonToRelease, spotForPokemon, pokeball.transform.rotation) as GameObject;
		pokemonToBe.transform.LookAt(whereToLook);
		pokemonToBe.tag = "PlayerPokemon";
		pokemonToBe.GetComponent<Pokemon>().isAlive = pokemonData.isAlive;
		pokemonToBe.GetComponent<Pokemon>().timeOfDeath = pokemonData.timeOfDeath;
		pokemonToBe.GetComponent<Pokemon>().isCaptured = pokemonData.isCaptured;
		pokemonToBe.GetComponent<Pokemon>().number = pokemonData.number;
		pokemonToBe.GetComponent<Pokemon>().pokemonName = pokemonData.pokemonName;
		pokemonToBe.GetComponent<Pokemon>().description = pokemonData.description;
		pokemonToBe.GetComponent<Pokemon>().isFromTrade = pokemonData.isFromTrade;
		pokemonToBe.GetComponent<Pokemon>().level = pokemonData.level;
		pokemonToBe.GetComponent<Pokemon>().evolveLevel = pokemonData.evolveLevel;
		pokemonToBe.GetComponent<Pokemon>().type01 = pokemonData.type01;
		pokemonToBe.GetComponent<Pokemon>().type02 = pokemonData.type02;
		pokemonToBe.GetComponent<Pokemon>().sex = pokemonData.sex;
		pokemonToBe.GetComponent<Pokemon>().nature = pokemonData.nature;
		pokemonToBe.GetComponent<Pokemon>().ability01 = pokemonData.ability01;
		pokemonToBe.GetComponent<Pokemon>().ability02 = pokemonData.ability02;
		pokemonToBe.GetComponent<Pokemon>().baseHP = pokemonData.baseHP;
		pokemonToBe.GetComponent<Pokemon>().baseATK = pokemonData.baseATK;
		pokemonToBe.GetComponent<Pokemon>().baseDEF = pokemonData.baseDEF;
		pokemonToBe.GetComponent<Pokemon>().baseSPATK = pokemonData.baseSPATK;
		pokemonToBe.GetComponent<Pokemon>().baseSPDEF = pokemonData.baseSPDEF;
		pokemonToBe.GetComponent<Pokemon>().baseSPD = pokemonData.baseSPD;
		pokemonToBe.GetComponent<Pokemon>().maxHP = pokemonData.maxHP;
		pokemonToBe.GetComponent<Pokemon>().curMaxHP = pokemonData.curMaxHP;
		pokemonToBe.GetComponent<Pokemon>().maxATK = pokemonData.maxATK;
		pokemonToBe.GetComponent<Pokemon>().maxDEF = pokemonData.maxDEF;
		pokemonToBe.GetComponent<Pokemon>().maxSPATK = pokemonData.maxSPATK;
		pokemonToBe.GetComponent<Pokemon>().maxSPDEF = pokemonData.maxSPDEF;
		pokemonToBe.GetComponent<Pokemon>().maxSPD = pokemonData.maxSPD;
		pokemonToBe.GetComponent<Pokemon>().curHP = pokemonData.curHP;
		pokemonToBe.GetComponent<Pokemon>().curATK = pokemonData.curATK;
		pokemonToBe.GetComponent<Pokemon>().curDEF = pokemonData.curDEF;
		pokemonToBe.GetComponent<Pokemon>().curSPATK = pokemonData.curSPATK;
		pokemonToBe.GetComponent<Pokemon>().curSPDEF = pokemonData.curSPDEF;
		pokemonToBe.GetComponent<Pokemon>().curSPD = pokemonData.curSPD;
		pokemonToBe.GetComponent<Pokemon>().evasion = pokemonData.evasion;
		pokemonToBe.GetComponent<Pokemon>().accuracy = pokemonData.accuracy;
		pokemonToBe.GetComponent<Pokemon>().hpEV = pokemonData.hpEV;
		pokemonToBe.GetComponent<Pokemon>().atkEV = pokemonData.atkEV;
		pokemonToBe.GetComponent<Pokemon>().defEV = pokemonData.defEV;
		pokemonToBe.GetComponent<Pokemon>().spatkEV = pokemonData.spatkEV;
		pokemonToBe.GetComponent<Pokemon>().spdefEV = pokemonData.spdefEV;
		pokemonToBe.GetComponent<Pokemon>().spdEV = pokemonData.spdEV;
		pokemonToBe.GetComponent<Pokemon>().hpIV = pokemonData.hpIV;
		pokemonToBe.GetComponent<Pokemon>().atkIV = pokemonData.atkIV;
		pokemonToBe.GetComponent<Pokemon>().defIV = pokemonData.defIV;
		pokemonToBe.GetComponent<Pokemon>().spatkIV = pokemonData.spatkIV;
		pokemonToBe.GetComponent<Pokemon>().spdefIV = pokemonData.spdefIV;
		pokemonToBe.GetComponent<Pokemon>().spdIV = pokemonData.spdIV;
		pokemonToBe.GetComponent<Pokemon>().baseEXPYield = pokemonData.baseEXPYield;
		pokemonToBe.GetComponent<Pokemon>().levelingRate = pokemonData.levelingRate;
		pokemonToBe.GetComponent<Pokemon>().lastRequiredXP = pokemonData.lastRequiredXP;
		pokemonToBe.GetComponent<Pokemon>().currentXP = pokemonData.currentXP;
		pokemonToBe.GetComponent<Pokemon>().nextRequiredXP = pokemonData.nextRequiredXP;
		pokemonToBe.GetComponent<Pokemon>().hpEVYield = pokemonData.hpEVYield;
		pokemonToBe.GetComponent<Pokemon>().atkEVYield = pokemonData.atkEVYield;
		pokemonToBe.GetComponent<Pokemon>().defEVYield = pokemonData.defEVYield;
		pokemonToBe.GetComponent<Pokemon>().spatkEVYield = pokemonData.spatkEVYield;
		pokemonToBe.GetComponent<Pokemon>().spdefEVYield = pokemonData.spdefEVYield;
		pokemonToBe.GetComponent<Pokemon>().spdEVYield = pokemonData.spdEVYield;
		pokemonToBe.GetComponent<Pokemon>().baseFriendship = pokemonData.baseFriendship;
		pokemonToBe.GetComponent<Pokemon>().catchRate = pokemonData.catchRate;
		pokemonToBe.GetComponent<Pokemon>().statusCondition = pokemonData.statusCondition;
		pokemonToBe.GetComponent<Pokemon>().confusion = pokemonData.confusion;
		pokemonToBe.GetComponent<Pokemon>().confusionTurns = pokemonData.confusionTurns;
		pokemonToBe.GetComponent<Pokemon>().curse = pokemonData.curse;
		pokemonToBe.GetComponent<Pokemon>().embargo = pokemonData.embargo;
		pokemonToBe.GetComponent<Pokemon>().embargoTurns = pokemonData.embargoTurns;
		pokemonToBe.GetComponent<Pokemon>().encore = pokemonData.encore;
		pokemonToBe.GetComponent<Pokemon>().encoreTurns = pokemonData.encoreTurns;
		pokemonToBe.GetComponent<Pokemon>().flinch = pokemonData.flinch;
		pokemonToBe.GetComponent<Pokemon>().healBlock = pokemonData.healBlock;
		pokemonToBe.GetComponent<Pokemon>().healBlockTurns = pokemonData.healBlockTurns;
		pokemonToBe.GetComponent<Pokemon>().identification = pokemonData.identification;
		pokemonToBe.GetComponent<Pokemon>().infatuation = pokemonData.infatuation;
		pokemonToBe.GetComponent<Pokemon>().nightmare = pokemonData.nightmare;
		pokemonToBe.GetComponent<Pokemon>().partiallyTrapped = pokemonData.partiallyTrapped;
		pokemonToBe.GetComponent<Pokemon>().partiallyTrappedTurns = pokemonData.partiallyTrappedTurns;
		pokemonToBe.GetComponent<Pokemon>().perishSong = pokemonData.perishSong;
		pokemonToBe.GetComponent<Pokemon>().perishSongTurnCountDown = pokemonData.perishSongTurnCountDown;
		pokemonToBe.GetComponent<Pokemon>().seeding = pokemonData.seeding;
		pokemonToBe.GetComponent<Pokemon>().taunt = pokemonData.taunt;
		pokemonToBe.GetComponent<Pokemon>().tauntTurns = pokemonData.tauntTurns;
		pokemonToBe.GetComponent<Pokemon>().telekineticLevitation = pokemonData.telekineticLevitation;
		pokemonToBe.GetComponent<Pokemon>().telekineticLevitationTurns = pokemonData.telekineticLevitationTurns;
		pokemonToBe.GetComponent<Pokemon>().torment = pokemonData.torment;
		pokemonToBe.GetComponent<Pokemon>().trapped = pokemonData.trapped;
		pokemonToBe.GetComponent<Pokemon>().aquaRing = pokemonData.aquaRing;
		pokemonToBe.GetComponent<Pokemon>().bracing = pokemonData.bracing;
		pokemonToBe.GetComponent<Pokemon>().centerOfAttention = pokemonData.centerOfAttention;
		pokemonToBe.GetComponent<Pokemon>().defenseCurl = pokemonData.defenseCurl;
		pokemonToBe.GetComponent<Pokemon>().glowing = pokemonData.glowing;
		pokemonToBe.GetComponent<Pokemon>().rooting = pokemonData.rooting;
		pokemonToBe.GetComponent<Pokemon>().magicCoat = pokemonData.magicCoat;
		pokemonToBe.GetComponent<Pokemon>().magneticLevitation = pokemonData.magneticLevitation;
		pokemonToBe.GetComponent<Pokemon>().magneticLevitationTurns = pokemonData.magneticLevitationTurns;
		pokemonToBe.GetComponent<Pokemon>().minimize = pokemonData.minimize;
		pokemonToBe.GetComponent<Pokemon>().protection = pokemonData.protection;
		pokemonToBe.GetComponent<Pokemon>().recharging = pokemonData.recharging;
		pokemonToBe.GetComponent<Pokemon>().semiInvulnerable = pokemonData.semiInvulnerable;
		pokemonToBe.GetComponent<Pokemon>().substitute = pokemonData.substitute;
		pokemonToBe.GetComponent<Pokemon>().substituteHP = pokemonData.substituteHP;
		pokemonToBe.GetComponent<Pokemon>().takingAim = pokemonData.takingAim;
		pokemonToBe.GetComponent<Pokemon>().takingInSunlight = pokemonData.takingInSunlight;
		pokemonToBe.GetComponent<Pokemon>().withdrawing = pokemonData.withdrawing;
		pokemonToBe.GetComponent<Pokemon>().whippingUpAWhirlwind = pokemonData.whippingUpAWhirlwind;
		pokemonToBe.GetComponent<Pokemon>().movesToLearn = pokemonData.movesToLearn;
		pokemonToBe.GetComponent<Pokemon>().pokemonsMoves = pokemonData.pokemonsMoves;
		pokemonToBe.GetComponent<Pokemon>().equippedItem = pokemonData.equippedItem;
		pokemonToBe.GetComponent<Pokemon>().isInBattle = pokemonData.isInBattle;
		pokemonToBe.GetComponent<Pokemon>().origin = rosterSpot;
		#endregion
		Vector3 moveTo = new Vector3(parentBone.transform.position.x, parentBone.transform.position.y, parentBone.transform.position.z);
		while(Vector3.Distance(pokeball.transform.position, moveTo) > 1f){
			pokeball.transform.position = Vector3.Lerp(pokeball.transform.position, moveTo, 5f * Time.deltaTime);
			yield return null;
		}
		Destroy(pokeball);
		pokemonCallBack(pokemonToBe);	
		yield return null;
	}
	public void CreateBall(){
		pokeball = Instantiate(pokemonPokeBallPrefab, parentBone.transform.position, parentBone.transform.rotation) as GameObject;
		pokeball.transform.parent = parentBone.transform;
		pokeball.rigidbody.useGravity = false;
	}
	public void ReleaseMe(){
		pokeball.transform.parent = null;
		pokeball.rigidbody.useGravity = true;
		pokeball.transform.rotation = parentBone.transform.rotation;
		pokeball.rigidbody.AddForce(transform.forward * 1000);
	}

	public IEnumerator CapturePokemon(GameObject target){
		gameObject.GetComponent<Animator>().SetTrigger("AttemptingCapture");
		gameObject.audio.PlayOneShot(pokeBallGrow);
		yield return new WaitForSeconds(3.25f);
		Vector3 moveTo = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
		while(Vector3.Distance(pokeBall.transform.position, moveTo) > 1f){
			pokeBall.transform.position = Vector3.Lerp(pokeBall.transform.position, moveTo, 5f * Time.deltaTime);
			yield return null;
		}	
		yield return null;
	}
	public void CreateEmptyPokeBall(){
		pokeBall = Instantiate(emptyPokeBallPrefab, parentBone.transform.position, parentBone.transform.rotation) as GameObject;
		pokeBall.collider.enabled = false;
		pokeBall.transform.parent = parentBone.transform;
		pokeBall.rigidbody.useGravity = false;
	}
	public void ReleaseIt(){
		pokeBall.transform.parent = null;
		pokeBall.collider.enabled = true;
	}

}
