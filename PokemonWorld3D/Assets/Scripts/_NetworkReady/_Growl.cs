using UnityEngine;
using System.Collections;

public class _Growl : _Move
{

	public IEnumerator GrowlEffect()
	{
		yield return new WaitForSeconds(2.0f);
		MoveResults();
	}
}
