using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlCenter : Singleton<ControlCenter> {

	public int Team1Score = 0;
	public int Team2Score = 0;
	public List<PlayerData> _playerData;
	
	public void Awake() 
    {
        
	}

    public void Quit() 
    {
        Application.Quit();
    }

    public void LoadTeamSelect()
    {
        Application.LoadLevel("Team Select");
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void LoadGame(List<PlayerData> data)
    {
		_playerData = data;
        Application.LoadLevel("The Arena");
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void LoadScore()
    {
		_playerData = PlayerControl.Instance.GetAllPlayerData ();
        Application.LoadLevel("Score");
        GameObject.DontDestroyOnLoad(gameObject);
    }

	public override void Destroyed(){}
}
