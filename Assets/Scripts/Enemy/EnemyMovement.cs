using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyMovement : MonoBehaviour {
	public enum MoveType{Patrol, Idle}
	public enum ObserveType{Rotate, Clamp, Fixed}

	public MoveType moveType;
	public ObserveType observeType;
	[Header ("Patrol points")]
	public Vector3[] points;
	int currentTargetPoint = 0;

	NavMeshAgent agent;
	FieldOfView fov;
	EnemyAttack attack;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		fov = GetComponent<FieldOfView>();
		attack = GetComponent<EnemyAttack>();
		switch(observeType){
		case ObserveType.Rotate:
			break;
		case ObserveType.Clamp:
			break;
		case ObserveType.Fixed:
			break;
		}
		switch(moveType){
		case MoveType.Idle:
			break;
		case MoveType.Patrol:
			StartCoroutine(Patrol());
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Idle(){
		yield return null;
	}

	IEnumerator Patrol() {
		while(true){
			attack.Attack();
			if(!attack.isAttacking){
				if(!agent.hasPath){
					currentTargetPoint++;
					if(currentTargetPoint >= points.Length){
						currentTargetPoint = 0;	
					}
					agent.SetDestination(points[currentTargetPoint]);	
				}	

			}
			yield return null;	
		}


	}

}
