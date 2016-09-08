using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour {
	public enum MoveType{Patrol, Idle, None}
	public enum ObserveType{Rotate, Clamp, Fixed}

	public MoveType moveType;
	public ObserveType observeType;
	public FieldOfView fov;
	[Header ("Patrol points")]
	public Vector3[] points;
	int currentTargetPoint = 0;

	NavMeshAgent agent;

	EnemyAttack attack;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		if(fov == null){
			fov = GetComponent<FieldOfView>();	
		}
		attack = GetComponent<EnemyAttack>();

		switch(moveType){
		case MoveType.Idle:
			StartCoroutine(Idle());
			break;
		case MoveType.Patrol:
			StartCoroutine(Patrol());
			break;
		case MoveType.None:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch(observeType){
		case ObserveType.Rotate:

			break;
		case ObserveType.Clamp:
			
			break;
		case ObserveType.Fixed:
			
			break;
		}
	}
	void FoVRotate(){
		
	}

	void FoVClamp(){
		
	}

	void FoVFixed(){
		
	}

	IEnumerator Idle(){
		while(true){
			attack.Attack();
			yield return null;	
		}
		yield return null;
	}

	IEnumerator Patrol() {
		while(true){
			attack.Attack();
			if(!attack.isAttacking){
				if(!agent.hasPath){
					if(currentTargetPoint >= points.Length){
						currentTargetPoint = 0;	
					}
					agent.SetDestination(points[currentTargetPoint]);	
					currentTargetPoint++;
				}	

			}
			yield return null;	
		}


	}

}
