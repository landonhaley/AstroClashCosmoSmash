using UnityEngine;
using System.Collections;

public class Script_GameBall : MonoBehaviour {


	private int bounceCount;

	private Rigidbody2D ballRigidBody;
	private CircleCollider2D ballCollider;
	private PhysicsMaterial2D bounceMat;

	public int forceValue;
	public Camera gameCamera;
	public Vector3 target;


	void Start () 
	{
		bounceCount = 0;

		ballRigidBody = gameObject.GetComponent<Rigidbody2D>();
		ballCollider = gameObject.GetComponent<CircleCollider2D>();
		bounceMat = ballCollider.sharedMaterial;
	}
	

	void Update () 
	{	
		//target = gameCamera.ScreenToWorldPoint(Input.mousePosition);

		//if(Input.GetMouseButtonDown(0))
		//	ballRigidBody.AddForce(target * forceValue * Time.deltaTime, ForceMode2D.Impulse);

		//if (transform.parent == null)
			//ballRigidBody.isKinematic = true;

		if (!ballRigidBody.isKinematic && bounceCount == 0 && transform.parent == null) {
			ballRigidBody.isKinematic = true;
		}
	}

	void OnCollisionEnter2D( Collision2D col)
	{
		if(col.collider.tag == "Environment")
		{
			bounceCount++;
			
			if(bounceCount == 4)
				StopBounce();
		}

		if (col.gameObject.tag == "Player") {
			ballRigidBody.isKinematic = true;
		}
	}

	public void pushBall(float pushX, float pushY){
		
		ballRigidBody.isKinematic = false;
		bounceCount++;
		ballRigidBody.AddForce(new Vector2(pushX, pushY) * forceValue * Time.deltaTime, ForceMode2D.Impulse);
		//ballRigidBody.isKinematic = false;

	}

	void StopBounce()
	{
		ballRigidBody.velocity = Vector3.zero;
		ballRigidBody.isKinematic = true;
		bounceCount = 0;
	}

	public void StopBall()
	{
		ballRigidBody.velocity = Vector3.zero;
		//ballRigidBody.isKinematic = true;
		bounceCount = 0;
	}

	void OnTriggerExit2D(Collider2D col){

		if (col.gameObject.tag == "Player") {
			transform.parent = null;
		}

	}

	IEnumerator ballKinematicReset(){

		yield return new WaitForSeconds (3f);
		if (!ballRigidBody.isKinematic && bounceCount == 0 && transform.parent == null) {
			ballRigidBody.isKinematic = true;
		}

	}

}
