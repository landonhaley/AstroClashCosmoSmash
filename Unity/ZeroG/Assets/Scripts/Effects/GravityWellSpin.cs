using UnityEngine;
using System.Collections;

public class GravityWellSpin : MonoBehaviour {

	public GameObject GravityRim;
	public GameObject GravityAbsorb;
	public GameObject GravityVoid;

	public float speed1;
	public float speed2;
	public float speed3;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GravityRim.transform.Rotate(new Vector3(0f,0f,speed1)*Time.deltaTime);
		GravityAbsorb.transform.Rotate(new Vector3(0f,0f,speed2)*Time.deltaTime);
		GravityVoid.transform.Rotate(new Vector3(0f,0f,speed3)*Time.deltaTime);
	}
}
