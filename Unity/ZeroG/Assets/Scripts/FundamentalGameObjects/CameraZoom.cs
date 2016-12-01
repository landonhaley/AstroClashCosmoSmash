using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoom : MonoBehaviour {
	
	Camera cam;
	Vector3 startingPosition;
	GameObject[] players;
	public float baseZoom;
	public float ratio;
	public float minZoom;
	public float maxZoom;
	
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		players = GameObject.FindGameObjectsWithTag("Player");
		
		if(Time.timeScale == 0){return;}
		
		List<float> xPos = new List<float>();
		List<float> yPos = new List<float>();
		
		foreach(GameObject obj in players){
			xPos.Add(obj.transform.position.x);
			yPos.Add(obj.transform.position.y);
		}
		float minX = Mathf.Min (xPos.ToArray());
		float maxX = Mathf.Max (xPos.ToArray());
		float minY = Mathf.Min (yPos.ToArray());
		float maxY = Mathf.Max (yPos.ToArray());
		float dist = Vector2.Distance(new Vector2(minX,minY),new Vector2(maxX,maxY));
		
		Vector3 center = new Vector3((minX + maxX) /2, (minY + maxY) /2, 0f);
		transform.position = center * 0.05f + transform.position * 0.95f;
		cam.fieldOfView = cam.fieldOfView * 0.95f + Mathf.Clamp(baseZoom+dist*ratio,minZoom,maxZoom) * 0.05f;
	}
}
