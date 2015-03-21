using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _Scratch : _Move
{
	public TrailRenderer claws;
	
	public void StartScratchEffect()
	{
		claws.enabled = true;
	}
	public void EndScratchEffect()
	{
		claws.enabled = false;
	}
	public void ScratchDamage()
	{
		MoveResults();
	}
	public void FinishScratch()
	{
		GetComponent<Animator>().SetBool(moveName, false);
	}
}
