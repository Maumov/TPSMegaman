using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Runtime.Remoting.Lifetime;

public class Bullet : MonoBehaviour {
	Vector3 direction;
	public float Speed = 1f;

	public GameObject Nulled, Hit, Death;

	bool start;
	public float damage;
	public float lifeTime = 30f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(start){
			transform.position += direction * Speed * Time.deltaTime;
		}

	}
	public void StartTravel(Vector3 dir){
		direction = dir;
		//Destroy(gameObject,lifeTime);
		Invoke("DeathTimeReached",lifeTime);
		start = true;

	}
	void OnTriggerEnter(Collider other){
		if(other.GetComponent <IStats>() != null)
		{
			other.GetComponent <IStats> ().Damage (damage);
			Destroy (((GameObject)Instantiate(Hit,transform.position,Quaternion.identity)).gameObject, 5f);
		}else{
			Destroy (((GameObject)Instantiate(Nulled,transform.position,Quaternion.identity)).gameObject, 5f);
		}
		Destroy (gameObject);
	}
	void DeathTimeReached(){
		Destroy (((GameObject)Instantiate(Death,transform.position,Quaternion.identity)).gameObject, 5f);
		Destroy (gameObject);
	}

}
