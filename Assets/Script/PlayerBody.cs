using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBody : MonoBehaviour {
	public GameObject egg;
	public GameObject miniCucco;
	public GameObject _nextBody;
	public int arrowTo;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		/*if(gameObject.tag == "Chick"){
			child.renderer.material.color = new Color(1,0,0,1);
		}
		else if(gameObject.tag == "Egg"){
			child.renderer.material.color = new Color(1,1,0,1);
		}*/
	}
	
	void OnTriggerStay(Collider other) {
		if (other.tag=="Player" && Controller.stepTime<=0 && Controller.gameStart){
			
			//Controller.gameStart=false;
			GameObject.FindWithTag("Controller").GetComponent<Controller>().GameOver(0);
		}
	}


	public void Trun(int _arrow){
		if(_nextBody != null)
			_nextBody.GetComponent<PlayerBody>().Trun(arrowTo);
		arrowTo = _arrow;
		if ( arrowTo==1 ){
			transform.rotation = Quaternion.Euler(0,0,0);
		}
		if ( arrowTo==2 ){
			transform.rotation = Quaternion.Euler(0,180,0);
		}
		if ( arrowTo==3 ){
			transform.rotation = Quaternion.Euler(0,270,0);
		}
		if ( arrowTo==4 ){
			transform.rotation = Quaternion.Euler(0,90,0);
		}
	}
	public void ChangeAni(){
		miniCucco.animation.CrossFade("Walk_Fast",0.5f);
	}


	public void Change(int _type){
		if(_type == 0)
		{
			tag = "Egg";
			miniCucco.SetActive(false);
			egg.SetActive(true);
		}
		else
		{
			tag = "Chick";
			egg.SetActive(false);
			miniCucco.SetActive(true);
			if(Controller.level >= 4){
				miniCucco.animation.CrossFade("Walk_Fast",0.5f);
			}
		}
	}

}
