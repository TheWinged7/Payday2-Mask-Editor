using UnityEngine;
using System.Collections;

public class SwapMask : MonoBehaviour {
	
	
	public GameObject[] masks;
	public Texture [] textures;
	public Texture2D[] patterns; 	//array storing all models and textures respectivley
	public Color [] patternMain, patternSecond;
	public GameObject mainMenu, maskMenu, materialMenu;//, patternMenu, colourMenu;
	public GameObject [] maskPages, materialPages, patternPages;
	
	private int modelNumber, textureNumber, patternNumber; 	//index for model, texture, and pattern respectivley
	private int maskPageNumber, materialPageNumber, patternPageNumber;
	private GameObject tempMask, currentMask;	//internal model to swap arround
	
	
	
	// Use this for initialization
	void Start () {
		modelNumber = 0; 	//set index to start
		textureNumber = 0;	//set index to start
		patternNumber = 0;	//set index to start
		maskPageNumber = 0; //set mask page number to first page
		materialPageNumber = 0; //set material page number to first page
		patternPageNumber = 0; //set pattern page number to first page
		currentMask= (GameObject)  Object.Instantiate (masks[modelNumber], transform.position, transform.rotation); //load default mask
		currentMask.transform.parent = transform;	//set mask to match parent location and rotation
		currentMask.SetActive (true);				//set mask to visible
		
		mainMenu.SetActive (true);
		maskMenu.SetActive (false);
		materialMenu.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	//closes all open menus, then pulls up hud for viewing the mask
	private void mainView() 
	{
		//close all other open menus
		maskMenu.SetActive (false);
		materialMenu.SetActive (false);
		//others to go here soon
		//pull up base menu
		mainMenu.SetActive (true);
		currentMask.SetActive(true) ; //show mask
	}
	
	//closes all other open menus and hud, then pulls up mask selection menu
	//then pulls up previously open (or first if none previously opened) page in the menu and hides all others
	public void showMaskMenu()
	{
		currentMask.SetActive(false) ; //hide mask while menu is 
		mainMenu.SetActive (false);//hide base menu
		maskMenu.SetActive (true);//pull up menu of all masks to pick from either as names, or images with name underneath
		//++++++++++++++++++++++++++++++++++++++++++++++
		//add setting maskpages[maskPageNumber] to active 
		//add settning all other maskPages to inactive
	}
	
	//closes all other open menus and hud,then pulls up texture selection menu
	//then pulls up previously open (or first if none previously opened) page in the menu and hides all others
	public void showTextureMenu()
	{
		
		currentMask.SetActive(false) ; //hide mask while menu is up
		mainMenu.SetActive (false);//hide base menu
		materialMenu.SetActive (true);//pull up menu of all masks to pick from either as names, or images with name underneath
		
		foreach (GameObject page in materialPages) //itterate through all menu pages
		{
			if (page== materialPages[materialPageNumber])	//if it matches the current selected:
			{
				page.SetActive (true);	//set panel to visible
			}
			else 											//if it does not match
			{
				page.SetActive (false); //set panel to invisible
			}
		}
	}
	
	
	//closes all other open menus and hud,then pulls up pattenr selection menu
	//then pulls up previously open (or first if none previously opened) page in the menu and hides all others
	public void showPatternMenu()
	{
		//********************************** below here is temporary and just itterates through patterns
		if (patternNumber >= patterns.Length) {
			currentMask.GetComponentInChildren<MeshRenderer> ().material.DisableKeyword ("_DETAIL_MULX2");
			patternNumber = 0;
		} else {
			currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailMask", patterns [patternNumber]);
			currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailAlbedoMap", patterns [patternNumber]);
			currentMask.GetComponentInChildren<MeshRenderer> ().material.EnableKeyword ("_DETAIL_MULX2");
			patternNumber++;
		}
		//********************************** above here is temporary and just itterates through patterns
		
		
		
		//++++++++++++++++++++++++++++++++++++++++++++++
		//add proper selection of menu items when a menu is implimented
		//add setting maskpages[maskPageNumber] to active 
		//add settning all other maskPages to inactive
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
		mainView(); //set to the main view and close all menus
		
		modelNumber=n;		//set the model number to the one sent to the function
		tempMask = (GameObject) Instantiate(masks[modelNumber], transform.position, transform.rotation); //instantiate temporary object with new model
		Destroy(currentMask);		//destroy current model

		//tempMask.transform.parent = transform;	

		tempMask.transform.localScale = new Vector3(1,1,1); //not working, should scale model up to full sized in stead of 0.03 of size

		currentMask = tempMask;	//set temp mask as the current mask

		Destroy(tempMask);		//destroy temp mask now that we are done with it
	}
	
	public void textureSwap(int n)
	{
		mainView();
		
		textureNumber = n;
		currentMask.GetComponentInChildren<MeshRenderer> ().material.mainTexture = textures[textureNumber];
		
	}
	
	public void patternSwap(int n)
	{
		mainView();
		
		patternNumber = n;
		
		currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailMask", patterns [patternNumber]);
		currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailAlbedoMap", patterns [patternNumber]);
		currentMask.GetComponentInChildren<MeshRenderer> ().material.EnableKeyword ("_DETAIL_MULX2");
		patternNumber++;
		
	}


	public void nextMaterialPage()
	{
		materialPageNumber++;
		if (materialPageNumber >= materialPages.Length) 
		{
			materialPageNumber=0;
		}

		foreach (GameObject page in materialPages) //itterate through all menu pages
		{
			if (page== materialPages[materialPageNumber])	//if it matches the current selected:
			{
				page.SetActive (true);	//set panel to visible
			}
			else 											//if it does not match
			{
				page.SetActive (false); //set panel to invisible
			}
		}
	
	}

	public void previousMaterialPage()
	{
		materialPageNumber--;
		if (materialPageNumber <0) 
		{
			materialPageNumber= materialPages.Length-1;
		}
		
		foreach (GameObject page in materialPages) //itterate through all menu pages
		{
			if (page== materialPages[materialPageNumber])	//if it matches the current selected:
			{
				page.SetActive (true);	//set panel to visible
			}
			else 											//if it does not match
			{
				page.SetActive (false); //set panel to invisible
			}
		}
		
	}

}
