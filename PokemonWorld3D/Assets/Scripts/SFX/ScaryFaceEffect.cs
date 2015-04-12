using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScaryFaceEffect : MonoBehaviour
{
	public Image scaryFace;	
	public float time = 1.5f;

	void Update()
	{
		if(scaryFace.color.a > 0)
		{
			var temp = scaryFace.color;
			temp.a -= Time.deltaTime / time;
			scaryFace.color = temp;
		}
		if(scaryFace.color.a <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
