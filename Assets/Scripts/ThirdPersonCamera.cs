using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	GameObject position;

	[Range(0f,1f)] public float Smoothness = 0.2f;
	// Use this for initialization
	void Start () {
		position = GameObject.FindGameObjectWithTag("PlayerCameraPosition");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position,position.transform.position,Smoothness*Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation,position.transform.rotation,Smoothness*Time.deltaTime);
	}
}
