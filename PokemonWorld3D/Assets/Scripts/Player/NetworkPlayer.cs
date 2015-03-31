using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour
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
	private bool throwPokemonBall = false;
	private bool throwEmptyBall = false;

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
			anim.SetBool("ThrowPokemonBall", throwPokemonBall);
			anim.SetBool("ThrowEmptyBall", throwEmptyBall);
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
			stream.SendNext(anim.GetBool("ThrowPokemonBall"));
			stream.SendNext(anim.GetBool("ThrowEmptyBall"));
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
			throwPokemonBall = (bool)stream.ReceiveNext();
			throwEmptyBall = (bool)stream.ReceiveNext();
		}
	}
}
