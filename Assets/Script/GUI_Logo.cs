using UnityEngine;
using System.Collections;

public class GUI_Logo : MonoBehaviour {
	//for SettingLable
	private GameObject ControllerObj;
	private Vector3 SettingMousePosition = Vector3.zero;
	private Vector3 SettingMouseDelta = Vector3.zero;
	private Vector3 NowLablePosition = Vector3.zero;
	private Vector3 NowLablePosition2 = Vector3.zero;
	private Vector3 delta = Vector3.zero;
	private Vector3 lastPos = Vector3.zero;
	public GameObject GUIcamera;
	private float rayDistance = 100.0f;
	private Ray ray;
	private string x;
	public GameObject settingLable;
	public GameObject settingLable2;
	// Use this for initialization
	void Start () {
		//InvokeRepeating("SelfJump",5f,2f);
		ControllerObj = GameObject.Find("Controller");
	}

	void SelfJump(){
		iTween.MoveFrom(settingLable,iTween.Hash("y",0.5f,"time",0.5f,"easetype",iTween.EaseType.easeOutBounce,"islocal",true));
	}
	// Update is called once per frame
	void Update () {
		mouseRay();
		touchRay();
		if(x == "Logo" && delta.x < 0.05f && delta.y < 0.05f){
			if(Input.GetMouseButtonDown(0)){
				SettingMousePosition = GUIcamera.camera.ScreenToWorldPoint(Input.mousePosition);
				SettingMouseDelta = Vector3.zero;
				NowLablePosition = settingLable.transform.position;
			}
			else if(Input.GetMouseButton(0)){
				SettingMouseDelta = GUIcamera.camera.ScreenToWorldPoint(Input.mousePosition) - SettingMousePosition;
				if(NowLablePosition.y+SettingMouseDelta.y > 100f)
					settingLable.transform.position = new Vector3(NowLablePosition.x,NowLablePosition.y+SettingMouseDelta.y,-2);
			}

			else if(Input.GetMouseButtonUp(0) && settingLable.transform.position.y > 101f){
				iTween.MoveTo(settingLable,iTween.Hash("y",5f,"time",0.5f,"easetype",iTween.EaseType.easeOutCubic,"islocal",true));
				Controller.gameStart = true;
				ControllerObj.GetComponent<Controller>().Pause();
				x = "";	
			}	
			else if(Input.GetMouseButtonUp(0) && settingLable.transform.position.y < 101f){
				iTween.MoveTo(settingLable,iTween.Hash("y",0f,"time",0.5f,"easetype",iTween.EaseType.easeOutBounce,"islocal",true));

			}

		//	else if(Input.GetMouseButtonUp(0)){
			//	iTween.MoveTo(settingLable,iTween.Hash("x",-4.5f,"time",0.5f,"easetype",iTween.EaseType.easeOutBack,"islocal",true));
			//}
		}
		if(x == "Pause" && delta.x < 0.05f && delta.y < 0.05f){
			if(Input.GetMouseButtonDown(0)){
				SettingMousePosition = GUIcamera.camera.ScreenToWorldPoint(Input.mousePosition);
				SettingMouseDelta = Vector3.zero;
				NowLablePosition2 = settingLable2.transform.position;
			}
			else if(Input.GetMouseButton(0)){
				SettingMouseDelta = GUIcamera.camera.ScreenToWorldPoint(Input.mousePosition) - SettingMousePosition;
				if(NowLablePosition2.y+SettingMouseDelta.y > 100f)
					settingLable2.transform.position = new Vector3(NowLablePosition2.x,NowLablePosition2.y+SettingMouseDelta.y,-1);
			}
			
			else if(Input.GetMouseButtonUp(0) && settingLable2.transform.position.y > 101f){
				iTween.MoveTo(settingLable2,iTween.Hash("y",5f,"time",0.5f,"easetype",iTween.EaseType.easeOutCubic,"islocal",true));
				Controller.gameStart = true;
				ControllerObj.GetComponent<Controller>().Pause();
				x = "";
			}	
			else if(Input.GetMouseButtonUp(0) && settingLable2.transform.position.y < 101f){
				iTween.MoveTo(settingLable2,iTween.Hash("y",0f,"time",0.5f,"easetype",iTween.EaseType.easeOutBounce,"islocal",true));
				
			}

			//	else if(Input.GetMouseButtonUp(0)){
			//	iTween.MoveTo(settingLable,iTween.Hash("x",-4.5f,"time",0.5f,"easetype",iTween.EaseType.easeOutBack,"islocal",true));
			//}
		}
	}
	void touchRay(){
		if(Input.touchCount > 0){
			for(int i=0;i<Input.touchCount;i++){
				if(Input.GetTouch(i).phase == TouchPhase.Began){
					ray = GUIcamera.camera.ScreenPointToRay (Input.GetTouch(i).position);
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
			ray = GUIcamera.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;	
			if(Physics.Raycast (ray ,out hit , rayDistance)){
				x = hit.collider.name;
			}
		}
	}
}
