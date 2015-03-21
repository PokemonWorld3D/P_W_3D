using UnityEngine;
using System.Collections;

public class _NetworkPokemonBall : Photon.MonoBehaviour
{
	private Vector3 updatePos = Vector3.zero;
	private Quaternion updateRot = Quaternion.identity;
	private AudioClip currentAudio;
	private AudioClip updateAudio;
	private Animator anim;
	private bool open = false;
	private bool tryingToCatch = false;

	void Start()
	{
		anim = GetComponent<Animator>();
	}
	void Update()
	{
		currentAudio = audio.clip;
		if(photonView.isMine)
		{
			
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, updatePos, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, updateRot, 0.1f);
			audio.PlayOneShot(updateAudio);
			anim.SetBool("Open", open);
			anim.SetBool("TryingToCatch", tryingToCatch);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(currentAudio);
			stream.SendNext(anim.GetBool("Open"));
			stream.SendNext(anim.GetBool("TryingToCatch"));
		}
		else
		{
			updatePos = (Vector3)stream.ReceiveNext();
			updateRot = (Quaternion)stream.ReceiveNext();
			updateAudio = (AudioClip)stream.ReceiveNext();
			open = (bool)stream.ReceiveNext();
			tryingToCatch = (bool)stream.ReceiveNext();
		}
	}
}
