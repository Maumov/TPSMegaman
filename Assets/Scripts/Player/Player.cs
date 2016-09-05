using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : MonoBehaviour ,IStats{

	//Camera
	public GameObject Camera;
	//UI
	//public Text nameUI;
	public Text hpUI;

	//IStats
	string name;
	public string Name{
		get{ 
			return name;
		}
		set{ 
			name = value;

		}
	}
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
		Name = PlayerPrefs.GetString("Player Name","Player1");
		Camera.transform.SetParent(null);
	}

	// Update is called once per frame
	void Update () {

	}
	public void Damage(float val){
		HealthPoints -= val;
		hpUI.text = HealthPoints.ToString();
		Debug.Log ("received damage");
	}
	public void Die(){
		Debug.Log ("Player died");
	}


}
