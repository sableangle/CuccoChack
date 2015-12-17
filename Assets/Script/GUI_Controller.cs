using UnityEngine;
using System.Collections;

public class GUI_Controller : MonoBehaviour {
	public GameObject ControllerObj;
	public GUIStyle textStyle;
	private float rayDistance = 100.0f;
	private Ray ray;
	static public string x;
	static public string y;	
	public GameObject pause;
	public GameObject Xobj;
	public GameObject XLobj;
	public GameObject pauseBtn;
	public GameObject rankBtn;
	// Use this for initialization
	void Start () {
		camera.aspect = 1.333f;
	}
	
	// Update is called once per frame
	void Update () {
		touchRay();
		mouseRay();
		switch(x){
		case "Logo":
			resetX();
		break;
		case "HelpButton":	
			ControllerObj.GetComponent<Controller>().HelpItem();
			iTween.MoveTo(Xobj,iTween.Hash("x",1.15f,"y",-0.82f,"islocal",true,"time",1f));
			iTween.MoveTo(rankBtn,iTween.Hash("x",-1.57f,"y",-1.14f,"islocal",true,"time",1f));
			iTween.MoveTo(pauseBtn,iTween.Hash("x",1.5f,"y",-1.17f,"islocal",true,"time",1f));
			resetX();
		break;
		case "X":	
			ControllerObj.GetComponent<Controller>().HelpItemBack();
			iTween.MoveTo(Xobj,iTween.Hash("x",1.5f,"y",-1.2f,"islocal",true,"time",1f));
			iTween.MoveTo(rankBtn,iTween.Hash("x",-1.22f,"y",-0.86f,"islocal",true,"time",1f));
			iTween.MoveTo(pauseBtn,iTween.Hash("x",1.15f,"y",-0.85f,"islocal",true,"time",1f));
			resetX();
		break;
		case "XL":	
			ControllerObj.GetComponent<Controller>().RankBarBack();
			iTween.MoveTo(XLobj,iTween.Hash("x",-1.58f,"y",-1.58f,"islocal",true,"time",1f));
			iTween.MoveTo(pauseBtn,iTween.Hash("x",1.15f,"y",-0.85f,"islocal",true,"time",1f));
			iTween.MoveTo(rankBtn,iTween.Hash("x",-1.22f,"y",-0.86f,"islocal",true,"time",1f));
			resetX();
		break;
		case "PauseButton":	
			Debug.Log("PauseButton");
			iTween.MoveTo(pause,iTween.Hash("y",0f,"time",2f,"easetype",iTween.EaseType.easeOutBounce,"islocal",true));
			Controller.gameStart = false;
			ControllerObj.GetComponent<Controller>().Pause();
			resetX();
		break;
		case "RankButton":	
			ControllerObj.GetComponent<Controller>().RankBar();
			iTween.MoveTo(XLobj,iTween.Hash("x",-1.13f,"y",-0.88f,"islocal",true,"time",1f));
			iTween.MoveTo(pauseBtn,iTween.Hash("x",1.5f,"y",-1.17f,"islocal",true,"time",1f));
			iTween.MoveTo(rankBtn,iTween.Hash("x",-1.57f,"y",-1.14f,"islocal",true,"time",1f));
			resetX();
		break;
		case "Pause":	
			Debug.Log("Pause");

			resetX();
		break;
		case "GameOver":
		case "GameOverA":
			Application.LoadLevel("Play");
			resetX();
			break;
		}
	}
	void resetX(){
		x = "";	
	}
	
	void touchRay(){
		if(Input.touchCount > 0){
			for(int i=0;i<Input.touchCount;i++){
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					ray = this.camera.ScreenPointToRay (Input.GetTouch(i).position);
					RaycastHit hit;	
					if(Physics.Raycast(ray,out hit,rayDistance)){
						y = hit.collider.name;
					}
				}
				if(Input.GetTouch(i).phase == TouchPhase.Ended){
					ray = this.camera.ScreenPointToRay (Input.GetTouch(i).position);
					RaycastHit hit;	
					if(Physics.Raycast(ray,out hit,rayDistance)){
						x = hit.collider.name;
					}
				}
			}
		}
	}
	
	void mouseRay(){
		if(Input.GetMouseButtonDown(0)){
			ray = this.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;	
			if(Physics.Raycast (ray ,out hit , rayDistance)){
				y = hit.collider.name;
			}
		}
		if(Input.GetMouseButtonUp(0)){
			ray = this.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;	
			if(Physics.Raycast (ray ,out hit , rayDistance)){
				x = hit.collider.name;
			}
		}
	}

}
