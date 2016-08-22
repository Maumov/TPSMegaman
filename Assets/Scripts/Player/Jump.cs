using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	//Character Configuration
	public float jumpSpeed = 1f;

	//Inputs
	bool jump;

	//Private fields
	CharacterController Controller;
	Vector3 moveDirection;
	public float currentJumpSpeed = 0f;
	public bool jumping = false;
	public float jumpTime = 2f;
	// Use this for initialization
	void Start () {
		Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Controller.collisionFlags);
		GetInputs ();
		moveDirection = Vector3.zero;
		//Move y
		if(jump && Controller.isGrounded){
			jumping = true;
			currentJumpSpeed = jumpSpeed;
		}
		if(jumping){
			moveDirection.y = currentJumpSpeed;
			currentJumpSpeed -= Time.deltaTime * jumpTime * jumpSpeed;
			currentJumpSpeed = Mathf.Clamp (currentJumpSpeed, 0f, jumpSpeed);
			if(currentJumpSpeed == 0f){
				jumping = false;
			}
		}
		Controller.Move (moveDirection * Time.deltaTime);
	}
	void GetInputs(){
		
		jump = Input.GetButtonDown ("Jump");
	}

}
