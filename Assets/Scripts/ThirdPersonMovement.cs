using UnityEngine;
using System.Collections;

public class ThirdPersonMovement : MonoBehaviour {
	public float Speed = 1f;
	CharacterController Controller;
	float vertical;
	float horizontal;
	Vector3 moveDirection;
	// Use this for initialization
	void Start () {
		Controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {
		moveDirection = Vector3.zero;
		vertical = Input.GetAxisRaw("Vertical");
		horizontal = Input.GetAxisRaw("Horizontal");
		moveDirection.z = vertical;
		moveDirection = transform.TransformDirection(moveDirection);
		transform.Rotate(0f,horizontal*Speed*25*Time.deltaTime,0f);
		Controller.Move(moveDirection*Speed*Time.deltaTime);
	}
}
