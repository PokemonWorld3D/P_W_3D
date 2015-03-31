using UnityEngine;
using System.Collections;

public class NetworkCaterpie : Photon.MonoBehaviour
{
	private Vector3 updatePos;
	private Quaternion updateRot;
	private AudioClip currentAudio;
	private AudioClip updateAudio;
	private Animator anim;
	private float speed = 0.0f;
	private bool jumping = false;
	private bool falling = false;
	private bool inBattle = false;
	private bool fainting = false;
	private bool evolving = false;
	private bool tackle = false;
	private bool stringShot = false;
	private bool bugBite = false;

	void Start()
	{
		updatePos = transform.position;
		updateRot = transform.rotation;
		anim = GetComponent<Animator>();
	}
	void Update()
	{
		if(audio.isPlaying)
			currentAudio = audio.clip;
		if(photonView.isMine)
		{

		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, updatePos, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, updateRot, 0.1f);
			audio.PlayOneShot(updateAudio);
			anim.SetFloat("Speed", speed);
			anim.SetBool("Jumping", jumping);
			anim.SetBool("Falling", falling);
			anim.SetBool("InBattle", inBattle);
			anim.SetBool("Fainting", fainting);
			anim.SetBool("Evolving", evolving);
			anim.SetBool("Tackle", tackle);
			anim.SetBool("StringShot", stringShot);
			anim.SetBool("BugBite", bugBite);
		}
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(currentAudio);
			stream.SendNext(anim.GetFloat("Speed"));
			stream.SendNext(anim.GetBool("Jumping"));
			stream.SendNext(anim.GetBool("Falling"));
			stream.SendNext(anim.GetBool("InBattle"));
			stream.SendNext(anim.GetBool("Fainting"));
			stream.SendNext(anim.GetBool("Evolving"));
			stream.SendNext(anim.GetBool("Tackle"));
			stream.SendNext(anim.GetBool("StringShot"));
			stream.SendNext(anim.GetBool("BugBite"));
		}
		else
		{
			updatePos = (Vector3)stream.ReceiveNext();
			updateRot = (Quaternion)stream.ReceiveNext();
			updateAudio = (AudioClip)stream.ReceiveNext();
			speed = (float)stream.ReceiveNext();
			jumping = (bool)stream.ReceiveNext();
			falling = (bool)stream.ReceiveNext();
			inBattle = (bool)stream.ReceiveNext();
			fainting = (bool)stream.ReceiveNext();
			evolving = (bool)stream.ReceiveNext();
			tackle = (bool)stream.ReceiveNext();
			stringShot = (bool)stream.ReceiveNext();
			bugBite = (bool)stream.ReceiveNext();
		}
	}
}
