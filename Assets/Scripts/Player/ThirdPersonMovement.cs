using UnityEngine;
using System.Collections;

public class ThirdPersonMovement : MonoBehaviour {
	//Character Configuration
	public float Speed = 1f;
	public float gravity = -3f;
	public float jumpSpeed = 1f;
	public float currentJumpSpeed = 0f;
	public bool jumping = false;
	public float jumpTime = 2f;


	public bool jumpingWall = false;
	public float currentWallJumpSpeed = 0f;
	public float walljumpTime;
	public float walljumpSpeed;
	public float wallJumpYMultiplier = 2f;

	public GameObject cameraPivot;
	//Inputs
	float vertical;
	float horizontal;
	float mouseX,mouseY,fire1,fire2;
	bool jump;
	//Private fields
	CharacterController Controller;
	Vector3 moveDirection;
	Vector3 jumpDirection;
	Ray wallCollisionRay;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		Controller = GetComponent<CharacterController>();

	}

	// Update is called once per frame
	void Update () {
		Debug.Log (Controller.collisionFlags);

		GetInputs ();
		moveDirection = Vector3.zero;

		//Move x,z
		Move();

		//Move y
		Jump();
		Gravity ();


		//Apply movements
		transform.Rotate(0f,mouseX,0f);
		Controller.Move (moveDirection * Time.deltaTime);

		//Camera Rotations
		cameraPivot.transform.Rotate (-mouseY,0f,0f);

	}

	void GetInputs(){
		vertical = Input.GetAxisRaw("Vertical");
		horizontal = Input.GetAxisRaw("Horizontal");
		mouseX = Input.GetAxisRaw("Mouse X");
		mouseY = Input.GetAxisRaw("Mouse Y");
		fire1 = Input.GetAxisRaw("Fire1");
		fire2 = Input.GetAxisRaw("Fire2");
		jump = Input.GetButtonDown ("Jump");
	}
	void Move(){
		moveDirection.z = vertical;
		moveDirection.x = horizontal;
		moveDirection = transform.TransformDirection( moveDirection ).normalized;
		moveDirection = moveDirection * Speed;
	}
	void Jump(){
		if (jump && Controller.isGrounded) {
			jumping = true;
			currentJumpSpeed = jumpSpeed;
		} 
		if(jump && IsWallClimbing ()) {
			jumpingWall = true;
			currentJumpSpeed = jumpSpeed * wallJumpYMultiplier;
			currentWallJumpSpeed = walljumpSpeed;
			jumpDirection = -moveDirection;
		}
		if(jumpingWall){
			//Y
			moveDirection.y = currentJumpSpeed;
			currentJumpSpeed -= Time.deltaTime * jumpTime * jumpSpeed;
			currentJumpSpeed = Mathf.Clamp ( currentJumpSpeed , 0f , jumpSpeed );
			//XZ
			moveDirection += currentWallJumpSpeed * jumpDirection;
			currentWallJumpSpeed -= Time.deltaTime * walljumpTime * walljumpSpeed;
			currentWallJumpSpeed = Mathf.Clamp ( currentWallJumpSpeed , 0f , walljumpSpeed );

			if (currentWallJumpSpeed == 0f) {
				jumpingWall = false;
			}
		}
		if (jumping) {
			moveDirection.y = currentJumpSpeed;
			currentJumpSpeed -= Time.deltaTime * jumpTime * jumpSpeed;
			currentJumpSpeed = Mathf.Clamp ( currentJumpSpeed , 0f , jumpSpeed );
			if (currentJumpSpeed == 0f) {
				jumping = false;
			}
		}
	}
	void Gravity(){
		if (IsWallClimbing ()) {
			currentJumpSpeed = 0f;
			jumping = false;
			moveDirection.y += gravity * 0.1f;
		} else {
			moveDirection.y += gravity;
		}
	}
	bool IsWallClimbing(){
		wallCollisionRay.origin = transform.position;
		wallCollisionRay.direction = new Vector3 (0f,-1f,0f).normalized;
		if ( Physics.Raycast ( wallCollisionRay , out hit , Controller.radius + Controller.skinWidth + 0.02f ) ) {
			return false;
		}
		wallCollisionRay.origin = transform.position + new Vector3(0f,-0.5f,0f);
		wallCollisionRay.direction = new Vector3 (moveDirection.x,0f,moveDirection.z);
		if ( Physics.Raycast ( wallCollisionRay , out hit , Controller.radius + Controller.skinWidth + 0.02f ) ) {
			return true;
		} else {
			return false;
		}
	}
}
