using UnityEngine;
using System.Collections;

public class TestSpawn : MonoBehaviour {

	public Transform aitest;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 500; i++)
		{
			Instantiate(aitest, new Vector3(-50 + (i * 10), 1, 63), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{


	}
}
