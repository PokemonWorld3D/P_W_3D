using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrowlEffect : MonoBehaviour
{
	public Image growl;	
	public float time = 0.5f;
	
	void Update()
	{
		if(growl.color.a > 0)
		{
			var temp = growl.color;
			temp.a -= Time.deltaTime / time;
			growl.color = temp;
		}
		if(growl.color.a <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
