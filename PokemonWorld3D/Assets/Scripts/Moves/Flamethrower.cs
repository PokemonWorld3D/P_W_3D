using UnityEngine;
using System.Collections;

public class Flamethrower : Move
{
	public Transform instantiatePoint;
	public ParticleSystem flamethrower;
	
	public void FinishFlamethrower()
	{
		flamethrower.Stop();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	public IEnumerator FlamethrowerEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			flamethrower.Play();
			MoveResults();
			yield return null;
//			GameObject flamethrower = PhotonNetwork.Instantiate("Flamethrower", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
//			yield return new WaitForSeconds(2.0f);
//			MoveResults();
//			yield return new WaitForSeconds(15.0f);
//			PhotonNetwork.Destroy(flamethrower);
		}
	}
}
