using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

	public float width = 10f;
	public float height = 10f;
	public float depth = 10f;

	public float wallThickness = 1f;

	public int stories = 0;

	public GameObject frontWallLeft;
	public GameObject frontWallRight;
	public GameObject backWall;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject roof;
	public GameObject secondFloor;
	public Renderer frontWallLeftRend;
	public Renderer frontWallRightRend;
	public Renderer backWallRend;
	public Renderer leftWallRend;
	public Renderer rightWallRend;
	public Renderer secondFloorRend;
	public Renderer secondFrontWallRend;
	public Renderer secondBackWallRend;
	public Renderer secondLeftWallRend;
	public Renderer secondRightWallRend;
	public Renderer roofRend;
	public Material newMat;
	public Material secondStoryMat;

	public GameObject secondFrontWall;
	public GameObject secondBackWall;
	public GameObject secondLeftWall;
	public GameObject secondRightWall;
	public BoxCollider ourCollider;
	public Light pointLight;
	public GameObject pointLightObject;
	bool runOnce = true;
	bool lightBool = true;

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		
	}

	public void constructHouse() {
		//1. Instantiate 4 walls
		frontWallLeft = GameObject.CreatePrimitive(PrimitiveType.Cube);
		frontWallLeft.name = "FrontLeft";
		frontWallRight = GameObject.CreatePrimitive(PrimitiveType.Cube);
		frontWallRight.name = "FrontRight";
		backWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		backWall.name = "Back";
		leftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		leftWall.name = "Left";
		rightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		rightWall.name = "Right";
		roof = GameObject.CreatePrimitive(PrimitiveType.Cube);
		roof.name = "Top";
		//Make the walls a child of the house
		frontWallLeft.transform.parent = transform;
		frontWallRight.transform.parent = transform;
		backWall.transform.parent = transform;
		leftWall.transform.parent = transform;
		rightWall.transform.parent = transform;
		roof.transform.parent = transform;

		frontWallLeftRend = frontWallLeft.GetComponent<Renderer> ();

		rebuildHouse ();
	}

	public void rebuildHouse() {
		if(ourCollider==null){
		ourCollider = this.gameObject.AddComponent<BoxCollider> ();
			ourCollider.isTrigger = true;
		}

		BuildStories ();
		if(roof == null){
			//roof
			roof = this.gameObject.transform.Find("Top").gameObject;
			//entranceway
			frontWallLeft = this.gameObject.transform.Find("FrontLeft").gameObject;
			frontWallRight = this.gameObject.transform.Find("FrontRight").gameObject;
		}
		if (secondFloor == null && stories > 1){
			secondFloor = this.gameObject.transform.Find("Second Floor").gameObject;
		}

		//assign specific mat to each house
		if (newMat == null) {
			Material mat = new Material (Shader.Find ("Standard"));
			newMat = mat;
		} else {
			Material mat = new Material (Shader.Find ("Standard"));
			newMat = mat;
		}
		if(secondStoryMat == null){
			Material secondMat = new Material(Shader.Find("Standard"));
			secondStoryMat = secondMat;
		}

		//frontwallleft
		frontWallLeftRend = frontWallLeft.GetComponent<Renderer> ();
		frontWallLeftRend.material = newMat;
		//frontrightleft
		frontWallRightRend = frontWallRight.GetComponent<Renderer> ();
		frontWallRightRend.material = newMat;
		//backwall
		backWallRend = backWall.GetComponent<Renderer> ();
		backWallRend.material = newMat;
		//leftwall
		leftWallRend = leftWall.GetComponent<Renderer> ();
		leftWallRend.material = newMat;
		//rightwall
		rightWallRend = rightWall.GetComponent<Renderer> ();
		rightWallRend.material = newMat;
		//roof
		roofRend = roof.GetComponent<Renderer> ();
		roofRend.material = newMat;

		//second story materials
		if (stories > 1){
		//secondfloor
		secondFloorRend = secondFloor.GetComponent<Renderer> ();
		secondFloorRend.material = newMat;
		//frontwall
		secondFrontWallRend = secondFrontWall.GetComponent<Renderer> ();
		secondFrontWallRend.material = secondStoryMat;
		//backwall
		secondBackWallRend = secondBackWall.GetComponent<Renderer> ();
		secondBackWallRend.material = secondStoryMat;
		//leftwall
		secondLeftWallRend = secondLeftWall.GetComponent<Renderer> ();
		secondLeftWallRend.material = secondStoryMat;
		//rightwall
		secondRightWallRend = secondRightWall.GetComponent<Renderer> ();
		secondRightWallRend.material = secondStoryMat;
		//roof
		roofRend = roof.GetComponent<Renderer> ();
		roofRend.material = secondStoryMat;
		}

		newMat.color = new Color (Random.Range(0,255f)/255,Random.Range(0,255f)/255,Random.Range(0,255f)/255);
		secondStoryMat.color = new Color (Random.Range(0,255f)/255,Random.Range(0,255f)/255,Random.Range(0,255f)/255);

		if (secondFrontWall == null && stories > 1) {
			secondFrontWall = this.gameObject.transform.Find("Second Front").gameObject; 
			secondBackWall = this.gameObject.transform.Find("Second Back").gameObject; 
			secondLeftWall = this.gameObject.transform.Find("Second Left").gameObject; 
			secondRightWall = this.gameObject.transform.Find("Second Right").gameObject; 
		}
		//2. Scale them appropriately
		Vector3 frontAndBackDimension = new Vector3 (width, height, wallThickness);
		Vector3 frontEntranceDimension = new Vector3 (width/3, height, wallThickness);
		Vector3 leftAndRightDimension = new Vector3 (wallThickness, height, depth);
		Vector3 roofDimension = new Vector3 (width, wallThickness, depth + (wallThickness*2f));
		frontWallLeft.transform.localScale = frontEntranceDimension;
		frontWallRight.transform.localScale = frontEntranceDimension;
		backWall.transform.localScale = frontAndBackDimension;
		leftWall.transform.localScale = leftAndRightDimension;
		rightWall.transform.localScale = leftAndRightDimension;
		roof.transform.localScale = roofDimension;
		roof.transform.rotation = Quaternion.Euler (0, 0, 0);
		ourCollider.size = frontAndBackDimension;
		if (secondFrontWall != null && stories > 1){
			secondFloor.transform.localScale = roofDimension;
			secondFloor.transform.rotation = Quaternion.Euler (0, 0, 0);
			secondFrontWall.transform.localScale = frontAndBackDimension;
			secondBackWall.transform.localScale = frontAndBackDimension;
			secondLeftWall.transform.localScale = leftAndRightDimension;
			secondRightWall.transform.localScale = leftAndRightDimension;
		}

		//3. Position them appropriately
		frontWallLeft.transform.position = new Vector3 (transform.position.x - width/3, 
			transform.position.y + height / 2, 
			transform.position.z - depth / 2 - wallThickness/2);
		frontWallRight.transform.position = new Vector3 (transform.position.x + width/3, 
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
		ourCollider.center = new Vector3 (0, 
			transform.position.y + height / 2, 
			-depth/2);
		if (stories <= 1) {
			roof.transform.position = new Vector3 (transform.position.x, 
				transform.position.y + height - wallThickness / 2, 
				transform.position.z);
		} else {
			roof.transform.position = new Vector3 (transform.position.x, 
				transform.position.y + height * 2f - wallThickness / 2, 
				transform.position.z);
		}

		if (secondFrontWall != null && stories > 1) {
			secondFloor.transform.position = new Vector3 (transform.position.x, 
				transform.position.y + height - wallThickness / 2, 
				transform.position.z);
			secondFrontWall.transform.position = new Vector3 (transform.position.x, 
				transform.position.y + height * 1.5f, 
				transform.position.z - depth / 2 - wallThickness / 2);
			secondBackWall.transform.position = new Vector3 (transform.position.x, 
				transform.position.y + height * 1.5f, 
				transform.position.z + depth / 2 + wallThickness / 2);
			secondLeftWall.transform.position = new Vector3 (transform.position.x - width / 2 + wallThickness / 2, 
				transform.position.y + height * 1.5f, 
				transform.position.z);
			secondRightWall.transform.position = new Vector3 (transform.position.x + width / 2 - wallThickness / 2, 
				transform.position.y + height * 1.5f, 
				transform.position.z);
		}
	}

	public void BuildStories(){		
		if (runOnce) {
			stories = Random.Range (0, 3);
			runOnce = false;
			if(stories > 1){
				secondFloor = GameObject.CreatePrimitive (PrimitiveType.Cube);
				secondFloor.name = "Second Floor";
				secondFrontWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
				secondFrontWall.name = "Second Front";
				secondBackWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
				secondBackWall.name = "Second Back";
				secondLeftWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
				secondLeftWall.name = "Second Left";
				secondRightWall = GameObject.CreatePrimitive (PrimitiveType.Cube);
				secondRightWall.name = "Second Right";

				secondFloor.transform.parent = transform;
				secondFrontWall.transform.parent = transform;
				secondBackWall.transform.parent = transform;
				secondLeftWall.transform.parent = transform;
				secondRightWall.transform.parent = transform;
			}
		}
	}

	public void OnTriggerEnter(Collider other){
		if (lightBool){
			lightBool = false;
			float randomFloat = Random.Range (0,10);
			if(randomFloat>2f){
				pointLightObject = new GameObject ("The Light");
				pointLight = pointLightObject.AddComponent<Light> ();
				pointLightObject.transform.parent = transform;
				pointLightObject.transform.position = new Vector3(this.gameObject.transform.position.x, height/2, this.gameObject.transform.position.z);
				pointLight.color = new Color (Random.Range(0,255f)/255,Random.Range(0,255f)/255,Random.Range(0,255f)/255);
				pointLight.intensity = Random.Range (5, 10);
				pointLight.type = LightType.Point;
			}
		}
	}
}
