using UnityEngine;
using System.Collections;

public class TankScript: MonoBehaviour {


	//Internal Use Variables
	public float healthFloat = 100;
	public float fuelsubtractFloat = 0.5f;
	public Texture2D healthBarTexture, bar2Texture, bar3Texture;
	GUIStyle healthBarStyle = new GUIStyle(); 
	float distanceFloat; 
	public float yFloat  = 120.0f;
	bool checkBool;
	int checkInt, fuelInt;
	public float timeFloat;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	  
		fuelInt += 1;

		if (fuelInt > 2 && checkBool == false) {
			checkBool = true;
			GameObject.Find ("Cube").GetComponent<UIScript> ().maxFuelFloat *= 1.25f;
		}
	
      if(GameObject.Find ("Cube").GetComponent<UIScript> ().fuelFloat <= 0) {
			transform.Find("PL").GetComponent<Light> ().enabled = false;
			transform.Find("PL").GetComponent<SphereCollider>().enabled = false;

		} 
		else {
			transform.Find("PL").GetComponent<Light> ().enabled = true;
			transform.Find("PL").GetComponent<SphereCollider>().enabled = true;


		}


	}

	void Fuel() {




	}

	//GUI Elements Go Here 
	void OnGUI() 
	{
		//Get the position of the enemy on the screen
		Vector2 AIScreenVector = Camera.main.WorldToScreenPoint(transform.position);

		//Get the distance between 2 characters
		distanceFloat = Vector3.Distance (GameObject.Find("Cube").transform.position, transform.position);


			//Set background of the health bar based on health 
			if (healthFloat >= 75) {
				healthBarStyle.normal.background = healthBarTexture;
			}
			else if (healthFloat >= 50) {
				healthBarStyle.normal.background = bar2Texture;
			}
			else {
				healthBarStyle.normal.background = bar3Texture;
			}
			
			//Set Font color 
			healthBarStyle.normal.textColor = Color.green;
			
			//Draw only if ai is 10 units away 
			if (distanceFloat <= 2) 
			{
				GUI.Box (new Rect (AIScreenVector.x - 35, (Screen.height - AIScreenVector.y) - yFloat, 60, 20), "  "
				         + healthFloat.ToString ("0") + "/" + "100",healthBarStyle);
			}





	}

	void OnTriggerStay(Collider ObjectTag) {

		    checkInt += 1;
			
			int tagint = ObjectTag.gameObject.layer;
			GameObject cube = GameObject.Find ("Cube");

			if (tagint == 9 && checkInt < 10) {
				Debug.Log ("Destory : " + gameObject.name.ToString () + " @ " + transform.position.ToString ());
				cube.GetComponent<CharacterMove> ().messageObject.SetActive (true);
				cube.GetComponent<UIScript> ().resourcesFloat += 30;
				Destroy (this.gameObject);
			

			}
		

	}









}
