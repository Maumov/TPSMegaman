using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public GameObject position;
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
		
		if(Physics.Raycast(player.transform.position,position.transform.position - player.transform.position,out hit, distance)){
			targetPosition = hit.point;
		}else{
			targetPosition = position.transform.position; 
		}

		transform.position = Vector3.Lerp(transform.position,targetPosition,Time.deltaTime * Smoothness);
		transform.rotation = Quaternion.Slerp(transform.rotation,position.transform.rotation,Time.deltaTime * Smoothness);

	}

}
