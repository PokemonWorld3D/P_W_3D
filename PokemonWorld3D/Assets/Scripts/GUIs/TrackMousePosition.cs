using UnityEngine;
using System.Collections;

public class TrackMousePosition : MonoBehaviour
{
	void Update()
	{
		transform.position = Input.mousePosition;
	}
}
