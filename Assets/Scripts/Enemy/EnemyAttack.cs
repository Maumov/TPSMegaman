using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	[Header ("Weapon")]
	public Bullet bullet;

	[Header ("Status")]
	public bool isAttacking;
	public float timeBetweenAttacks;
	float nextShot;

	Transform target;
	Vector3 returnPosition;
	FieldOfView fov;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		nextShot = Time.time;
		fov = GetComponent<FieldOfView>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	bool hasTarget(){
		Debug.Log(fov);
		Debug.Log(fov.visibleTargets);
		return fov.visibleTargets.Count > 0;
	}
	public void Attack(){
		if(hasTarget()){
			this.target = target;
			StartCoroutine(AttackTarget());	
		}

	}
	IEnumerator AttackTarget(){
		returnPosition = transform.position;
		if(!isAttacking){
			isAttacking = true;
		}	
		while(target != null){
			agent.Stop();
			if(fov.visibleTargets.Contains(target)){
				Shoot();	
			}else{
				target = null;
				StartCoroutine(BackToPosition());
			}
			yield return null;
		}

		yield return null;
	}
	void Shoot(){
		if(nextShot < Time.time){
			nextShot = Time.time + timeBetweenAttacks;
			Instantiate(bullet,transform.position, Quaternion.LookRotation(target.position - transform.position));	
		}

	}
	IEnumerator BackToPosition(){
		
		agent.SetDestination(returnPosition);
		isAttacking = false;

		yield return null;
	}
}
