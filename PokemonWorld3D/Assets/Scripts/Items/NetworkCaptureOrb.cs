using UnityEngine;
using System.Collections;

public class NetworkCaptureOrb : Photon.MonoBehaviour
{
	private Vector3 updatePos = Vector3.zero;
	private Quaternion updateRot = Quaternion.identity;
	private string currentAnimation;
	private string updateAnimation;
	private AudioClip currentAudio;
	private AudioClip updateAudio;

	void Update()
	{
		currentAnimation = GetCurrentAnimation();
		currentAudio = audio.clip;
		if(photonView.isMine)
		{
			
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, updatePos, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, updateRot, 0.1f);
			animation.CrossFade(updateAnimation);
			audio.PlayOneShot(updateAudio);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(currentAnimation);
			stream.SendNext(currentAudio);
		}
		else
		{
			updatePos = (Vector3)stream.ReceiveNext();
			updateRot = (Quaternion)stream.ReceiveNext();
			updateAnimation = (string)stream.ReceiveNext();
			updateAudio = (AudioClip)stream.ReceiveNext();
		}
	}
	
	private string GetCurrentAnimation()
	{
		foreach(AnimationState a in animation)
		{
			if(animation.IsPlaying(a.name))
			{
				return a.name;
			}
		}
		return string.Empty;
	}
}
