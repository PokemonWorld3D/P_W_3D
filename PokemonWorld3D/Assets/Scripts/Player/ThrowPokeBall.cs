using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowPokeBall : MonoBehaviour
{
	public AudioClip pokeBallGrow;
	public GameObject pokemonBall;
	public GameObject emptyBall;
	public GameObject grip;
	public float throw_power;
	public float max_angle_velocity;
	public AudioClip pokemonOut;

	private PlayerCharacter player;
	private PlayerInput input;
	private GameObject target;
	private Animator anim;

	void Start()
	{
		player = GetComponent<PlayerCharacter>();
		input = GetComponent<PlayerInput>();
		anim = GetComponent<Animator>();
	}

	[RPC]
	public void NetworkStats(bool nIsCaptured, int trainer, string nTrainersName, string nNickName, bool nIsFromTrade, int nLevel, int nGender,
	                         int nNature, int nCurHP, int nHPEV, int nATKEV, int nDEFEV, int nSPATKEV, int nSPDEFEV, int nSPDEV, int nHPIV, int nATKIV, int nDEFIV,
	                         int nSPATKIV, int nSPDEFIV, int nSPDIV, int nCurEXP, string[] nMovesToLearnNames, string[] nKnownMovesNames, int nOrigin, int pokemonID)
	{
		GameObject thePokemon = PhotonView.Find(pokemonID).gameObject;
		Pokemon thisPokemon = thePokemon.GetComponent<Pokemon>();
		GameObject thisTrainer = PhotonView.Find(trainer).gameObject;
		thisPokemon.isCaptured = nIsCaptured;
		thisPokemon.trainer = thisTrainer;
		thisPokemon.trainersName = thisTrainer.GetComponent<PlayerCharacter>().playersName;
		thisPokemon.nickName = nNickName;
		thisPokemon.isFromTrade = nIsFromTrade;
		thisPokemon.level = nLevel;
		thisPokemon.gender = (Pokemon.Genders)nGender;
		thisPokemon.nature = (Pokemon.Natures)nNature;
		thisPokemon.curHP = nCurHP;
		thisPokemon.hpEV = nHPEV;
		thisPokemon.atkEV = nATKEV;
		thisPokemon.defEV = nDEFEV;
		thisPokemon.spatkEV = nSPATKEV;
		thisPokemon.spdefEV = nSPDEFEV;
		thisPokemon.spdEV = nSPDEV;
		thisPokemon.hpIV = nHPIV;
		thisPokemon.atkIV = nATKIV;
		thisPokemon.defIV = nDEFIV;
		thisPokemon.spatkIV = nSPATKIV;
		thisPokemon.spdefIV = nSPDEFIV;
		thisPokemon.spdIV = nSPDIV;
		thisPokemon.currentEXP = nCurEXP;
		List<string> MovesToLearnNames = new List<string>();
		for(int i = 0; i < nMovesToLearnNames.Length; i++)
		{
			MovesToLearnNames.Add(nMovesToLearnNames[i]);
		}
		thisPokemon.MovesToLearnNames = MovesToLearnNames;
		List<string> KnownMovesNames = new List<string>();
		for(int i = 0; i < nKnownMovesNames.Length; i++)
		{
			KnownMovesNames.Add(nKnownMovesNames[i]);
		}
		thisPokemon.KnownMovesNames = KnownMovesNames;
		thisPokemon.origin = nOrigin;
		thePokemon.GetComponent<PhotonView>().RPC("SetupSetupPokemon", PhotonTargets.AllBuffered);
		
	}
	public void CreatePokemonBall()
	{
		audio.PlayOneShot(pokeBallGrow);
		pokemonBall.rigidbody.useGravity = false;
		pokemonBall.SetActive(true);
	}
	public void ThrowPokemonBall()
	{
		pokemonBall.transform.parent = null;
		pokemonBall.rigidbody.AddForce(transform.forward * 1000);
		pokemonBall.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throw_power);
		pokemonBall.GetComponent<Rigidbody>().AddTorque(10, 0, 0);
		pokemonBall.GetComponent<Rigidbody>().maxAngularVelocity = max_angle_velocity;
	}
	public void CreateEmptyBall()
	{
		audio.PlayOneShot(pokeBallGrow);
		emptyBall.collider.enabled = false;
		emptyBall.rigidbody.useGravity = false;
		emptyBall.SetActive(true);
	}
	public void ThrowEmptyBall()
	{
		emptyBall.transform.parent = null;
		emptyBall.rigidbody.useGravity = true;
		Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().renderer.bounds.center;
		Vector3 throwSpeed = calculateBestThrowSpeed(emptyBall.transform.position, targetPos, 1.0f);
		emptyBall.rigidbody.AddForce(throwSpeed, ForceMode.VelocityChange);
		emptyBall.collider.enabled = true;
	}
	public void EndThrowingBall()
	{
		anim.SetBool("ThrowPokemonBall", false);
		anim.SetBool("ThrowEmptyBall", false);
	}
	public void PauseReturn()
	{
		anim.SetBool("StartPokemonReturn", false);
	}
	public void EndReturn()
	{
		pokemonBall.SetActive(false);
		anim.SetBool("FinishPokemonReturn", false);
	}

	private void OpenBall()
	{
		pokemonBall.GetComponent<Animator>().SetBool("Open", true);
	}
	private void CloseBall()
	{
		pokemonBall.GetComponent<Animator>().SetBool("Open", false);
	}
	private void HandOverStats(Pokemon pokemon, PlayerPokemonData data)
	{
		bool isCaptured = data.isCaptured;
		int trainerID = GetComponent<PhotonView>().viewID;
		string trainersName = GetComponent<PlayerCharacter>().playersName;
		string nickName = data.nickName;
		bool isFromTrade = data.isFromTrade;
		int level = data.level;
		int gender = (int)data.gender;
		int nature = (int)data.nature;
		int curHP = data.curHP;
		int hpEV = data.hpEV;
		int atkEV = data.atkEV;
		int defEV = data.defEV;
		int spatkEV = data.spatkEV;
		int spdefEV = data.spdefEV;
		int spdEV = data.spdEV;
		int hpIV = data.hpIV;
		int atkIV = data.atkIV;
		int defIV = data.defIV;
		int spatkIV = data.spatkIV;
		int spdefIV = data.spdefIV;
		int spdIV = data.spdIV;
		int curEXP = data.currentEXP;
		string[] MovesToLearnNames = new string[data.MovesToLearnNames.Count];
		for(int i = 0; i < data.MovesToLearnNames.Count; i++)
		{
			MovesToLearnNames[i] = data.MovesToLearnNames[i];
		}
		string[] KnownMovesNames = new string[data.KnownMovesNames.Count];
		for(int i = 0; i < data.KnownMovesNames.Count; i++)
		{
			KnownMovesNames[i] = data.KnownMovesNames[i];
		}
		//string equippedItem = data.equippedItem.name;
		int origin = data.origin;
		int pokemonID = pokemon.gameObject.GetComponent<PhotonView>().viewID;
		GetComponent<PhotonView>().RPC("NetworkStats", PhotonTargets.AllBuffered, isCaptured, trainerID, trainersName, nickName, isFromTrade, level, gender,
		                               nature, curHP, hpEV, atkEV, defEV, spatkEV, spdefEV, spdEV, hpIV, atkIV, defIV, spatkIV, spdefIV, spdIV, curEXP,
		                               MovesToLearnNames, KnownMovesNames, origin, pokemonID);
	}
	private void GetStatsBack(Pokemon pokemon, PlayerPokemonData data)
	{
		data.isCaptured = pokemon.isCaptured;
		data.nickName = pokemon.nickName;
		data.isFromTrade = pokemon.isFromTrade;
		data.level = pokemon.level;
		data.gender = pokemon.gender;
		data.nature = pokemon.nature;
		data.curHP = pokemon.maxHP;
		data.hpEV = pokemon.hpEV;
		data.atkEV = pokemon.atkEV;
		data.defEV = pokemon.defEV;
		data.spatkEV = pokemon.spatkEV;
		data.spdefEV = pokemon.spdefEV;
		data.spdEV = pokemon.spdEV;
		data.hpIV = pokemon.hpIV;
		data.atkIV = pokemon.atkIV;
		data.defIV = pokemon.defIV;
		data.spatkIV = pokemon.spatkIV;
		data.spdefIV = pokemon.spdefIV;
		data.spdIV = pokemon.spdIV;
		data.currentEXP = pokemon.currentEXP;
		data.MovesToLearnNames = pokemon.MovesToLearnNames;
		data.KnownMovesNames = pokemon.KnownMovesNames;
		//string equippedItem = data.equippedItem.name;
		data.origin = pokemon.origin;
	}
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
	{
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;
		
		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;
		
		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;
		
		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)
		
		return result;
	}

	public IEnumerator PokemonGo(PlayerPokemonData data)
	{
		anim.SetBool("ThrowPokemonBall", true);
		yield return new WaitForSeconds(0.10f);
		CreatePokemonBall();
		while(Vector3.Distance(transform.position, pokemonBall.transform.position) < 5.0f)
		{
			yield return null;
		}
		pokemonBall.rigidbody.velocity = Vector3.zero;
		pokemonBall.transform.LookAt(transform.forward);
		OpenBall();
		pokemonBall.audio.PlayOneShot(pokemonOut);
		yield return new WaitForSeconds(pokemonOut.length);
		float current_terrain_height = Terrain.activeTerrain.SampleHeight(pokemonBall.transform.position);
		Vector3 here = new Vector3(pokemonBall.transform.position.x, current_terrain_height, pokemonBall.transform.position.z);
		GameObject pokemon = PhotonNetwork.Instantiate(data.pokemonName.ToString(), here, Quaternion.identity, 0) as GameObject;
		pokemon.GetComponent<WildPokemonAI>().enabled = false;
		CloseBall();
		while(Vector3.Distance(pokemonBall.transform.position, grip.transform.position) > 1f){
			pokemonBall.transform.position = Vector3.Lerp(pokemonBall.transform.position, grip.transform.position, 5f * Time.deltaTime);
			yield return null;
		}
		pokemonBall.transform.parent = grip.transform;
		pokemonBall.transform.position = grip.transform.position;
		pokemonBall.transform.rotation = grip.transform.rotation;
		pokemonBall.SetActive(false);
		HandOverStats(pokemon.GetComponent<Pokemon>(), data);
		pokemon.GetComponent<Pokemon>().SetupSetupPokemon();
		pokemon.GetComponent<PokemonInput>().myCamera = GetComponent<PlayerInput>().myCamera;
		int poke = pokemon.GetComponent<PhotonView>().viewID;
		GetComponent<PhotonView>().RPC("SetActivePokemon", PhotonTargets.AllBuffered, poke);
		input.throwingPokeBall = false;
		yield return null;
	}
	public IEnumerator PokemonReturn()
	{
		transform.LookAt(player.activePokemon.transform);
		anim.SetBool("StartPokemonReturn", true);
		CreatePokemonBall();
		//ANIMATIONS AND EFFECTS GO HERE FOR BRINGING THE POKEMON BACK INTO THE BALL-------------!!!!!!!!!!!
		Pokemon pokemon = player.activePokemon.GetComponent<Pokemon>();
		int slot = pokemon.origin;
		GetStatsBack(pokemon, player.pokemonRoster.pokemonRoster[slot]);
		PhotonNetwork.Destroy(player.activePokemon);

		yield return new WaitForSeconds(1f);
		anim.SetBool("FinishPokemonReturn", true);
		input.returningPokemon = false;
		yield return null;
	}
	public IEnumerator PokeBallGo()
	{
		target = GetComponent<PlayerInput>().target;
		anim.SetBool("ThrowEmptyBall", true);
		yield return new WaitForEndOfFrame();
		anim.SetBool("ThrowEmptyBall", false);
		yield return null;
	}
}
