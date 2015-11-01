//Following Script Animates the Attack Object and Aids the AIFollow Script. 
//Player Damage is handled by CharacterMove.cs

using UnityEngine;
using System.Collections;

public class AIAttack : MonoBehaviour {

	//Inspector Values
	public Transform PlayerTransform; 
	public float attackSpeedFloat;


	//Damage Value (Randomized upon spawn)
	public float damageValue;

    //Storage Variables 
	float distFloat; 


	// Use this for initialization
	void Start () {
	
		//Look at Player 
		transform.LookAt (PlayerTransform); 

		//Get Distance between player and attack object 
		distFloat = Vector3.Distance (PlayerTransform.transform.position, transform.position);

	}
	
	// Update is called once per frame
	void Update () {


		//Translate Attack object forward
		if (distFloat >= 0) {
			transform.Translate (Vector3.forward * (attackSpeedFloat) * Time.deltaTime);
		}
		
	
	}
}
