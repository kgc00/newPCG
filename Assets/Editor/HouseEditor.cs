using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(House))]
public class HouseEditor : Editor {

	//This allows us to create a house just as we would create cube or whatever
	[MenuItem("GameObject/Create Other/House")]
	static void Create() {
		GameObject houseObj = new GameObject ("House");
		House houseScript = houseObj.AddComponent<House> ();
		houseScript.constructHouse ();
	}
		
	public void OnSceneGUI() {
		//Get a reference to the particular house that this script is changing
		House myTarget = target as House;

		//There are a ton of other interesting handle tools. Check the documentation
		Handles.Label (myTarget.transform.position, myTarget.gameObject.name);
	}

	public override void OnInspectorGUI() {
		//This function will draw the regular/not custom inspector
		//DrawDefaultInspector ();

		//Get a reference to the particular house that this script is changing
		House myTarget = target as House;

		//This function starts listening to see if the inspector has changed anything
		//	This works in conjuntion with the EditorGUI.EndChangeCheck function which
		//	returns true or false based on whether something has changed in the inspector.
		EditorGUI.BeginChangeCheck ();

		myTarget.width = EditorGUILayout.Slider(myTarget.width, 1, 100);
		myTarget.height = EditorGUILayout.Slider(myTarget.height, 1, 100);
		myTarget.depth = EditorGUILayout.Slider(myTarget.depth, 1, 100);

		myTarget.wallThickness = EditorGUILayout.Slider(myTarget.wallThickness, 1, 3);

		if (EditorGUI.EndChangeCheck ()) {
			//For efficiency reasons, only rebuild the house if something has changed
			myTarget.rebuildHouse ();
		}
	}
}
