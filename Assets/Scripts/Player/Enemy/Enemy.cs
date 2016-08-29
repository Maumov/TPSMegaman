﻿using UnityEngine;
using System.Collections;



public class Enemy : MonoBehaviour, IStats {
	float healthPoints = 100f;

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
		Destroy (gameObject);
	}
}
