using UnityEngine;
using System.Collections;

public class Enemy : IStats {
	public ParticleSystem DeathEffect;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void Damage(float val){
		
		base.Damage(val);
	}
	public override void Die(){
		Instantiate(DeathEffect,transform.position,Quaternion.identity);
		base.Die();
	}
}
