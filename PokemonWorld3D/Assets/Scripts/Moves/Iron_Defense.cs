using UnityEngine;
using System.Collections;

public class Iron_Defense : Move
{
	public SkinnedMeshRenderer skinMeshRenderer;
	public int slotInRendererMaterials;
	public Texture ironTexture;

	public IEnumerator StartIronDefense()
	{
//		Material[] curMats = renderer.materials;
//		curMats[slotInRendererMaterials] = ironMat;
//		renderer.materials = curMats;

		Material[] materials = skinMeshRenderer.materials;
		Material mat = materials[slotInRendererMaterials];
		mat.SetTexture("_Texture2", ironTexture);
		float counter = mat.GetFloat("_Blend");
		while(counter != 1f){
			float increase = mat.GetFloat("_Blend") + Time.deltaTime;
			if(increase > 1.0f)
				increase = 1.0f;
			mat.SetFloat("_Blend", increase);
			counter += increase;
			yield return null;
		}
	}
	public void FinishIronDefense()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}