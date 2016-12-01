using UnityEngine;
using System.Collections;

public class Satellites : MonoBehaviour {

	public GameObject[] satellites;
	public float countDown;
	private float originalCountdown;
	private int index;

	// Use this for initialization
	void Start () 
	{
		index = 0;
		countDown = 3;
		originalCountdown = countDown;
	}
	
	// Update is called once per frame
	void Update () 
	{
		countDown -= Time.deltaTime;

		if(countDown <= 0)
		{
			SpawnSatellite();
		}
	}

	void SpawnSatellite()
	{
		if(index < 2)
			index++;
		else
			index=0;
		Instantiate(satellites[index], transform.position, transform.rotation);
		countDown = originalCountdown;
	}
}
