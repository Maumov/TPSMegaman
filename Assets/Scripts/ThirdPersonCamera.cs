using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public Transform target;
	public GameObject player;
	public float Smoothness = 1f;
	public Vector3 direction;


	public float distance;
	RaycastHit hit;

	Vector3 targetPosition;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		

		//targetPosition = position.transform.position; 
		transform.position = Vector3.Lerp(transform.position,target.position,Time.deltaTime * Smoothness);
		transform.rotation = Quaternion.Slerp(transform.rotation,target.rotation,Time.deltaTime * Smoothness);

	}

}
