using UnityEngine;
using System.Collections;

public class NetworkPokemon : Photon.MonoBehaviour
{
	private Vector3 updatePos;
	private Quaternion updateRot;
	private AudioClip currentAudio;
	private AudioClip updateAudio;
	private Animator anim;
	private float speed = 0.0f;
//	private bool jumping = false;
//	private bool falling = false;
//	private bool inBattle = false;
//	private bool fainting = false;
//	private bool evolving = false;
//	private bool scratch = false;
//	private bool growl = false;
//	private bool ember = false;
//	private bool smokescreen = false;
	public string[] AnimBools;
	public bool[] AnimBoolValues;

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
			for(int i = 0; i < AnimBools.Length; i++)
			{
				anim.SetBool(AnimBools[i], AnimBoolValues[i]);
			}
//			anim.SetBool("Jumping", jumping);
//			anim.SetBool("Falling", falling);
//			anim.SetBool("InBattle", inBattle);
//			anim.SetBool("Fainting", fainting);
//			anim.SetBool("Evolving", evolving);
//			anim.SetBool("Scratch", scratch);
//			anim.SetBool("Growl", growl);
//			anim.SetBool("Ember", ember);
//			anim.SetBool("Smokescreen", smokescreen);
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
			for(int i = 0; i < AnimBools.Length; i++)
			{
				stream.SendNext(anim.GetBool(AnimBools[i]));
			}
//			stream.SendNext(anim.GetBool("Jumping"));
//			stream.SendNext(anim.GetBool("Falling"));
//			stream.SendNext(anim.GetBool("InBattle"));
//			stream.SendNext(anim.GetBool("Fainting"));
//			stream.SendNext(anim.GetBool("Evolving"));
//			stream.SendNext(anim.GetBool("Scratch"));
//			stream.SendNext(anim.GetBool("Growl"));
//			stream.SendNext(anim.GetBool("Ember"));
//			stream.SendNext(anim.GetBool("Smokescreen"));
		}
		else
		{
			updatePos = (Vector3)stream.ReceiveNext();
			updateRot = (Quaternion)stream.ReceiveNext();
			updateAudio = (AudioClip)stream.ReceiveNext();
			speed = (float)stream.ReceiveNext();
			for(int i = 0; i < AnimBoolValues.Length; i++)
			{
				AnimBoolValues[i] = (bool)stream.ReceiveNext();
			}
//			jumping = (bool)stream.ReceiveNext();
//			falling = (bool)stream.ReceiveNext();
//			inBattle = (bool)stream.ReceiveNext();
//			fainting = (bool)stream.ReceiveNext();
//			evolving = (bool)stream.ReceiveNext();
//			scratch = (bool)stream.ReceiveNext();
//			growl = (bool)stream.ReceiveNext();
//			ember = (bool)stream.ReceiveNext();
//			smokescreen = (bool)stream.ReceiveNext();
		}
	}
}
