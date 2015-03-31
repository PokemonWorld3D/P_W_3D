﻿using UnityEngine;
using System.Collections;

public class LightningCreator : MonoBehaviour
{
	public Lightning lightning;

	private IEnumerator Start()
	{
		while(true)
		{
			Instantiate(lightning, transform.position, transform.rotation);
			//Instantiate(lightning, transform.position, transform.rotation);
            //Instantiate(lightning, transform.position, transform.rotation);
//			yield return null;
			yield return new WaitForSeconds(1.0f);
		}
	}
}
