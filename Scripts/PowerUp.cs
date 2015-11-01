using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	//Inspector Values 
	public GameObject parentObject;


	//Local Variables
	float rotateSpeedFloat = 30f;
	int tempInt, temp2Int; 
	float resourceFloat, fuelFloat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		//Get Number of AI Killed from Cube Object 
		tempInt = GameObject.Find ("Cube").GetComponent<CharacterMove> ().killedAIInteger;

		//Get Number of Placed Buildings 
		temp2Int = GameObject.Find ("Cube").GetComponent<CharacterMove> ().placedBuildingInt;

		//Rotate Power Up 
		transform.Rotate (Vector3.forward * (rotateSpeedFloat) * Time.deltaTime);

		//Determine how much resources to give 
		if (temp2Int < 5) {
			resourceFloat = 10.0f; 
		} else if (temp2Int < 15) {
			resourceFloat = 5.0f;
		} else if (temp2Int < 20) {
			resourceFloat = 2.5f;
		}
		else {
			resourceFloat = 0.5f;
		}

		//Determine how much fuel to give 
		if (tempInt < 20) {
			fuelFloat = 1.0f;
		} else if (tempInt < 30) {
			fuelFloat = 0.75f;
		}
		else{
			fuelFloat = 0.5f;
		}


	
	}

	void OnTriggerStay(Collider col) {

		if (col.gameObject.tag == "Player") {

			if (Random.value < 0.5) {

				//Give Resources
				col.gameObject.GetComponent<UIScript>().resourcesFloat += resourceFloat;
				col.gameObject.GetComponent<UIScript>().resourcesFloat = Mathf.Clamp (col.gameObject.GetComponent<UIScript>().resourcesFloat, 0, 100);

				//Display Message
				DisplayMessage(" + " + resourceFloat.ToString("0") + " R");

			}
			else {

				//Give Fuel
				col.gameObject.GetComponent<UIScript>().fuelFloat += fuelFloat;
				col.gameObject.GetComponent<UIScript>().fuelFloat = Mathf.Clamp (col.gameObject.GetComponent<UIScript>().fuelFloat, 0, col.gameObject.GetComponent<UIScript>().maxFuelFloat);

				// //Display Message
				DisplayMessage(" + " + fuelFloat.ToString("0") + " F");
				
			}
		}

	}

	void DisplayMessage(string msgString) {

		//Point Variables
		Vector3 ScreenPointVector = Camera.main.WorldToScreenPoint (transform.position);

		//float x = ScreenPointVector.x;  -- Delete Later 
		//float y = ScreenPointVector.y;

		//Clamp Position Values
		//x = Mathf.Clamp (x, 0.05f, 0.95f);
		//y = Mathf.Clamp (y, 0.05f, 0.9f);

		Debug.Log (ScreenPointVector.ToString ());

		//Instantiate a text object 
		GameObject txtObject = Instantiate (parentObject,  ScreenPointVector, Quaternion.identity) as GameObject;

		//Set Message 
		txtObject.GetComponentInChildren<Text> ().text = msgString;
	
		//Destroy Text Object 
		Destroy (this.gameObject);
	}
}
