﻿using UnityEngine;
using System.Collections;

public class GUI_Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Score: " + ScoreManager.score ;
	}
}
