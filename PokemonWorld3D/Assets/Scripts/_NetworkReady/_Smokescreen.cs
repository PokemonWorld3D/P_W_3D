using UnityEngine;
using System.Collections;

public class _Smokescreen : _Move
{
	public Transform instantiatePoint;

	public IEnumerator SmokescreenEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject smokescreen = PhotonNetwork.Instantiate("Smokescreen", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			yield return new WaitForSeconds(2.0f);
			MoveResults();
			yield return new WaitForSeconds(15.0f);
			PhotonNetwork.Destroy(smokescreen);
		}
	}
}
