using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	
	//"sbody" Class
	public class sbody{
		public int posX ;
		public int posZ ;
		public GameObject body ;
		public sbody(int x, int z, GameObject b){
			posX = x ;
			posZ = z ;
			body = b ;
		}
	}
	//for floor creater
	public int maxTreeNum = 10;
	private List<int> treeX = new List<int>();
	private List<int> treeZ = new List<int>();
	public GameObject[] prefabFloor;
	//for Touch
	private float rayDistance = 100.0f;
	private Ray ray;
	public GameObject Test;
	private Vector3 touchPoistion;
	//
	public GameObject prefabDot ;
	public GameObject prefabBody ;
	public GameObject prefabBoard ;
	public GameObject prefabTitle ;
	public GameObject noHeadOBJ ;
	public GameObject floorMother;
	public GameObject player;
	public Transform Bodys;
	static public bool gameStart = false ;
	static public int level = 1 ;
	static public int score = 0 ;
	static public float time = 5.0f ;
	static public float speed = 1.0f ; //越小越快
	static public float minSpeed = 0.01f ;
	static public int scene_wh = 6 ;
	static public bool triEatFood = false ;
	
	static public float test = 0f ;

	public AudioClip chickSE;
	public AudioClip chickenSE;
	public AudioClip clickSE;
	public AudioClip EDSE;
	
	private bool titleIn = false ;
	private bool first = true ;
	private int tmpX, tmpZ = 0 ;
	private GameObject OBJ ;
	private GameObject dOBJ ;
	private GameObject fOBJ ;
	private GameObject bOBJ ;
	private GameObject tOBJ ;
	private int arrowTo = 1 ; //1234 上下左右
	static public float stepTime = 0.5f;
	
	static public List<sbody> bodyList;
	private List<GameObject> floorList;
	//title
	public GameObject TitleObj;
	public GameObject GameOverObj;
	public GameObject GameOverObj1;
	public GameObject MainCamera;
	private bool cameraBack = false;
	public GameObject pausebutton;
	public GameObject HelpButton;
	public GameObject RankButton;
	public GameObject cucoo;
	public GameObject Destrub;
	public GameObject RankBarObj;
	public void Pause(){
		if(gameStart){
			iTween.MoveTo(pausebutton,iTween.Hash("x",-1.18f,"y",0.85f,"islocal",true,"time",1f));
			iTween.MoveTo(HelpButton,iTween.Hash("x",1.5f,"y",-1.17f,"islocal",true,"time",1f));
			iTween.MoveTo(RankButton,iTween.Hash("x",-1.57f,"y",-1.14f,"islocal",true,"time",1f));
		}
		else{
			iTween.MoveTo(pausebutton,iTween.Hash("x",-1.5f,"y",1.15f,"islocal",true,"time",1f));
		}
	}
	//foodCreater
	void foodCreater (){
		int ranX = 0, ranZ = 0 ;
		bool ok = true;
		do
		{
			ok = true;
			ranX = Random.Range(-6,6);
			ranZ = Random.Range(-6,6);
			for(int j=0;j<treeX.Count;j++)
			{
				//print (ranX+"/"+ranZ+"/"+treeX[j]+"/"+treeZ[j]);
				if(ranX == treeX[j] && ranZ == treeZ[j])
				{
					ok = false;
					j = maxTreeNum;
				}
			}
			if(ok)
			{
				foreach( sbody now in bodyList )
				{
					if(ranX == now.posX && ranZ == now.posZ)
					{
						ok = false;
					}
				}
			}
		}while(!ok);
		dOBJ = Instantiate(prefabDot,new Vector3(ranX,-1,ranZ),Quaternion.Euler(0,0,0)) as GameObject ;
		iTween.MoveTo(dOBJ,iTween.Hash("time",speed,"x",ranX,"y",0,"z",ranZ)); 

	}
	
	//body 生成功能
	void bodyCreat (){
		OBJ = Instantiate(prefabBody,new Vector3(tmpX,-1,tmpZ),Quaternion.Euler(0,0,0)) as GameObject;
		OBJ.transform.parent = Bodys;
		//iTween.MoveTo(OBJ,iTween.Hash("time",speed,"x",bodyList[(bodyList.Count-1)].posX,"y",0,"z",bodyList[(bodyList.Count-1)].posZ)); 
		iTween.MoveTo(OBJ,iTween.Hash("time",speed,"x",tmpX,"y",0,"z",tmpZ)); 
		bodyList.Add(new sbody(tmpX,tmpZ,OBJ));
		if(bodyList.Count > 1)
			bodyList[bodyList.Count-2].body.GetComponent<PlayerBody>()._nextBody = bodyList[bodyList.Count-1].body;
		//bodyList[bodyList.Count-1].body.tag = "Egg";
		bodyList[bodyList.Count-1].body.GetComponent<PlayerBody>().Change(0);
		for(int i=0;i<bodyList.Count-1;i++){
			//bodyList[i].body.tag = "Chick";
			bodyList[i].body.GetComponent<PlayerBody>().Change(1);
		}
	}
	void ButtonFadeIn(){
		iTween.MoveTo(HelpButton,iTween.Hash("x",1.15f,"y",-0.85f,"islocal",true,"time",1f));
		iTween.MoveTo(RankButton,iTween.Hash("x",-1.22f,"y",-0.86f,"islocal",true,"time",1f));
	}
	void Title(){
		AudioSource.PlayClipAtPoint(chickenSE,Vector3.zero);
		AudioSource.PlayClipAtPoint(chickSE,Vector3.zero);
		iTween.MoveTo(TitleObj,iTween.Hash("y",0,"time",1f,"islocal",true,"easetype",iTween.EaseType.easeOutBounce));
	}
	public void HelpItem(){
		iTween.MoveTo(Destrub,iTween.Hash("y",0,"time",1f,"islocal",true,"easetype",iTween.EaseType.easeOutBounce));
	}
	public void HelpItemBack(){
		iTween.MoveTo(Destrub,iTween.Hash("y",5,"time",1f,"islocal",true));
	}
	public void RankBar(){
		iTween.MoveTo(RankBarObj,iTween.Hash("y",0,"time",1f,"islocal",true,"easetype",iTween.EaseType.easeOutBounce));
	}
	public void RankBarBack(){
		iTween.MoveTo(RankBarObj,iTween.Hash("y",5,"time",1f,"islocal",true));
	}
	public void GameOver(int _type){
		AudioSource.PlayClipAtPoint(EDSE,Vector3.zero);
		if(_type == 0)
			iTween.MoveTo(GameOverObj,iTween.Hash("y",0,"time",1f,"islocal",true,"easetype",iTween.EaseType.easeOutBounce));
		else
			iTween.MoveTo(GameOverObj1,iTween.Hash("y",0,"time",1f,"islocal",true,"easetype",iTween.EaseType.easeOutBounce));

		gameStart = false;
	}

	void TitleCamera(){
		iTween.MoveTo(MainCamera,iTween.Hash("x",0,"y",20f,"z",-10f,"time",2f));
		cameraBack = false;
	}

	//Floor 生成功能
	void floorCreat (){
		int flX, flZ = 0 ;
		int[] floors = new int[196];
		for(int k=0;k<196;k++)
		{
			floors[k] = k;
		}
		for(int k=0;k<200;k++)
		{
			int _a = (int)Random.Range(0,196);
			int _b = (int)Random.Range(0,196);
			floors[195] = floors[_a];
			floors[_a] = floors[_b];
			floors[_b] = floors[195];
		}		
		int _count = 0;
		for (int i=-7;i<=7;i++){
			flZ=i;
			for (int j=-7;j<=7;j++){
				flX=j;
				int floor;
				bool isTree = false;
				for(int k = 0;k<maxTreeNum;k++)
				{
					if (_count == floors[k])
					{
						isTree = true;
						break;
					}
				}
				if(isTree)
				{
					if(i== -6 || i== 6 || j==-6 || j==6 || (i>-2 && i<2)|| (j>-2 && j<2))
					{
						floor = (int)Random.Range(0,3);
					}else
					{
						treeX.Add(j);
						treeZ.Add(i);
						floor = 3;
					}
				}
				else
					floor = (int)Random.Range(0,3);
				if(i == -7||i==7)
					floor = 5;
				if(j==-7||j==7)
					floor = 4;
				if(i==-7&&j==-7)
					floor = 6;
				if(i==-7&&j==7)
					floor = 7;
				if(i==7&&j==-7)
					floor = 8;
				if(i==7&&j==7)
					floor = 9;

				switch(floor)
				{
				case 4:
					fOBJ= Instantiate(prefabFloor[4],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(-90,0,0)) as GameObject;
						break;
				case 5:
					fOBJ= Instantiate(prefabFloor[4],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(-90,90,0)) as GameObject;
					break;
				case 6:
					fOBJ= Instantiate(prefabFloor[5],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(-90,-90,0)) as GameObject;
					break;
				case 7:
					fOBJ= Instantiate(prefabFloor[5],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(-90,180,0)) as GameObject;
					break;
				case 8:
					fOBJ= Instantiate(prefabFloor[5],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(-90,0,0)) as GameObject;
					break;
				case 9:
					fOBJ= Instantiate(prefabFloor[5],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(-90,90,0)) as GameObject;
					break;
				default:
					fOBJ= Instantiate(prefabFloor[floor],new Vector3(flX,-8*(j+i*0.5f),flZ),Quaternion.Euler(0,0,0)) as GameObject;
					break;
				}

				fOBJ.transform.parent = floorMother.transform;
				floorList.Add(fOBJ);
				iTween.MoveTo(fOBJ,iTween.Hash("time",speed,"easetype","easeOutBack","x",flX,"y",-0.8,"z",flZ,"delay",(j+i+15)*0.05));
				_count ++;
			}
		}
		iTween.MoveTo(fOBJ,iTween.Hash("time",speed,"x",0,"y",-0.0159111,"z",0,"delay",1));
		//bOBJ = Instantiate(prefabBoard,new Vector3(0,15,0),Quaternion.Euler(0,0,0)) as GameObject;
		//iTween.MoveTo(bOBJ,iTween.Hash("time",speed,"x",0,"y",-0.5,"z",0,"delay",1.8));
	}
	void GameInput(){
		if(arrowTo == 1){
			if(touchPoistion.x > player.transform.position.x){
				arrowTo = 3;
				touchPoistion = Vector3.zero;
			}
			else if(touchPoistion.x < player.transform.position.x){
				arrowTo = 4;
				touchPoistion = Vector3.zero;
			}
		}
		else if(arrowTo == 2){
			if(touchPoistion.x > player.transform.position.x){
				arrowTo = 3;
				touchPoistion = Vector3.zero;
			}
			else if(touchPoistion.x < player.transform.position.x){
				arrowTo = 4;
				touchPoistion = Vector3.zero;
			}
		}
		else if(arrowTo == 3){
			if(touchPoistion.z > player.transform.position.z){
				arrowTo = 2;
				touchPoistion = Vector3.zero;
			}
			else if(touchPoistion.z < player.transform.position.z){
				arrowTo = 1;
				touchPoistion = Vector3.zero;
			}
		}
		else if(arrowTo == 4){
			if(touchPoistion.z > player.transform.position.z){
				arrowTo = 2;
				touchPoistion = Vector3.zero;
			}
			else if(touchPoistion.z < player.transform.position.z){
				arrowTo = 1;
				touchPoistion = Vector3.zero;
			}
		}
	}

	void touch(){
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;	
			if(Physics.Raycast (ray ,out hit , rayDistance)){
				//hit.collider.gameObject.renderer.material.color = new Color(1,0,0,1);
				if(hit.collider.tag == "Floor" && iTween.Count(hit.collider.gameObject) == 0){
					AudioSource.PlayClipAtPoint(clickSE,Vector3.zero);
					touchPoistion = hit.point;
					iTween.MoveFrom(hit.collider.gameObject,iTween.Hash("y",-2f,"time",speed,"easetype",iTween.EaseType.easeOutBack));
					Instantiate(Test,new Vector3(hit.collider.transform.position.x,hit.collider.transform.position.y+1.2f,hit.collider.transform.position.z),Quaternion.Euler(0,0,0));
					GameInput();
				}
			}
		}
		/*
		if(Input.touchCount > 0){
			if(Input.GetTouch(i).phase == TouchPhase.Began){
				ray = this.camera.ScreenPointToRay (Input.GetTouch(i).position);
				RaycastHit hit;	
				if(Physics.Raycast(ray,out hit,rayDistance)){
					x = hit.collider.name;
				}
			}
		}
		*/

	}


	// Use this for initialization
	void Start () {
		//---------- reset static  ------------
		gameStart = false ;
		level = 1 ;
		score = 0 ;
		time = 5.0f ;
		speed = 1.0f ; //越小越快
		minSpeed = 0.01f ;
		scene_wh = 6 ;
		triEatFood = false ;
		stepTime = 0.5f;
		Pause();
		Invoke("ButtonFadeIn",3.5f);
		//---------- reset static end ------------
		
	}
	
	// Update is called once per frame
	void Update () {
	
		/*
		if(isTitle==true){
			if (titleIn==false){
				tOBJ = Instantiate(prefabTitle,new Vector3(0.5f,1.5f,0f),Quaternion.Euler(0,0,0)) as GameObject;
				iTween.MoveTo(tOBJ,iTween.Hash("time",speed,"x",0.5,"y",0.5,"z",0,"easetype","easeOutBack"));
				titleIn = true ;
			}
			
			if (Input.GetKeyDown(KeyCode.Space)){
				iTween.MoveTo(tOBJ,iTween.Hash("time",speed,"x",0.5,"y",-0.5,"z",0,"easetype","easeInBack"));
				isTitle = false ;
			}
		}
		*/
		if( first == true ){
			bodyList = new List<sbody>();
			floorList = new List<GameObject>();
			floorCreat();
			foodCreater();
			// 加入 bodyList[0] 為頭
			//bodyList.Add(new sbody(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z), noHeadOBJ));
			bodyCreat ();
			first = false ;
			Invoke("Title",2.5f);
			Invoke("TitleCamera",1.5f);
		}
		
		
		// --------------------- Pause Start--------------------------------------
		if(gameStart==false){
			if (Input.GetKeyDown(KeyCode.Space)){
				Application.LoadLevel(0);
			}
		}
		
		// --------------------- Pause END--------------------------------------
		
		
		// --------------------- Playing Start--------------------------------------
		if(gameStart==true){
			if(!cameraBack){
				iTween.MoveTo(MainCamera,iTween.Hash("x",0,"y",10.5f,"z",-7f,"time",2f));
				cameraBack = true;
			}
			touch();
			//--------------- Trigger Start ---------------
			if ( triEatFood == true ) {
				bodyCreat();
				if(bodyList.Count%4 == 0)
				{
					level ++;
					if (Controller.speed>= Controller.minSpeed){
						Controller.speed -= 0.1f ;
					}
					if (Controller.speed< Controller.minSpeed){
						Controller.speed = Controller.minSpeed ;
					}
					print(level);
					if(level == 4){

						cucoo.animation.CrossFade("Wakl_Fast",0.5f);
						for(int j=0;j<bodyList.Count;j++){
							bodyList[j].body.GetComponent<PlayerBody>().ChangeAni();
						}

					}
				}
				foodCreater();
				triEatFood = false ;

			}
			//--------------- Trigger END ---------------
			
			
			
			//按鍵判斷方向
			if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (Mathf.RoundToInt(player.transform.eulerAngles.y)!=180)){
				arrowTo = 1 ;
			}
			if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && (Mathf.RoundToInt(player.transform.eulerAngles.y)!=0)){
				arrowTo = 2 ;
			}
			if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && (Mathf.RoundToInt(player.transform.eulerAngles.y)!=90)){
				arrowTo = 3 ;
			}
			if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && (Mathf.RoundToInt(player.transform.eulerAngles.y)!=270)){
				arrowTo = 4 ;
			}
			
			//穿牆判斷
			/*if (player.transform.position.x>scene_wh){
				iTween.Stop(player);
				player.transform.position = new Vector3(-scene_wh,player.transform.position.y,player.transform.position.z);
			}
			if (player.transform.position.x<-scene_wh){
				iTween.Stop(player);
				player.transform.position = new Vector3(scene_wh,player.transform.position.y,player.transform.position.z);
			}
			if (player.transform.position.z>scene_wh){
				iTween.Stop(player);
				player.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-scene_wh);
			}
			if (player.transform.position.z<-scene_wh){
				iTween.Stop(player);
				player.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,scene_wh);
			}*/
			// stepTime 計算
			if (stepTime>0){
				stepTime -= Time.deltaTime ;
			}
			else{
				//換身體位置
				int nextX = Mathf.RoundToInt(player.transform.position.x) ;
				int nextZ = Mathf.RoundToInt(player.transform.position.z) ;
				foreach( sbody now in bodyList ){
					//更新 List 資料
					iTween.MoveTo(now.body,iTween.Hash("time",speed,"x",nextX,"y",0,"z",nextZ)); //更新 Body 位置
					//now.SetPosition(nextX,nextZ);
					tmpX = now.posX;
					tmpZ = now.posZ;
					now.posX = nextX;
					now.posZ = nextZ;
					nextX = tmpX;
					nextZ = tmpZ;
					//now.body.transform.position = new Vector3( now.posX, 0, now.posZ ) ;
					//now.SetPos(now.posX, now.posZ);
				}
				//頭移動+旋轉
				bodyList[0].body.GetComponent<PlayerBody>().Trun(arrowTo);
				/*for(int i=0;i<bodyList.Count;i++)
				{
					if(i == 0)
					{
						bodyList[0].body.GetComponent<PlayerBody>().Trun(arrowTo);
					}
					else
					{
						bodyList[i].body.GetComponent<PlayerBody>().Trun(bodyList[i-1].body.GetComponent<PlayerBody>().arrowTo);
					}
				}*/
				if ( arrowTo==1 ){
					iTween.MoveTo(player,iTween.Hash("time",speed,"z",player.transform.position.z+1f));
					player.transform.rotation = Quaternion.Euler(0,0,0);
					}
				if ( arrowTo==2 ){
					iTween.MoveTo(player,iTween.Hash("time",speed,"z",player.transform.position.z-1f));
					player.transform.rotation = Quaternion.Euler(0,180,0);
				}
				if ( arrowTo==3 ){
					iTween.MoveTo(player,iTween.Hash("time",speed,"x",player.transform.position.x-1f));
					player.transform.rotation = Quaternion.Euler(0,270,0);
				}
				if ( arrowTo==4 ){
					iTween.MoveTo(player,iTween.Hash("time",speed,"x",player.transform.position.x+1f));
					player.transform.rotation = Quaternion.Euler(0,90,0);
				}
				stepTime = speed;
				gameObject.GetComponent<ScoreManager>().StepScore(bodyList.Count,level);
			}
		}
		// --------------------- Playing END--------------------------------------
	}
}
