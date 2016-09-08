using UnityEngine;
using System.Collections;
[RequireComponent (typeof(EnemyMovement))]
[RequireComponent (typeof(NavMeshAgent))]
public class EnemyAttack : MonoBehaviour {
	public enum EnemyType{Chaser, Turret, None}
	public EnemyType enemyType;
	[Header ("Weapon")]
	public GameObject bullet;

	[Header ("Status")]
	public bool isAttacking;
	public float timeBetweenAttacks;
	float nextShot;

	Transform target;
	Vector3 returnPosition;
	EnemyMovement enemyMovement;
	FieldOfView fov;
	NavMeshAgent agent;
	Vector3 targetLastSeen;

	// Use this for initialization
	void Start () {
		nextShot = Time.time;
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	bool hasTarget(){
		if(fov == null){
			fov = enemyMovement.fov;
		}
		return fov.visibleTargets.Count > 0;
	}
	public void Attack(){
		if(hasTarget() && !isAttacking){
			target = fov.visibleTargets[0];
			StartCoroutine(AttackTarget());	
		}

	}
	IEnumerator AttackTarget(){
		Debug.Log ("Started AttackTarget");
		returnPosition = transform.position;
		targetLastSeen = returnPosition;
		if(!isAttacking){
			isAttacking = true;

		}	
		while(target != null){
			agent.Stop ();
			transform.LookAt(target.position);
			if(fov.visibleTargets.Contains(target)){
				targetLastSeen = target.transform.position;
				Shoot();	
			}else{
				//LOS lost from target, Proceed to search for target at last seen position.
				if(enemyType.Equals(EnemyType.Chaser)){
					yield return StartCoroutine (SearchTargetAtLastSeenPosition (targetLastSeen));	
				}
				if(enemyType.Equals(EnemyType.Turret)){
					target = null;
				}
			}
			yield return null;
		}
		Debug.Log ("End AttackTarget");

		yield return null;
	}
	void Shoot(){
		if(nextShot < Time.time){
			nextShot = Time.time + timeBetweenAttacks;
			GameObject b = (GameObject)Instantiate (bullet , transform.position + transform.forward , transform.rotation );	
			b.GetComponent<Bullet>().StartTravel(target.position - transform.position);	
			//Debug.Log ("Attacking, "+ target.name);
		}

	}

	IEnumerator BackToPosition(){
		Debug.Log ("Start Back to position");

		agent.ResetPath ();
		agent.SetDestination(returnPosition);
		while(agent.pathPending){
			yield return null;
		}
		agent.Resume ();
		isAttacking = false;
		Debug.Log ("End Back to position");
		//yield return null;
	}

	IEnumerator SearchTargetAtLastSeenPosition(Vector3 pos){
		Debug.Log ("Start SearchTargetAtLastSeenPosition");
		agent.ResetPath ();
		agent.SetDestination(pos);
		while(agent.pathPending || !agent.hasPath){
			yield return null;
		}
		agent.Resume ();
		bool targetFound = false;
		Debug.Log(agent.hasPath);
		while(!targetFound && agent.hasPath){
			if(hasTarget ()){
				targetFound  = true;
				target = fov.visibleTargets[0];
				targetLastSeen = target.transform.position;
				Debug.Log("Target found");
			}
			yield return null;
		}

		if(!targetFound){
			Debug.Log("No Target found");
			target = null;
			StartCoroutine(BackToPosition());
		}
		Debug.Log ("End SearchTargetAtLastSeenPosition");
		//yield return null;
	
	}
}
