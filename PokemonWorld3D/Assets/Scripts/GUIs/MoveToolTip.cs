using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveToolTip : MonoBehaviour
{
	public Image typeIcon;
	public Text moveName;
	public Text movePPCost;
	public Text moveDescription;
	public Text moveCoolDown;
	public Sprite[] typeIcons;

	public void SetToolTipInfo(Move move)
	{
		typeIcon.sprite = typeIcons[(int)(move.type - 1)];
		moveName.text = move.moveName;
		movePPCost.text = "PP COST : " + move.ppCost.ToString();
		moveDescription.text = move.description;
		moveCoolDown.text = "COOL DOWN \n\n" + move.coolDown.ToString() + " s";
	}
}
