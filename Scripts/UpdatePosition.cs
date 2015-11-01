using UnityEngine;
using System.Collections;

public class UpdatePosition : MonoBehaviour {

	//Camera and Caracter Variables
	public Vector3 currentPosition;
	public Transform playerPosition;


	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//Update Position of object to match player's position
		transform.position = playerPosition.position + currentPosition;
	}
}
