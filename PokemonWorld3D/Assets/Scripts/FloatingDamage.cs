using UnityEngine;
using System.Collections;

public class FloatingDamage : MonoBehaviour
{
	public Color color;
	public float scroll = 0.05f; 			//HOW FAST TO SCROLL
	public float duration = 1.5f; 			//HOW LONG BEFORE DESTROYING
	public float alpha;
	
	void Start()
	{
		guiText.material.color = color;
		alpha = 1;
	}
	
	void Update()
	{
		if (alpha > 0)
		{
			Vector3 curPos = transform.position;
			curPos.y += scroll * Time.deltaTime;
			transform.position = curPos;
			alpha -= Time.deltaTime / duration;
			Color curColor = guiText.material.color;
			curColor.a = alpha;
			guiText.material.color = curColor;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
