﻿using UnityEngine;
using System.Collections;

public class DeleteMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Destroy Object in 3 seconds
		Destroy (this.gameObject, 3.0f);
	}
}
