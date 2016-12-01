using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject PlayerArrow;
	public float xInput;
	public float yInput;
	Rigidbody2D playerRigidBody;
	float impulseRate;
	PlayerClass myInfo;

	public Animator anims;
	public AudioClip[] grunts;
	AudioSource audioPlayer;

	public CharacterSwap spriteHandler;

	// Conditional Checks
	public bool canJump;
	bool didJump;
	bool jumpHold;
	public bool grounded;
	public bool floatingCheck;
	public bool checkStarted;
	public bool hasBall;
	public GameObject theBall;
	public bool firstHit;

	Vector3 respawnPoint;

	// Use this for initialization
	void Start () {
	
		playerRigidBody = GetComponent<Rigidbody2D> ();
		myInfo = GetComponent<PlayerClass> ();
		audioPlayer = GetComponent<AudioSource> ();
		theBall = null;

        RegisterInput();

		canJump = true;
		didJump = false;
		grounded = false;
		hasBall = false;
		floatingCheck = true;
		checkStarted = false;
		firstHit = false;
		impulseRate = 15f;

		respawnPoint = this.transform.position;

		spriteHandler.setSprites (myInfo.Data.PlayerCountry);

	}
	
	// Update is called once per frame
	void Update () {
	
		calculateArrowAngle ();



		if (didJump) {
			if(hasBall){
				theBall.GetComponent<Script_GameBall>().pushBall(xInput, yInput);
				anims.SetTrigger ("Launch_Release");
			}else{
				Jump();
			}
		}

		if (!checkStarted && floatingCheck) {
			StartCoroutine(checkForRespawn());
		}

	}

	void calculateArrowAngle(){

		float arrowDegree;

        if (xInput == 0 && yInput == 0)
        {
            xInput = 1;
        }
	
		arrowDegree = Mathf.Atan2(yInput,xInput) * Mathf.Rad2Deg;

		PlayerArrow.transform.eulerAngles = new Vector3(0, 0, arrowDegree);

	}

	void Jump() {

		didJump = false;
		canJump = false;
		grounded = false;
		anims.SetBool ("Grounded", grounded);

		anims.SetTrigger ("Launch_Release");
		playerRigidBody.AddForce (new Vector2(xInput * impulseRate, yInput * impulseRate), ForceMode2D.Impulse); 

	}

    void RegisterInput()
    {
        InputControl.Instance.RegisterInputEvent(myInfo.Data.PlayerNum, new InputControlData.InputAction(UpdateValues));
    }

    void UpdateValues(float XAxis, float YAxis, bool down, bool up)
    {
        xInput = XAxis;
        yInput = YAxis;
        if (down)
        {
			jumpHold = true;
			anims.SetBool("Launch_Hold", jumpHold);
        }
		if(up && jumpHold)
		{
			jumpHold = false;
			anims.SetBool("Launch_Hold", jumpHold);
		}
		if (up && canJump) {
			didJump = true;
		}
    }

	void OnCollisionStay2D(Collision2D other){

		if (other.gameObject.tag == "Environment") {
			canJump = true;
			grounded = true;
			anims.SetBool ("Grounded", grounded);
			floatingCheck = false;
		}

	}

	void OnCollisionExit2D(Collision2D other){
		
		if (other.gameObject.tag == "Environment") {
			canJump = false;
			grounded = false;
			anims.SetBool ("Grounded", grounded);
			floatingCheck = true;
		}
		
	}

	void OnTriggerEnter2D(Collider2D other){

		PlayerController otherInfo;
	//	if(other.gameObject.tag == "Player")
			otherInfo = other.gameObject.GetComponent<PlayerController> ();

		// Swap position with other player if the other player is grounded 
		if (other.gameObject.tag == "Player" && grounded) {
			playerRigidBody.AddForce (new Vector2(otherInfo.xInput * -1 *impulseRate, otherInfo.yInput * -1 * impulseRate), ForceMode2D.Impulse);
			playGruntSound();
		}

		// Bounce off other player after first hit
		else if (other.gameObject.tag == "Player" && !grounded && !otherInfo.grounded && firstHit) {
			playerRigidBody.AddForce (new Vector2(xInput * -2 *impulseRate, yInput * -2 * impulseRate), ForceMode2D.Impulse);
			Debug.Log("Hey Player: " + myInfo.Data.PlayerNum);
			playGruntSound();
		}

		// Bounce off other player before first hit
		else if (other.gameObject.tag == "Player" && !grounded && !otherInfo.grounded && !firstHit) {
			playerRigidBody.AddForce (new Vector2(otherInfo.xInput * -2 *impulseRate, otherInfo.yInput * -2 * impulseRate), ForceMode2D.Impulse);
			Debug.Log("Hey Player: " + myInfo.Data.PlayerNum);
			playGruntSound();
		}

		// Take ball from collided player
		if (other.gameObject.tag == "Player" && otherInfo.hasBall) {
			BallGrab(otherInfo.theBall);
			otherInfo.BallLost();
			playGruntSound();
		}

		// Grab the ball
		if (other.gameObject.tag == "Ball") {

			//theBall.transform.parent = this.gameObject.transform.FindChild("Player Sprite").transform;
			BallGrab(other.gameObject);
		}
	}

	void BallGrab(GameObject ball){


		theBall = ball;
		theBall.GetComponent<Script_GameBall> ().StopBall();
		//theBall.GetComponent<Rigidbody2D> ().isKinematic = true;

		Debug.Log ("Catching the ball!");
		theBall.transform.parent = this.gameObject.transform;
		theBall.transform.localPosition = new Vector3(0, 0, 0);
		hasBall = true;
		theBall.gameObject.GetComponent<BallClass> ().assignPosession (this.gameObject);
	}

	public void BallLost(){
		theBall = null;
		hasBall = false;
	}

	void OnTriggerExit2D(Collider2D other){

		if (other.gameObject.tag == "Ball") {
			theBall = null;
			hasBall = false;
			didJump = false;
		}

	}

	IEnumerator delayedJump(){

		anims.SetTrigger ("Launch_Trigger");
		yield return new WaitForSeconds (.2f);
		playerRigidBody.AddForce (new Vector2(xInput * impulseRate, yInput * impulseRate), ForceMode2D.Impulse); 


	}

	IEnumerator checkForRespawn(){

		checkStarted = true;

		for (int i = 0; i<50; i++) {
			yield return new WaitForSeconds (.1f);
			if(grounded || !floatingCheck){
				floatingCheck = false;
				break;
			}
		}
		if (floatingCheck) {
			transform.position = respawnPoint;
			playerRigidBody.velocity = Vector3.zero;
			canJump = true;
			firstHit = false;
		}
		checkStarted = false;
	}

	void playGruntSound(){

		audioPlayer.PlayOneShot (grunts [Random.Range (0, grunts.Length)]);

	}

}
