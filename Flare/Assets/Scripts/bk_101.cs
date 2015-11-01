using UnityEngine;
using System.Collections;

public class bk_101 : MonoBehaviour {

	//Values for inspector
	public GameObject bloodIObject;

	//Values for intenral use
	RaycastHit bloodHitCast;

	void Awake() 
	{
		//Local variables
		int counterInteger = 1;
		int dropCountInteger = Random.Range (40, 80);

		//Spawn blood object
		while (counterInteger <= dropCountInteger) 
		{
			//increment counter 
			counterInteger = counterInteger + 1;
			
			Vector3 direction = transform.TransformDirection (Random.onUnitSphere * 5);

			if (Physics.Raycast(transform.position, direction, out bloodHitCast, 10)) {

				//Spawn bloodIobject 
				GameObject bloodIIObject = Instantiate(bloodIObject, bloodHitCast.point + (bloodHitCast.normal * 0.1f), Quaternion.FromToRotation(Vector3.up, bloodHitCast.normal) ) as GameObject ;
				
		       
				//Translate the object on the xz axis based on a random value
				float rFloat = Random.value;

				//Get the position of the blood object
				Vector3 objectVector = bloodIIObject.transform.localScale;

				//Modify and apply the position
				objectVector.x *= rFloat;
				objectVector.z *= rFloat;
				objectVector.y = 0;
				bloodIIObject.transform.localScale = objectVector;




				//Rotate object based on random value 
				float qFloat = Random.value;
				bloodIIObject.transform.RotateAround(bloodHitCast.point, bloodHitCast.normal, qFloat);

				//Destroy the bloodII object
				Destroy(bloodIIObject, 5);




			}

		}
	}
}
