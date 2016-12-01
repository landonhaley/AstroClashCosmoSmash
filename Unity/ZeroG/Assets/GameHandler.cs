using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameHandler : MonoBehaviour {
	
	private float timeLeft;
	private int minsPerSec = 60 ;
	private int minsPerMatch = 180;
	private int mins;
	private int seconds;
	private string clockSymbol;
	private Action endGame;
	public GameObject LeaderBoard;
	public GameObject Timer;
	
	// Use this for initialization
	void Start () {
		endGame += stopTimer;
		endGame += showLeaderBoard;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeLeft > 0) {
			timeLeft = minsPerMatch - Time.timeSinceLevelLoad;
			mins = (int)(timeLeft / minsPerSec);
			seconds = (int)timeLeft - (mins * 60);
			if (seconds < 10)
				clockSymbol = ":0";
			else
				clockSymbol = ":";
			Timer.GetComponent<Text> ().text = mins + clockSymbol + seconds;
		}
		else {
			endGame();
		}
	}
	
	void stopTimer(){
		Timer.active = false;
	}
	
	void showLeaderBoard(){
		LeaderBoard.active = true;
	}
}
