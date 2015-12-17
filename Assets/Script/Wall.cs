using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public GameObject tree;
	void Update()
	{
		if(tree != null)
		{
			tree.transform.LookAt(Camera.main.transform.position);
			tree.transform.rotation = Quaternion.Euler(tree.transform.eulerAngles.x,180,tree.transform.eulerAngles.z);
		}
	}
	void OnTriggerStay(Collider other) {
		if (other.tag=="Player" && Controller.stepTime<=0 && Controller.gameStart){
			//Controller.gameStart=false;
			Debug.Log("OK");
			GameObject.FindWithTag("Controller").GetComponent<Controller>().GameOver(1);
		}
	}
}
