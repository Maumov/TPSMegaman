using UnityEngine;
using System.Collections;

public class Enemy : IStats {
	public GameObject DeathEffect;


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
		Destroy (((GameObject)Instantiate(DeathEffect,transform.position,Quaternion.identity)).gameObject, DeathEffect.GetComponent<ParticleSystem>().duration);
		base.Die();
	}
}
