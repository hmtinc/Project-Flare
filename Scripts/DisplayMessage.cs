using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayMessage : MonoBehaviour {


	float counterFloat  = 1f;
	float scrollFloat = 15f;
	float durationFloat = 1.5f;
	Vector3 screenVector;


	// Use this for initialization
	void Start () {
	
		//Get the position of the player
		Vector3 playVector = GameObject.Find ("Cube").transform.position;

		//Get the Screen Position 
		screenVector = Camera.main.WorldToScreenPoint (playVector);

		//Modify the Y value to match the UI Canvas Anchor
		screenVector.y = (screenVector.y * -1f) + 650f;



	}
	
	// Update is called once per frame
	void Update () {

	

	
		if (counterFloat > 0) {

			//Animate Text
			transform.position = new Vector3 (screenVector.x, screenVector.y + scrollFloat * Time.deltaTime, transform.position.z);

			//Modify Counter
			counterFloat -= Time.deltaTime / durationFloat;

		} else {

			//Destroy the Text Object 
			Destroy(this.gameObject);
		}
	}
}
