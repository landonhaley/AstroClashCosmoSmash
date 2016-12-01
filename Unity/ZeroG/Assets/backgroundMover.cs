using UnityEngine;
using System.Collections;

public class backgroundMover : MonoBehaviour {

	public float rate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = new Vector2 ((transform.position.x - rate), transform.position.y);

	}
}
