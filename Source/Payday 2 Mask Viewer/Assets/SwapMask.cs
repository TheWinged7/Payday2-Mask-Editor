using UnityEngine;
using System.Collections;


/*
QUICK FUCNTION JUMP GUIDE
copy-paste pattern next to label to find that function quickly

	display main GUI overlay:			+++++++
	show mask selection menu:			++++++-
	show mask material menu:			+++++-+
	show pattern selection menu:		++++-++
	show primary pattern colour menu:	+++-+++
	show secondary pattern colour menu:	++-++++
	reset mask to default:				+-+++++
	change to mask model "N":			-++++++
	change to material "N":				+++++--
	change to mask pattern "N"			++++-+-
	show next material menu page:		+++-++-
	show previous material menu page:	++-+++-

*/

public class SwapMask : MonoBehaviour {
	
	
	public GameObject[] masks;
	public Texture [] textures;
	public Texture2D[] patterns; 	//array storing all models and textures respectivley
	public Color32 patternMain, patternSecond;
	public GameObject mainMenu, maskMenu, materialMenu, patternMenu;//, colourMenu;
	public GameObject [] maskPages, materialPages, patternPages;
	
	private int modelNumber, textureNumber, patternNumber; 	//index for model, texture, and pattern respectivley
	private int maskPageNumber, materialPageNumber, patternPageNumber;
	private GameObject tempMask, currentMask;	//internal model to swap arround
	private Texture2D currentPattern;
	private Color32 defaultRed, defaultGreen;
	
	
	
	// Use this for initialization
	void Start () {
		modelNumber = 0; 	//set index to start
		textureNumber = 0;	//set index to start
		patternNumber = 0;	//set index to start
		maskPageNumber = 0; //set mask page number to first page
		materialPageNumber = 0; //set material page number to first page
		patternPageNumber = 0; //set pattern page number to first page


		defaultRed=new Color32(255,0,7,255);
		defaultGreen=new Color32(0,255,18,255);

		patternMain = defaultRed;
		patternSecond = defaultGreen;

		currentMask= (GameObject)  Object.Instantiate (masks[modelNumber], transform.position, transform.rotation); //load default mask
		currentMask.transform.parent = transform;	//set mask to match parent location and rotation
		currentMask.transform.localScale = new Vector3(1,1,1);
		currentMask.SetActive (true);				//set mask to visible
		
		mainView ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	//closes all open menus, then pulls up hud for viewing the mask
	private void mainView()		//+++++++
	{
		//close all other open menus
		maskMenu.SetActive (false);
		materialMenu.SetActive (false);
		patternMenu.SetActive (false);
		//others to go here soon

		//pull up base menu
		mainMenu.SetActive (true);
		currentMask.SetActive(true) ; //show mask
	}
	
	//closes all other open menus and hud, then pulls up mask selection menu
	//then pulls up previously open (or first if none previously opened) page in the menu and hides all others
	public void showMaskMenu()	//++++++-
	{
		currentMask.SetActive(false) ; //hide mask while menu is 
		mainMenu.SetActive (false);//hide base menu
		maskMenu.SetActive (true);//pull up menu of all masks to pick from either as names, or images with name underneath
		patternMenu.SetActive (false);
	}
	
	//closes all other open menus and hud,then pulls up texture selection menu
	//then pulls up previously open (or first if none previously opened) page in the menu and hides all others
	public void showTextureMenu()	//+++++-+
	{
		
		currentMask.SetActive(false) ; //hide mask while menu is up
		mainMenu.SetActive (false);//hide base menu
		materialMenu.SetActive (true);//pull up menu of all masks to pick from either as names, or images with name underneath
		patternMenu.SetActive (false);
		
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
	public void showPatternMenu()		//++++-++
	{
	
		currentMask.SetActive(false) ; //hide mask while menu is up
		mainMenu.SetActive (false);//hide base menu
		materialMenu.SetActive (false);//pull up menu of all masks to pick from either as names, or images with name underneath
		patternMenu.SetActive (true);

		foreach (GameObject page in patternPages) //itterate through all menu pages
		{
			if (page== patternPages[patternPageNumber])	//if it matches the current selected:
			{
				page.SetActive (true);	//set panel to visible
			}
			else 											//if it does not match
			{
				page.SetActive (false); //set panel to invisible
			}
		}
	}


	public void showColourPicker()		//+++-+++
	{
		//colour picker for primary pattern colour to go here

		//when implimented, after selecting a colour:
			//+re-load current texture from array
			//+re-colour it according to chosen colour
			//if a pattern is selected, apply it to the mask
	}



	//resets mask to default material pattern and colour
	public void cleanMask() 	//+-+++++
	{
		tempMask = (GameObject) Instantiate(masks[modelNumber], transform.position, transform.rotation);
		Destroy(currentMask);
		tempMask.transform.parent = transform;
		tempMask.transform.localScale = new Vector3(1,1,1);
		currentMask = tempMask;
	}
	
	public void modelSwap(int n)	//-++++++
	{
		mainView(); //set to the main view and close all menus
		
		modelNumber=n;		//set the model number to the one sent to the function
		tempMask = (GameObject) Instantiate(masks[modelNumber], transform.position, transform.rotation); //instantiate temporary object with new model
		Destroy(currentMask);		//destroy current model

		tempMask.transform.parent = transform;	
		tempMask.transform.localScale = new Vector3(1,1,1);

		 //not working, should scale model up to full sized in stead of 0.03 of size

		currentMask = tempMask;	//set temp mask as the current mask


	}
	
	public void textureSwap(int n)		//+++++--
	{
		mainView();
		
		textureNumber = n;
		currentMask.GetComponentInChildren<MeshRenderer> ().material.mainTexture = textures[textureNumber];
		
	}
	
	public void patternSwap(int n)		//++++-+-
	{
		mainView();
		
		patternNumber = n;
		currentPattern = patterns [patternNumber];

		primaryColourSwap (patternMain);
		secondaryColourSwap (patternSecond);
		
		currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailMask", currentPattern);
		currentMask.GetComponentInChildren<MeshRenderer> ().material.SetTexture ("_DetailAlbedoMap", currentPattern);
		currentMask.GetComponentInChildren<MeshRenderer> ().material.EnableKeyword ("_DETAIL_MULX2");
		patternNumber++;
		
	}




	public void primaryColourSwap(Color32 primary)
	{
		currentPattern = patterns [patternNumber];

		patternMain = primary;


		Color[] pixels = currentPattern.GetPixels (0);


		for (int i=0; i< pixels.Length; i++)
		{
			if (pixels[i] == defaultRed)
			{
				pixels[i]=patternMain;
			}

		}

		currentPattern.SetPixels(pixels);

		currentPattern.Apply();
	}


	public void secondaryColourSwap(Color32 secondary)
	{
		patternSecond = secondary;

		Color[] pixels = currentPattern.GetPixels (0);
		
		
		for (int i=0; i< pixels.Length; i++)
		{
			if (pixels[i] == defaultGreen)
			{
				pixels[i]=patternSecond;
			}
			
		}

	}

	public void dummyColouring()
	{
		primaryColourSwap (new Color32(0,0,255,255));
		secondaryColourSwap (new Color32(255,0,255,255));
	}

	public void nextMaterialPage()		//+++-++-
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

	public void previousMaterialPage()		//++-+++-
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
