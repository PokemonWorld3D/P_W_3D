using UnityEngine;
using System.Collections;

public class _Ember : _Move
{
	public Transform instantiatePoint;

	private IEnumerator EmberEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject embers = PhotonNetwork.Instantiate("EmberContainer", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			Vector3 target_pos = target.transform.position - embers.transform.position;
			target_pos.Normalize();
			embers.rigidbody.AddForce(target_pos * 300.0f);
			while(Vector3.Distance(embers.transform.position, target.transform.position) > 0.1f)
			{
				yield return null;
			}
			//-------------Instantiate the explosion here.---------------------------------//
			PhotonNetwork.Destroy(embers);
		}
		MoveResults();
	}
}
