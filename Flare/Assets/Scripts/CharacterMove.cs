using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	//Values for the inspector 
	public float moveSpeedFloat = 2.0f;
	public float rotateSpeedFloat = 2.0f;
	public float yAxisRotateFloat = 1.0f;
	public GameObject messageObject; 
	public float healthRegenTimeFloat = 15.0f;
	public int killedAIInteger = 0;
	public int placedBuildingInt = 0; 



	//Values for Internal Use 
	Rigidbody charRigidBody;
	bool hitDetected = false;
	Vector3 mouseInputPosition;
	Vector3 mouseToWorldPosition;
	public bool lanternBoolean = false;
	GameObject lanternObject;
	float healthReGenFloat = 5f;



	//Fov Variables
	float minFOVFloat = 60f;
	float maxFOVFloat = 150f;
	float sensitivityFloat = 10f;
	public float FOVFloat;


	//Max and Min values 
	public float minZ = 0.0f;
	public float maxZ = 240.0f;
	public float maxMinY = 1.0f;
	public float minX = 0;
    public float maxX = 280.0f;


	// Use this for initialization
	void Start () {
	
	
		//get the rigidbody componet 
		charRigidBody = GetComponent<Rigidbody>();


		//Get the pointlight object
		lanternObject = GameObject.Find ("Lantern");

		//Health Regen 
		InvokeRepeating ("HealthRegen", 0, healthRegenTimeFloat); 

		//Hide Error Message Image
		InvokeRepeating("MHide", 0, 5.0f);


	}
	
	// Update is called once per frame
	void Update () 
	{

		

		//Get Current Field of View 
		FOVFloat = Camera.main.fieldOfView;

		//Modify field of view based on scrollwheel input
		FOVFloat += Input.GetAxis ("Mouse ScrollWheel") * sensitivityFloat * -1.0f;

		//Clamp the FOV value
		FOVFloat = Mathf.Clamp (FOVFloat, minFOVFloat, maxFOVFloat);

		//Apply new field of view 
		Camera.main.fieldOfView = FOVFloat;

		//Get the poistion of the mouse
		mouseInputPosition = Input.mousePosition;

		//Get the poistion of the mouse in relation to the 3d world 
		mouseToWorldPosition = Camera.main.ScreenToWorldPoint(
		//Subtract mouse position from screen width and height to fix inverted x and y values between world and mouse poistion
	    new Vector3(Screen.width - mouseInputPosition.x, Screen.height - mouseInputPosition.y, Camera.main.transform.position.z - 2f));

		//Ignore the y axis
		mouseToWorldPosition = new Vector3(mouseToWorldPosition.x , yAxisRotateFloat, mouseToWorldPosition.z);
		
		//Look at mouse point
		transform.LookAt (mouseToWorldPosition); 


		//Move character forward
		if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{

			transform.Translate(Vector3.forward * (moveSpeedFloat) * Time.deltaTime);
		


		}
		 
		//Move character backwards
		if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{
			transform.Translate(Vector3.back * (moveSpeedFloat) * Time.deltaTime);
		}

		//Move character left
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
		{
			transform.Translate(Vector3.left * (moveSpeedFloat) * Time.deltaTime);
		}

		//Move character right
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
		{
			transform.Translate(Vector3.right * (moveSpeedFloat) * Time.deltaTime);
		}

		//Lantern On/Off
		if (Input.GetKey (KeyCode.F)) 
		{
			if (lanternBoolean == false) {

				lanternBoolean = true;
				lanternObject.GetComponent<Light>().enabled = false;
				lanternObject.GetComponent<SphereCollider>().enabled = false;

			}
			else {

				lanternBoolean = false;
				lanternObject.GetComponent<Light>().enabled = true;
				lanternObject.GetComponent<SphereCollider>().enabled = true;;

			}

		}

		//Subtract fuel when lantern is on
		if (lanternBoolean == false) {

			GetComponent<UIScript>().fuelFloat -= 0.001f;
		}

		//Disable lantern if player has no fuel
		if (GetComponent<UIScript> ().fuelFloat <= 0) {
			lanternObject.GetComponent<Light>().enabled  = false;
		}


		//Clamp player poistion to min and max values 
		ClampPosition (); 

	}


	//Stop Player from bouncing back if character model hits a wall 
	void OnCollisionEnter(Collision col) 
	{
		//set kinematic to true 
		charRigidBody.isKinematic = true;

		//reassign hit detected value
		hitDetected = true;



	}


	//Update after frame has been drawn 
	void LateUpdate() 
	{
		if (hitDetected == true) {

			//reassign hitdetected value
			hitDetected = false;

			//Set kinematic value back to false
			charRigidBody.isKinematic = false;
		} 

	
	}

	//Hide Message
	void MHide() {
		messageObject.SetActive (false); 

	}

	void ClampPosition() {



		//Get the position vector 
		Vector3 playerPositionVector = transform.position;

		//Clamp Z component
		playerPositionVector.z =  Mathf.Clamp (playerPositionVector.z, minZ, maxZ);

		//Clamp X componet 
		playerPositionVector.x = Mathf.Clamp (playerPositionVector.x, maxX, minX);


		//Modify Y value 
		playerPositionVector.y = maxMinY;

		//Assign clamped vector to player object
		transform.position = playerPositionVector;

		//Write to console for debug purposes
		//Debug.Log ("Clamped Position : " + playerPositionVector.ToString ());


	}

	//Trigger Stay/Enter
	void OnTriggerStay(Collider trigCollide){

		//If Trigger Tag is attack then invoke damage 
		if (trigCollide.tag == "EAttack") {

			//Subtract health from player (Get and Modify Health value from UIScript.cs) 
			GetComponent<UIScript>().healthFloat -= 0.01f;

		}

	}

	//Health Regen 
	void HealthRegen() {

		//Get current and max Health Value
		float currHealthFloat  = GetComponent<UIScript> ().healthFloat; 
		float currMaxHealthFloat = GetComponent<UIScript> ().maxHealthFloat;

		//Check if health is less than max 
		if (currHealthFloat < currMaxHealthFloat) {

			//Add Health 
			GetComponent<UIScript>().healthFloat += healthReGenFloat; 
		}
	}




}
