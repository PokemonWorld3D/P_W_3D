using UnityEngine;
using System.Collections;

public class String_Shot : Move
{
	public void FinishStringShot()
	{
		GetComponent<Animator>().SetBool(moveName, false);
	}
}
