using UnityEngine;
using System.Collections;

public class AISpawn : MonoBehaviour {
	
	//Inspector variables
	public Transform[] spawnPoints = new Transform[25];


	//Variables for internal use 
	int randomInteger = 0; 

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Generate random integer 

		Debug.Log (randomInteger.ToString ());
	}
}
