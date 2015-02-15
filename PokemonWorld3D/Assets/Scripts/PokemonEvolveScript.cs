using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonEvolveScript : MonoBehaviour {

	public GameObject evolveInto;

	public Camera mainCam;
	private bool coroutineStarted = false;
	private Renderer[] renderersArray;
	private List<Material> materialsList = new List<Material>();
	private GameObject evolvedForm;
	private GameObject armature;
	public LensFlare evolveFlare;
	public float evolveFlareSize;
	public string thisPokemonName;
	public float spawnPosX;
	public float spawnPosY;
	public float spawnPosZ;

	void Start(){
		renderersArray = this.gameObject.GetComponentsInChildren<Renderer>();
//		armature = this.gameObject.transform.GetChild(1).gameObject;
//		evolveFlare = this.gameObject.transform.GetChild(2).gameObject.GetComponent<LensFlare>();
	}
	void Update(){
		if(!coroutineStarted){
			coroutineStarted = true;
			StartCoroutine(Evolve());
		}
	}

	private IEnumerator Evolve(){
		foreach(Renderer renderer in renderersArray){
			for(int i = 0; i < renderer.materials.Length; i++){
				if(renderer.materials[i].shader.name == "Toon/Basic Blender"){
					materialsList.Add(renderer.materials[i]);
				}
			}
		}
//		armature.SetActive(false);
		evolveFlare.enabled = true;
		foreach(Material material in materialsList){
			StartCoroutine(ChangeToWhite(material));
		}
		yield return new WaitForSeconds(1);
		animation.Play(thisPokemonName + "_Evolve");
		while(this.animation.isPlaying){
			yield return null;
		}
		StartCoroutine(IncreaseFlare());
		while(evolveFlare.brightness < evolveFlareSize - 1f){
			yield return null;
		}
		//Vector3 spawnPos = new Vector3(this.transform.position.x + spawnPosX, this.transform.position.y + spawnPosY, this.transform.position.z + spawnPosZ);
		evolvedForm = Instantiate(evolveInto, this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
		Renderer[] evolvedFormsRenderersArray = evolvedForm.gameObject.GetComponentsInChildren<Renderer>();
		List<Material> evolvedFormsMaterialsList = new List<Material>();
		evolvedForm.SetActive(false);
		foreach(Renderer renderer in evolvedFormsRenderersArray){
			for(int i = 0; i < renderer.materials.Length; i++){
				if(renderer.materials[i].shader.name == "Toon/Basic Blender"){
					evolvedFormsMaterialsList.Add(renderer.materials[i]);
				}
			}
		}
		foreach(Material material in evolvedFormsMaterialsList){
			material.SetFloat("_Blend", 1f);
		}
		evolvedForm.SetActive(true);
		SkinnedMeshRenderer componenets = this.gameObject.GetComponent<SkinnedMeshRenderer>(); //this.gameObject.transform.GetChild(0).gameObject;
		componenets.enabled = false;
		while(evolveFlare.brightness > 0f){
			yield return null;
		}
		foreach(Material material in evolvedFormsMaterialsList){
			StartCoroutine(ChangeToColor(material));
		}
		yield return new WaitForSeconds(1);
		Destroy(this.gameObject);
		yield return null;
	}

	private IEnumerator ChangeToWhite(Material mat){
		float counter = mat.GetFloat("_Blend");
		while(counter != 1f){
			float increase = mat.GetFloat("_Blend") + Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter += increase;
			yield return null;
		}
 	}
	private IEnumerator ChangeToColor(Material mat){
		float counter = mat.GetFloat("_Blend");
		while(counter != 0f){
			float increase = mat.GetFloat("_Blend") - Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter -= increase;
			yield return null;
		}
	}
	private IEnumerator IncreaseFlare(){
		float increase = 0.05f + Time.deltaTime;
		while(evolveFlare.brightness < evolveFlareSize){
			evolveFlare.brightness = evolveFlare.brightness + increase;
			yield return null;
		}
		float decrease = 0.075f + Time.deltaTime;
		while(evolveFlare.brightness > 0f){
			evolveFlare.brightness = evolveFlare.brightness - decrease;
			yield return null;
		}
	}
	
}
