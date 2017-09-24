using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

	public float width = 10f;
	public float height = 10f;
	public float depth = 10f;

	public float wallThickness = 1f;

	public GameObject frontWall;
	public GameObject backWall;
	public GameObject leftWall;
	public GameObject rightWall;

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		
	}

	public void constructHouse() {
		//1. Instantiate 4 walls
		frontWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		frontWall.name = "Front";
		backWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		backWall.name = "Back";
		leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		leftWall.name = "Left";
		rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		rightWall.name = "Right";

		//Make the walls a child of the house
		frontWall.transform.parent = transform;
		backWall.transform.parent = transform;
		leftWall.transform.parent = transform;
		rightWall.transform.parent = transform;

		rebuildHouse ();
	}

	public void rebuildHouse() {
		//2. Scale them appropriately
		Vector3 frontAndBackDimension = new Vector3 (width, height, wallThickness);
		Vector3 leftAndRightDimension = new Vector3 (wallThickness, height, depth);
		frontWall.transform.localScale = frontAndBackDimension;
		backWall.transform.localScale = frontAndBackDimension;
		leftWall.transform.localScale = leftAndRightDimension;
		rightWall.transform.localScale = leftAndRightDimension;

		//3. Position them appropriately
		frontWall.transform.position = new Vector3 (transform.position.x, 
			transform.position.y + height / 2, 
			transform.position.z - depth / 2 - wallThickness/2);
		backWall.transform.position = new Vector3 (transform.position.x, 
			transform.position.y + height / 2, 
			transform.position.z + depth / 2 + wallThickness/2);
		leftWall.transform.position = new Vector3 (transform.position.x - width/2 + wallThickness/2, 
			transform.position.y + height / 2, 
			transform.position.z);
		rightWall.transform.position = new Vector3 (transform.position.x + width/2 - wallThickness/2, 
			transform.position.y + height / 2, 
			transform.position.z);
	}
}
