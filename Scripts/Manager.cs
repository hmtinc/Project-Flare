using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {

	//Build Bar Variables 
	public Transform[] iobjects = new Transform[3] ;
	public float[] iValues = new float[3];
	bool BBar = false; 
	public GameObject BBBar;
	public GameObject GBar;
	public GameObject Settings; 
	public GameObject LMap;
	public GameObject MainBar;
	bool GGBar = false; 
	bool settingsbool = false;
	bool largeBool = false;




	void Start () {

	}

	// Update is called once per frame
	public void BuildBar () {
	
	
		if (BBar == false) {
			BBar = true;
			BBBar.SetActive (true); 

		} else {
			BBar = false;
			BBBar.SetActive(false);
		}

	}

	public void LargeMap() {

		if (largeBool == false) {
			largeBool = true;
			LMap.SetActive (true);
		} else {
			largeBool = false;
			LMap.SetActive(false);
		}
	}

	public void ExitLargeMap () {
		largeBool = false;
		LMap.SetActive(false);
	}

	public void SetBar () {
		
		
		if (settingsbool == false) {
			settingsbool = true;
			Settings.SetActive (true); 
			Time.timeScale = 0.0f;
			largeBool = false;
			LMap.SetActive(false);
			MainBar.SetActive(false);
			
		} else {
			settingsbool = false;
			Settings.SetActive(false);
			GGBar = false;
			GBar.SetActive(false);
			Time.timeScale = 1.0f;
			MainBar.SetActive(true);
		}
		
	}

	public void graphicsb () {
		if (GGBar == false) {
			GGBar = true;
			GBar.SetActive (true); 
			Settings.SetActive(false);
			GameObject.Find("ButtonManager").GetComponent<GraphicsS>().SetSet();
			
		

			
		} else {
			GGBar = false;
			GBar.SetActive(false);
			Settings.SetActive (true); 
		}

	}

	public void Spawn(int i) {

		// Spawn Variables
		GameObject playerObject;
		Vector3 playerVector;
		playerObject = GameObject.Find ("Cube");

		//Set placed bool to true
		playerObject.GetComponent<UIScript> ().placedBool = true;

		//Determine if player has resources to build object
		if (playerObject.GetComponent<UIScript> ().resourcesFloat >= iValues[i] ) {

			//Determine where to spawn object
			playerVector = playerObject.transform.position;
			playerVector.y -= 1;
			playerVector.z += 2.5f; 

			//Spawn Object 
			Instantiate (iobjects[i], playerVector, Quaternion.identity);

			//Subtract resource value
			playerObject.GetComponent<UIScript> ().resourcesFloat -= iValues[i];
		}
	}

	public void FMove (bool val) {

		if (val == true) {

			GameObject.Find ("Cube").transform.Translate (Vector3.forward * 
				GameObject.Find ("Cube").GetComponent<CharacterMove> ().moveSpeedFloat 
				* Time.deltaTime);
		}

	}

	public void LMove () {
		GameObject.Find("Cube").transform.Translate (Vector3.left * 
		                                             GameObject.Find("Cube").GetComponent<CharacterMove>().moveSpeedFloat 
		                                             * Time.deltaTime);
	}

	public void RMove () {
		GameObject.Find("Cube").transform.Translate (Vector3.right * 
		                                             GameObject.Find("Cube").GetComponent<CharacterMove>().moveSpeedFloat 
		                                             * Time.deltaTime);
	}

    public void DMove () {
		GameObject.Find("Cube").transform.Translate (Vector3.back * 
		                                             GameObject.Find("Cube").GetComponent<CharacterMove>().moveSpeedFloat 
		                                             * Time.deltaTime);
	}

	public void FlashOn () {
		bool lanternBoolean = GameObject.Find ("Cube").GetComponent<CharacterMove> ().lanternBoolean;
		GameObject lanternObject = GameObject.Find ("Lantern");

		if (lanternBoolean == false) {
			
			GameObject.Find ("Cube").GetComponent<CharacterMove> ().lanternBoolean = true;
			lanternObject.GetComponent<Light>().enabled = false;
			lanternObject.GetComponent<SphereCollider>().enabled = false;

			
		}
		else {
			
			GameObject.Find ("Cube").GetComponent<CharacterMove> ().lanternBoolean = false;
			lanternObject.GetComponent<Light>().enabled = true;
			lanternObject.GetComponent<SphereCollider>().enabled = true;;
			
		}
	}

	public void FireButton() {

		GameObject.Find ("Cube").GetComponent<LightGun> ().firebool = true;

	}

	public void FireStop() {

		GameObject.Find ("Cube").GetComponent<LightGun> ().firebool = false;
	}

}
