using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusEffectIcon : MonoBehaviour
{
	public Image icon;
	public Text timer;
	public float duration = 999.0f;

	void Update()
	{
		timer.text = ((int)duration).ToString();
		duration -= Time.deltaTime;
		if(duration <= 0.0f)
			Destroy(gameObject);
	}
		
	public void SetupIcon(Sprite _icon, float _duration)
	{
		icon.sprite = _icon;
		duration = _duration;
	}
}
