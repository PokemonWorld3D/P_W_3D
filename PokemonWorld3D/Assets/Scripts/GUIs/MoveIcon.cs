using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoveIcon : MonoBehaviour
{
	public Move move;
	public Image border;
	public Image icon;
	public Image timer;
	public Text pp;
	public List<Color> moveTypes;

	void Update()
	{
		timer.fillAmount = move.coolingDown / move.coolDown;
	}
	public void SetupIcon(Move _move)
	{
		move = _move;
		border.color = moveTypes[(int)move.type];
		icon.sprite = move.icon;
		timer.fillAmount = move.coolingDown / move.coolDown;
		pp.text = move.ppCost.ToString();
	}
}
