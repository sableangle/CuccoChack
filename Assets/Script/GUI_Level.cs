using UnityEngine;
using System.Collections;

public class GUI_Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Level: " + Controller.level ;
	}
}
