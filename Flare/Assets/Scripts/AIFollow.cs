
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AIFollow : MonoBehaviour {

	//Inspector Values 
	public Transform playerPosition;
	public float moveSpeedFloat = 4;
	public int maxDistanceInteger = 10;
	public int minDistanceInteger = 2;
	public AudioClip torchClip; 
	public float maxHealthFloat = 100.0f;
	public Texture2D healthBarTexture, bar2Texture, bar3Texture;
	public float lightDamageFloat = 0.1f;
	public float timeAvoidFloat = 3.0f; 

	


	//Storage variables
	float distanceFloat = 0;
	GameObject bloodObject;
	AudioSource effectPlayer;
	float healthFloat = 100.0f;
	GUIStyle healthBarStyle = new GUIStyle(); 
	bool collideMoveBool = false; 
	int directionInt;
	Vector3 cornerVector;
	float distancePosFloat;
	GameObject attackObject;
	GameObject placedBuildingObject;
	GameObject powerUpObject; 
	bool placedBuildingBool = false; 
	Color vectorColor = Color.red; 
	Transform playerTransform; 




	//Corner Calculation variables
	float objectXFloat, objectZFloat;
	Vector3 objectPosVector;

	

	
	


	// Use this for initialization
	void Start () {

		//Get the audio componet 
		effectPlayer  = GetComponent<AudioSource> ();
	
		//Get the player position 
		playerPosition = GameObject.FindWithTag ("Player").transform;
		playerTransform = GameObject.FindWithTag ("Player").transform;

		//Get bloodspawner object
		bloodObject = GameObject.Find ("bk_101");

		//Get the power up object 
		powerUpObject = GameObject.Find ("PowerUp");

		//Get attack object 
		attackObject = GameObject.Find ("aiAttack");

		//Reset collide bool value every timeavoidFloat seconds 
		InvokeRepeating("BoolResetFunction", 0, timeAvoidFloat);

		//Scan Area
		InvokeRepeating ("BuildingScan", 0, 10.0f); 

		//Generate random int
		if (Random.value < 0.5) {
			directionInt = 1;
		}
		else {
			directionInt = 2;
		}


	}
	
	// Update is called once per frame
	void Update () {
	



		//If AI has not collided with a wall 
		if (collideMoveBool == false) {

			try {
			Vector3 targetVector = playerPosition.position;

			//Modify Y Value
			targetVector.y  = 1f; 

			//Look at main character
			transform.LookAt (targetVector);
			
			//Get the distance between 2 characters
			distanceFloat = Vector3.Distance (playerPosition.position, transform.position);
			
			//Determine if AI should move
			if (distanceFloat >= maxDistanceInteger && distanceFloat > minDistanceInteger) {
				
				transform.Translate (Vector3.forward * (moveSpeedFloat) * Time.deltaTime);
				Debug.DrawLine (transform.position, playerPosition.position, vectorColor);
			}
			}

			//Catch Any exceptions
			catch {

				//Reset Player position 
				ResetTarget();
			}
			


		} else {
			
			if (directionInt == 1){
			transform.Translate(Vector3.right * moveSpeedFloat * Time.deltaTime);
				Debug.DrawLine (transform.position , transform.position + transform.right * 10 );
			}
			else {
			
			transform.Translate(Vector3.left * moveSpeedFloat * Time.deltaTime);
			}

			//transform.Translate (Vector3.forward * moveSpeedFloat * Time.deltaTime);	

		}

	

		//Attack Player 
		if (collideMoveBool == false && distanceFloat <= minDistanceInteger) {
			AttackPlayer();
		}
		
	

		//Delete object if it falls of the map
		if (transform.position.y < 0) 
		{
			Destroy(this.gameObject);
		}
	}

	//Collison actions
	void OnCollisionEnter(Collision col) 
	{

		
		//--
		GetComponent<Rigidbody>().isKinematic = true;

		if (col.gameObject.tag == "LightGun") {
 

			//Take away health from AI
			healthFloat = healthFloat - 5;
		    
		
		    //If health is 0 then kill the AI
			if (healthFloat <= 0) {

			//Get object position on screen
			Vector3 objPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

			//Play the torch sound
			effectPlayer.PlayOneShot(torchClip); 

			//Disable the meshrenderer
			GetComponent<MeshRenderer>().enabled = false;

			//Disable the object's collider
			//GetComponent<CapsuleCollider>().enabled = false;

			//Make object kinematic
			//GetComponent<Rigidbody>().isKinematic = true;

		    GetComponent<CapsuleCollider>().enabled = false;

			//Instantiate a PowerUp Object 
		    Quaternion powerUpRotation = Quaternion.Euler (90,0,0);
		    Instantiate(powerUpObject, transform.position, powerUpRotation);

			//Destroy the AI gameobject 
			Destroy(this.gameObject, torchClip.length);
			
			Debug.Log("AI Hit and Destroyed");

			//Create a ray based on screen point
			Ray collideRay = Camera.main.ScreenPointToRay(objPosOnScreen);
			RaycastHit collideRayCastHit;

			//Check for all colliders in range of teh ray
			if(Physics.Raycast(collideRay, out collideRayCastHit, Mathf.Infinity))
			{
				//Create a bloodsplatter object
				GameObject bloodSplatterObject = Instantiate(bloodObject, collideRayCastHit.point + (collideRayCastHit.normal * 2.5f), Quaternion.identity) as GameObject;

				//Destroy blood splater after 2 seconds
				Destroy(bloodSplatterObject,2);

			}

		

			    }
		}

		//Detect if AI has collided with a wall
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Mountain") {


            
			
			collideMoveBool = true;
			
			
			


		
		}

		//--
		GetComponent<Rigidbody>().isKinematic = false; 


		
	}

	//GUI Elements Go Here 
	void OnGUI() 
	{
		//Get the position of the enemy on the screen
		Vector2 AIScreenVector = Camera.main.WorldToScreenPoint(transform.position);

		//Draw Health bar only if renderer is active
		if (GetComponent<MeshRenderer> ().enabled == true) 
		{
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
			healthBarStyle.normal.textColor = Color.red;

			//Draw only if ai is 10 units away 
			if (distanceFloat <= 5.0f) 
			{
			GUI.Box (new Rect (AIScreenVector.x - 35, (Screen.height - AIScreenVector.y) - 50, 60, 20), "  "
			+ healthFloat.ToString ("0") + "/" + maxHealthFloat.ToString (),healthBarStyle);
			}
		}


		
	}

	//Trigger Enter/stay
	void OnTriggerStay(Collider col2) {

		if (col2.gameObject.tag == "Light") {
			
			//Take away health from AI
			healthFloat = healthFloat - lightDamageFloat;
			
			//If health is 0 then kill the AI
			if (healthFloat <= 0) {
				
				//Get object position on screen
				Vector3 objPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);
				
				//Play the torch sound
				effectPlayer.PlayOneShot(torchClip); 

				GetComponent<CapsuleCollider>().enabled = false;
				//Disable the meshrenderer
				GetComponent<MeshRenderer>().enabled = false;
				
				//Disable the object's collider
				//GetComponent<CapsuleCollider>().enabled = false;
				
				//Make object kinematic
				//GetComponent<Rigidbody>().isKinematic = true;

			    //Instantiate a power up object 
			    Instantiate(powerUpObject, transform.position, Quaternion.identity);


				//Increment Counter
				GameObject.Find("Cube").GetComponent<CharacterMove>().killedAIInteger += 1;

				
				//Destroy the AI gameobject 
				Destroy(this.gameObject, torchClip.length);
				
				Debug.Log("AI Hit and Destroyed");
				
				//Create a ray based on screen point
				Ray collideRay = Camera.main.ScreenPointToRay(objPosOnScreen);
				RaycastHit collideRayCastHit;
				
				//Check for all colliders in range of teh ray
				if(Physics.Raycast(collideRay, out collideRayCastHit, Mathf.Infinity))
				{
					//Create a bloodsplatter object
					GameObject bloodSplatterObject = Instantiate(bloodObject, collideRayCastHit.point + (collideRayCastHit.normal * 2.5f), Quaternion.identity) as GameObject;
					
					//Destroy blood splater after 2 seconds
					Destroy(bloodSplatterObject,2);
					
				}
				
				
				
			}
		}



	}

	void BoolResetFunction () {
		collideMoveBool = false;
		//Debug.Log ("bool reset");
	}




	//Attack Player 
	void AttackPlayer() {

		//Attack variables
		Vector3 aiPosVector = transform.position;
		Vector3 aiForwardVector = transform.forward;
		Vector3 attackVector = aiPosVector + aiForwardVector * 1;

		//Spawn Attack Object
		GameObject AAObject = Instantiate(attackObject, attackVector, Quaternion.identity) as GameObject;

		//Set damage value 
		AAObject.GetComponent<AIAttack>().damageValue = Random.value; 

		//Destroy attack after 2 seconds
		Destroy(AAObject,0.5f);


	}



	//Get Magnitude of Given Vector 
	IEnumerator GetMagnitude(Vector3 GivenVector) {

		//Result Variable 
		float resultFloat; 

		//Get the magnitude of the given vector |v| = (x^2 + y^2 + z^2) 
		resultFloat = Mathf.Pow (GivenVector.x, 2.0f) + Mathf.Pow (GivenVector.y, 2.0f) + Mathf.Pow (GivenVector.z, 2.0f);

		//Return Magnitude 
		yield return resultFloat; 

	}


	//Scan for Placed Buildings
	void BuildingScan() {

	

			//Detection Sphere
			Collider[] DetectionSphere = Physics.OverlapSphere (transform.position, 10);


			//Check Collider Tags
			foreach (Collider collide in DetectionSphere) {

				if (collide.gameObject.tag == "PBuilding") {

					//set first detected building as the target 
					playerPosition = collide.gameObject.transform;
					placedBuildingObject = collide.gameObject;
					placedBuildingBool = true;

					//Set Vector Color
					vectorColor = Color.green;

			

				   //Change min distance 
				   minDistanceInteger = 2; 

					//Break Loop 
					break;
				}

			}


	}

	//Reset AI Target to player
	void ResetTarget() {

		//Set Player position 
		playerPosition = playerTransform;

		//Set vector color
		vectorColor = Color.red;

		//Set Bool value
		placedBuildingBool = false; 

		
		//Change min distance 
		minDistanceInteger = 2; 
	}













}