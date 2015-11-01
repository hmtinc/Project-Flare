using UnityEngine;
using System.Collections;

public class LampPost : MonoBehaviour {


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
	public float rrFloat;


	// Use this for initialization
	void Start () {
		InvokeRepeating("Fuel", 0, timeFloat);

	}
	
	// Update is called once per frame
	void Update () {
	
	
      if(GameObject.Find ("Cube").GetComponent<UIScript> ().fuelFloat <= 0) {
			transform.Find("PL").GetComponent<Light> ().enabled = false;
			transform.Find("PL").GetComponent<SphereCollider>().enabled = false;

		} 
		else {
			transform.Find("PL").GetComponent<Light> ().enabled = true;
			transform.Find("PL").GetComponent<SphereCollider>().enabled = true;


		}


		//Destroy Game Object 
		if (healthFloat <= 0) {
			Destroy(this.gameObject); 
		}


	}

	void Fuel() {

		fuelInt += 1;

		if (fuelInt > 2) {
			GameObject.Find ("Cube").GetComponent<UIScript> ().fuelFloat -= fuelsubtractFloat;
		}
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
			checkBool = true;
			int tagint = ObjectTag.gameObject.layer;
			GameObject cube = GameObject.Find ("Cube");

			if (tagint == 9 && checkInt < 10) {
				Debug.Log ("Destory : " + gameObject.name.ToString () + " @ " + transform.position.ToString ());
				cube.GetComponent<CharacterMove> ().messageObject.SetActive (true);
				cube.GetComponent<UIScript> ().resourcesFloat += rrFloat;
				Destroy (this.gameObject);
			

			}

		//If Trigger Tag is attack then invoke damage 
		if (ObjectTag.tag == "EAttack") {
			
			//Subtract health from player (Get and Modify Health value from UIScript.cs) 
			healthFloat -= 0.01f; 
			
		}
		

	}









}
