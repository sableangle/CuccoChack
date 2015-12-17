using UnityEngine;
using System.Collections;

public class Partical : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject,0.5f);
	}
}
