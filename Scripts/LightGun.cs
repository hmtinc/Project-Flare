using UnityEngine;
using System.Collections;


public class LightGun : MonoBehaviour {

	public ParticleSystem lightParticle;
	public ParticleSystem glowParticle;
	public AudioClip lightSound;
	AudioSource audioPlayer; 
	public bool firebool = false;

	Collider  colliderObject;
	// Use this for initialization
	void Start () {
	   
		//Get the audio componet 
		audioPlayer = GetComponent<AudioSource> ();

		//Set light particle system to stop and turn of the collider
		lightParticle.emissionRate = 0.0f;
		glowParticle.emissionRate = 0.0f;
		colliderObject = GameObject.Find ("lightCollide").GetComponent<Collider>();
		colliderObject.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	  

		//Display light gun effect and play light gun sound 
		if (Input.GetMouseButton (1) && GetComponent<UIScript> ().fuelFloat > 0) {


			audioPlayer.PlayOneShot (lightSound);
			lightParticle.emissionRate = 150.0f;
			glowParticle.emissionRate = 50.0f;
			colliderObject.enabled = true;

			//Subtract fuel when light gun is in use
			GetComponent<UIScript> ().fuelFloat -= 0.1f;
			



		} else if (firebool == true) {
			
			audioPlayer.PlayOneShot (lightSound);
			lightParticle.emissionRate = 150.0f;
			glowParticle.emissionRate = 50.0f;
			colliderObject.enabled = true;
			
			//Subtract fuel when light gun is in use
			GetComponent<UIScript> ().fuelFloat -= 0.1f;
		}
		else 
		{
			audioPlayer.Stop(); 
			lightParticle.emissionRate = 0.0f;
			glowParticle.emissionRate = 0.0f;
			colliderObject.enabled = false;
		}
	}
}
