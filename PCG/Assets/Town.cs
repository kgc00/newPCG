using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour {

	public GameObject housePrefab;

	// Use this for initialization
	void Start () {
		//Create 30 houses, and 
		for (int i = 0; i < 30; i++) {
			//Get x,y,z positions that are "on" the town (the ground)
			float xPos = transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2);
			float yPos = transform.position.y;
			float zPos = transform.position.z + Random.Range(-transform.localScale.z/2, transform.localScale.z/2);
			Vector3 pos = new Vector3 (xPos, yPos, zPos);
			//Create the house at that position
			GameObject houseObj = (GameObject)Instantiate (housePrefab, pos, Quaternion.identity);
			House houseScript = houseObj.GetComponent<House> ();
			//Assign random width, height and depth values
			houseScript.width = Random.Range (5, 40);
			houseScript.height = 10;
			houseScript.depth = Random.Range (5, 40);
			//Calling this function scales and positions all of the walls of the house
			houseScript.rebuildHouse ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
