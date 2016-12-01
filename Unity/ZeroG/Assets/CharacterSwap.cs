using UnityEngine;
using System.Collections;

public class CharacterSwap : MonoBehaviour {

	// My Sprites
	public SpriteRenderer myTorso;
	public SpriteRenderer myFrontArm;
	public SpriteRenderer myBackArm;
	public SpriteRenderer myFrontLeg;
	public SpriteRenderer myBackLeg;

	// Storage Arrays
	public Sprite[] torso;
	public Sprite[] frontArm;
	public Sprite[] backArm;
	public Sprite[] frontLeg;
	public Sprite[] backLeg;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setSprites(PlayerData.Country myCountry){

		int index = 0;

		switch (myCountry) {

		case PlayerData.Country.China:
			index = 0;
			break;
		case PlayerData.Country.Japan:
			index = 1;
			break;
		case PlayerData.Country.Russia:
			index = 2;
			break;
		case PlayerData.Country.USA:
			index = 3;
			break;
		}

		myTorso.sprite = torso[index];
		myFrontArm.sprite = frontArm[index];
		myBackArm.sprite = backArm[index];
		myFrontLeg.sprite = frontLeg[index];
		myBackLeg.sprite = backLeg[index];

	}
}
