using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dot : MonoBehaviour {
	public GameObject Partical ;
	public GameObject food;
	public AudioClip eatSE;

	// Use this for initialization
	void Start () {
		Instantiate( Partical,new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z),Quaternion.Euler(0,0,0));
	
	}
	
	// Update is called once per frame
	void Update () {
		food.transform.LookAt(Camera.main.transform.position);
		food.transform.rotation = Quaternion.Euler(food.transform.eulerAngles.x,180,food.transform.eulerAngles.z);
	}
	
	void OnTriggerStay(Collider other) {
		if (other.tag=="Player"){
			AudioSource.PlayClipAtPoint(eatSE,Vector3.zero);
			Controller.triEatFood = true ;
	        Destroy(gameObject);
		}
    }
}
