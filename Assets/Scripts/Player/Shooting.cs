using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public float damage;
	Ray ray;
	Vector3 direction;
	GameObject endPoint,target;
	RaycastHit hit;
	float mouseX,mouseY,fire2;
	public GameObject PositionOfHit;
	public GameObject Bullet;
	bool fire1;
	//Charge Bullet
	public float LoadBullet = 0f;
	public LayerMask mask;
	// Use this for initialization
	void Start () {
		ray = new Ray ();
		endPoint = GameObject.FindGameObjectWithTag ("Shoot");
	}
	
	// Update is called once per frame
	void Update () {
		GetInputs ();
		Aim ();
		Shoot ();
	}
	void GetInputs(){
		
		mouseX = Input.GetAxisRaw("Mouse X");
		mouseY = Input.GetAxisRaw("Mouse Y");
		LoadBullet += Input.GetAxisRaw("Fire1") * Time.deltaTime;
		LoadBullet = Mathf.Clamp (LoadBullet, 0f, 2f);
		fire1 = Input.GetButtonUp("Fire1");
		fire2 = Input.GetAxisRaw("Fire2");

	}
	void Aim(){
		
		direction = endPoint.transform.position - transform.position ;
		direction.Normalize ();
		ray.origin = transform.position;
		ray.direction = direction;

		if (Physics.Raycast (ray.origin,ray.direction, out hit,100f,mask)) {
			PositionOfHit.SetActive (true);
			PositionOfHit.transform.position = hit.point;
		} else {
			PositionOfHit.SetActive (false);
		}

	}

	void Shoot(){
		if(fire1){
			GameObject bullet = (GameObject)Instantiate (Bullet,transform.position + (direction * 2f),transform.rotation);
			bullet.GetComponent<Bullet>().StartTravel(direction,damage,(int)(LoadBullet +1f));	
			LoadBullet = 0f;
		}

	}

}
