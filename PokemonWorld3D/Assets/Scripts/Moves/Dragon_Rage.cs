using UnityEngine;
using System.Collections;

public class Dragon_Rage : Move
{
	public Transform instantiatePoint;
	public float scale;
	public float scaleSpeed;
	
	public void FinishDragonRage()
	{
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	private IEnumerator DragonRageEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject rage = PhotonNetwork.Instantiate("Dragon_Rage", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			float timer = 0.0f;
			//Vector3 curScale = rage.transform.localScale;
			Vector3 desiredScale = new Vector3(scale, scale, scale);
			while(timer <= scaleSpeed)
			{
			//	float newScale = Mathf.Lerp(curScale, scale, timer);
				rage.transform.localScale = Vector3.Lerp(rage.transform.localScale, desiredScale, timer / scaleSpeed);
				timer += Time.deltaTime;
				yield return null;
			}
			Vector3 target_pos = target.transform.position - rage.transform.position;
			target_pos.Normalize();
			rage.rigidbody.AddForce(target_pos * 300.0f);
			while(Vector3.Distance(rage.transform.position, target.transform.position) > 0.1f)
			{
				yield return null;
			}
			//-------------Instantiate the explosion here.---------------------------------//
			PhotonNetwork.Destroy(rage);
		}
		MoveResults();
	}
}
