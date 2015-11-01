using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	//Inspector Values
	public Transform aiTest;

	//Local Variables
	int randomInt; 
	float randomXFloat, randomYFloat, randomZFloat; 
	Vector3 random1Vector , random2vector, random3vector; 


	Vector3 spawnVector;


	// Use this for initialization
	void Start () {
	
		InvokeRepeating("SpawnNow", 0, 5.0f);

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	//Spawn AI at a random location 
	void SpawnNow() {

		//Determine how many ai elements to spawn
		randomInt = Random.Range (1, 10);

		//Generate random X value
		randomXFloat = Random.Range (GameObject.Find ("Cube").GetComponent<CharacterMove> ().maxX, GameObject.Find ("Cube").GetComponent<CharacterMove> ().minX);

		//Generate Random Z value 
		randomZFloat = Random.Range (GameObject.Find ("Cube").GetComponent<CharacterMove> ().minZ, GameObject.Find ("Cube").GetComponent<CharacterMove> ().maxZ);

		//Y Value
		randomYFloat = GameObject.Find ("Cube").GetComponent<CharacterMove> ().maxMinY;


		//Spawn vector 
		spawnVector = new Vector3(randomXFloat, randomYFloat, randomZFloat);

		//Debug
		Debug.Log ("Spawn @ " + spawnVector.ToString ());


		//Spawn the AI
		for (int i = 0; i <= randomInt; i++) {
		Instantiate(aiTest, new Vector3(spawnVector.x + 10 , spawnVector.y, spawnVector.z), Quaternion.identity);


		}

	}
}
