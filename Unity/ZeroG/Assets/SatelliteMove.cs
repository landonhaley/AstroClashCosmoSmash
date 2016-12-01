using UnityEngine;
using System.Collections;

public class SatelliteMove : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start () 
	{
		moveSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
	}
}
