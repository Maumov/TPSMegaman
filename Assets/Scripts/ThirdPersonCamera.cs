using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	GameObject position;

	public float Smoothness = 1f;
	// Use this for initialization
	void Start () {
		position = GameObject.FindGameObjectWithTag("PlayerCameraPosition");
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = Vector3.Lerp(transform.position,position.transform.position,Time.deltaTime * Smoothness);
		transform.rotation = Quaternion.Slerp(transform.rotation,position.transform.rotation,Time.deltaTime * Smoothness);

	}
}
