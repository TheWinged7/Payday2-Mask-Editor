using UnityEngine;
using System.Collections;

public class SwapMask : MonoBehaviour {


	public GameObject[] masks;
	public Texture [] textures;
	public Texture2D[] patterns; 	//array storing all models and textures respectivley
	public Color [] patternMain, patternSecond;
	public GameObject mainMenu, maskMenu;//, patternMenu, materialeMenu, colourMenu;

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

		mainMenu.SetActive (true);
		maskMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	

	}


	private void mainView()
	{
		//close all other open menus
		maskMenu.SetActive (false);
			//others to go ehre soon
		//pull up base menu
		mainMenu.SetActive (true);
		currentMask.SetActive(true) ; //show mask
	}

	
	public void showMaskMenu()
	{
		currentMask.SetActive(false) ; //hide mask while menu is up
		mainMenu.SetActive (false);//hide base menu
		maskMenu.SetActive (true);//pull up menu of all masks to pick from either as names, or images with name underneath
	}

	public void showTextureMenu()
	{
		currentMask.GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[textureNumber];
		//below here is to be removed later, only for testing
		//texture number will later be set by drop-down list selection
		textureNumber++;
		textureNumber = textureNumber % textures.Length;

	}
	
	public void showPatternMenu()
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


	
	public void cleanMask() //resets mask to default material pattern and colour
	{
		tempMask = (GameObject) Instantiate(masks[modelNumber], transform.position, transform.rotation);
		Destroy(currentMask);
		tempMask.transform.parent = transform;
		currentMask = tempMask;
	}
	
	public void modelSwap(int n)
	{
		modelNumber=n;
		tempMask = (GameObject) Instantiate(masks[modelNumber], transform.position, transform.rotation);
		Destroy(currentMask);
		tempMask.transform.parent = transform;
		currentMask = tempMask;
		
		mainView();
	}
	
	public void textureSwap(int n)
	{
		if (n == -1) 
		{
			cleanMask();
		} 
		else 
		{
			textureNumber = n;
			currentMask.GetComponentInChildren<MeshRenderer> ().material.mainTexture = textures [textureNumber];
		}
		//below here is to be removed later, only for testing
		//texture number will later be set by drop-down list selection
		//	textureNumber++;
		//	textureNumber = textureNumber % textures.Length;
		
	}
	
	public void patternSwap(int n)
	{
		patternNumber = n;
		if (patternNumber ==-1) {
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
