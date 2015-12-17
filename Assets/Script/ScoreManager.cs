using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	static public int score = 0;
	
	void Start()
	{
		score = 0;
	}
	
	public void StepScore(int _bodyNum,int _level)
	{
		//score += ((_bodyNum*_bodyNum)-_bodyNum)*_level;
		score += (_bodyNum-1)*_level;
	}
}
