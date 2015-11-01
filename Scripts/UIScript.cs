using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	//Public Variables
	public float healthFloat = 100;
	public float fuelFloat = 100;
	public float maxFuelFloat = 100;
	public float resourcesFloat  = 100;
	public float maxHealthFloat = 100;
	public bool placedBool;

	//variables for internal use
	GameObject fProgress, hProgress, rProgress, fText, hText, rText;
	float fFillFloat, hFillFloat, rFillFloat; 
	bool debugFloat = false;
	float deltaTime = 0.0f;


	// Use this for initialization
	void Start () {
	
		//Get the progress bar objects 
		fProgress = GameObject.Find ("fProgress");
		hProgress = GameObject.Find ("hProgress");
		rProgress = GameObject.Find ("rProgress");

		//Get the text objects
		fText = GameObject.Find ("fText");
		hText = GameObject.Find ("hText");
		rText = GameObject.Find ("rText");

	


	}
	
	// Update is called once per frame
	void Update () {

		//Test Function
		TestFunction ();

	
		//Update Fuel Bar 
		FillFunction (fuelFloat, fProgress, maxFuelFloat);

		//Update Health Bar 
		FillFunction (healthFloat, hProgress, maxHealthFloat);

		//Update resources Bar 
		FillFunction (resourcesFloat, rProgress, 100.0f);


		//Update text objects
		TextFunction (fuelFloat, fText, "F : ", maxFuelFloat);
		TextFunction (healthFloat, hText, "H : ", maxHealthFloat);
		TextFunction (resourcesFloat, rText, "R : ", 100.0f);

		//Debug Info 
		if (Input.GetKey(KeyCode.Home)) {

			if (debugFloat == false) {
				debugFloat = true;
			}
			else {
				debugFloat = false;
			}
		}

		//Calculate FPS and Frame Delay.
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		//Draw minimap 
		DrawMinimap ();
	}


	//Calculate fill value and apply value to game object 
	void FillFunction (float valueFloat, GameObject fillObject, float maxF) {

		float fillFloat;

		fillFloat = valueFloat / maxF;

		fillObject.GetComponent<Image> ().fillAmount = fillFloat; 
	}

	//Update text objects
	void TextFunction(float textFloat, GameObject textObject, string typeString, float maxT) {

		if (textFloat > maxT) {
			textFloat = maxT;
		}

		textObject.GetComponent<Text> ().text = typeString + textFloat.ToString ("0");
	}


	//Modify values (for testing/debug purposes only) 
	void TestFunction () {

		//-- Test -- 
		if (Input.GetKey(KeyCode.F1)) {
			healthFloat = healthFloat +1 ;
		}
		
		if (Input.GetKey(KeyCode.F2)) {
			healthFloat = healthFloat - 1;
		}

		if (Input.GetKey(KeyCode.F3)) {
			fuelFloat  = fuelFloat + 1 ;
		}
		if (Input.GetKey(KeyCode.F4)) {
			fuelFloat = fuelFloat  - 1;
		}

		if (Input.GetKey(KeyCode.F5)) {
			resourcesFloat = resourcesFloat + 1 ;
		}
		
		if (Input.GetKey(KeyCode.F6)) {
			resourcesFloat = resourcesFloat - 1;
		}
	}

	//Draw items onto GUI 
	void OnGUI() 
	{
		//Draw Debug Info 
		if (debugFloat == true) 
		{
			//Get Screen Height and Width
			int w = Screen.width, h = Screen.height;

			//Create New Style 
			GUIStyle style = new GUIStyle();

			//Set style and rectangle 
			Rect rect = new Rect(0, 0, w, h * 2 / 100);
			Rect mouserect = new Rect(0, 20, w, h * 2 / 100);
			Rect playerrect = new Rect(0,40,w,h*2/100);
			Rect buildrect = new Rect (0,60,w,h*2/100);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = h * 2 / 100;
			style.normal.textColor = Color.red; 
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;

			//Create string 
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			string mtext = "X : " + Input.mousePosition.x.ToString() +  " Y : " + Input.mousePosition.y.ToString();
			string ptext = "Player Position (X,Y,Z) : " + transform.position.ToString() + "     FOV : " + 
				             GameObject.Find("Cube").GetComponent<CharacterMove>().FOVFloat.ToString("0");
			string btext = "Project Flare : Alpha Build # 002";

			//Print to Screen 
			GUI.Label(rect, text, style);
			GUI.Label(mouserect, mtext, style);
			GUI.Label(playerrect, ptext, style);
			GUI.Label(buildrect, btext, style);

		}
	}

	//Draw minimap to rawimage UI component 
	void DrawMinimap () {
	}
}
