using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingDamage : MonoBehaviour
{
	public Text theText;
	public string amount;
	public bool crit;
	public bool italics;
	public Color color;
	public float scroll = 0.5f; 			//HOW FAST TO SCROLL
	public float duration = 5.0f; 			//HOW LONG BEFORE DESTROYING
	public float alpha;
	public bool setup;
	public GameObject thisGameObject;
	
	void Start()
	{
		setup = false;
		crit = false;
		alpha = 1;
	}
	
	void Update()
	{
		if(!setup)
		{
			theText.text = amount;
			theText.color = color;
			if(crit)
			{
				theText.fontStyle = FontStyle.BoldAndItalic;
				theText.fontSize = 18;
			}
		}
		if (alpha > 0)
		{
			Vector3 curPos = transform.position;
			curPos.y += scroll * Time.deltaTime;
			transform.position = curPos;
			alpha -= Time.deltaTime / duration;
			Color curColor = theText.color;
			curColor.a = alpha;
			theText.color = color = curColor;
		}
		else
		{
			PhotonNetwork.Destroy(thisGameObject);
		}
	}
}
