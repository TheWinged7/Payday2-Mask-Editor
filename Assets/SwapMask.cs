using UnityEngine;
using System.Collections;

public class SwapMask : MonoBehaviour {


	public GameObject[] masks;
	public Texture [] textures, patterns; 	//array storing all models and textures respectivley
	public Color [] patternMain, patternSecond;
	private int modelNumber, textureNumber, patternNumber; 	//index for model, texture, and pattern respectivley
	private GameObject tempMask, currentMask;	//internal model to swap arround



	// Use this for initialization
	void Start () {
		modelNumber = 0; 	//set index to start
		textureNumber = 0;	//set index to start
		patternNumber = 0;	//set index to start
		currentMask= (GameObject)  Object.Instantiate (masks[modelNumber], transform.position, transform.rotation); //load default mask
		currentMask.transform.parent = transform;	//set mask to match parent location and rotation
		currentMask.SetActive (true);				//set mask to visible


	}
	
	// Update is called once per frame
	void Update () {
	

		if(Input.GetButtonDown("Horizontal")) 	// change this line later to be on mask slection button corresponding it to drop down position
		{
			modelSwap();
		}
		if (Input.GetButtonDown ("Vertical"))  // change this line later to be on texture slection button corresponding it to drop down position
		{
			textureSwap();
		}
		if (Input.GetButtonDown ("Jump"))  // change this line later to be on reset texture slection button
		{
			cleanMask();
		}
		if (Input.GetButtonDown ("Fire1"))  // change this line later to be on reset texture slection button
		{
			patternSwap();
		}
	}

	public void cleanMask()
	{
		//reload same mask to set it back to same material
		int temp = (modelNumber - 1);
		if (temp <0 ) { temp = masks.Length-1;}
		tempMask = (GameObject) Instantiate(masks[temp], transform.position, transform.rotation);
		Destroy(currentMask);
		tempMask.transform.parent = transform;
		currentMask = tempMask;
	}

	public void modelSwap()
	{
		tempMask = (GameObject) Instantiate(masks[modelNumber], transform.position, transform.rotation);
		Destroy(currentMask);
		tempMask.transform.parent = transform;
		currentMask = tempMask;
		//below here is to be removed later, only for testing
		//model number will later be set by drop-down list selection
		modelNumber++;
		modelNumber = modelNumber % masks.Length;
	}

	public void textureSwap()
	{
		currentMask.GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[textureNumber];
		//below here is to be removed later, only for testing
		//texture number will later be set by drop-down list selection
		textureNumber++;
		textureNumber = textureNumber % textures.Length;

	}
	
	public void patternSwap()
	{
		if (patternNumber >= patterns.Length) {
			currentMask.GetComponentInChildren<MeshRenderer> ().material.DisableKeyword ("_DETAIL_MULX2");
			patternNumber = 0;
		} else {
			currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailMask", patterns [patternNumber]);
			currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailAlbedoMap", patterns [patternNumber]);
			currentMask.GetComponentInChildren<MeshRenderer> ().material.EnableKeyword ("_DETAIL_MULX2");
			patternNumber++;
		}

	}

}
