using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Runtime.Remoting.Lifetime;

public class Bullet : MonoBehaviour {
	public Vector3 direction;
	public float Speed = 1f;
	bool start;
	float damage;
	float lifeTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(start){
			transform.position += direction * Speed * Time.deltaTime;
			lifeTime -= Time.deltaTime;
			if(lifeTime <= 0f){
				
				Destroy (gameObject);
			}
		}

	}
	public void StartTravel(Vector3 dir, float dmg){
		damage = dmg;
		direction = dir;
		lifeTime = 30f;
		start = true;

	}
	void OnTriggerEnter(Collider other){
		if(other.GetComponent <IStats>() != null)
		{
			other.GetComponent <IStats> ().Damage (damage);
		}
		Destroy (gameObject);
	}
}
