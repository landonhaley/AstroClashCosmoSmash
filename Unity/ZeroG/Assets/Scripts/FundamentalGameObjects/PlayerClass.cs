using UnityEngine;
using System.Collections;

public class PlayerClass : MonoBehaviour {
	SpriteRenderer[] playerSprites; 
	
    private PlayerData _data;

    public PlayerData Data
    {
        get
        {
            if(_data != null)
            return _data;
        else
            return new PlayerData();
        }
        set
        {
            _data = value;
        }
    }

	// Use this for initialization
	void Start () {

	}

    public void LoadPlayerData(PlayerData data)
    {
        Data = data;
    }
	
	// Update is called once per frame
	void Update () {
		
		
	}

}