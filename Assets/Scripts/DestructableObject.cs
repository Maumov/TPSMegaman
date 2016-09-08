using UnityEngine;
using System.Collections;

public class DestructableObject : IStats {

	public GameObject DeathEffect;

	public override void Die(){
		Destroy (((GameObject)Instantiate(DeathEffect,transform.position,Quaternion.identity)).gameObject, DeathEffect.GetComponent<ParticleSystem>().duration);
		base.Die();
	}
}
