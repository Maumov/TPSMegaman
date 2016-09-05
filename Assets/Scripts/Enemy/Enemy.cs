using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IStats {
	public ParticleSystem DeathEffect;

	string name;
	public string Name{
		get{ 
			return name;
		}
		set{ 
			name = value;

		}
	}

	float healthPoints;
	public float HealthPoints{
		get{ 
			return healthPoints;
		}
		set{ 
			healthPoints = value;
			if(healthPoints <= 0f){
				Die ();
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Damage(float val){
		HealthPoints -= val;
		Debug.Log ("received damage: "+val);
	}
	public void Die(){
		Instantiate(DeathEffect,transform.position,Quaternion.identity);
		Destroy (gameObject);
	}
}
