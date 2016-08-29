using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour ,IStats{
	public GameObject Camera;

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
		Camera.transform.SetParent(null);
	}

	// Update is called once per frame
	void Update () {

	}
	public void Damage(float val){
		HealthPoints -= val;
		Debug.Log ("received damage");
	}
	public void Die(){
		Debug.Log ("Player died");
	}
}
