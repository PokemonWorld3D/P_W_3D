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
		}
	}
}
