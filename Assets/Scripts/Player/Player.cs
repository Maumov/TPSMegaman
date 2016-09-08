using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player : IStats{

	//Camera
	public GameObject Camera;
	//UI
	//public Text nameUI;
	public Text hpUI;



	// Use this for initialization
	void Start () {
		Name = PlayerPrefs.GetString("Player Name","Player1");
		Camera.transform.SetParent(null);
	}

	// Update is called once per frame
	void Update () {

	}
	public override void Damage(float val){
		base.Damage(val);
		hpUI.text = HealthPoints.ToString();
	}
	public override void Die(){
		//Debug.Log ("Player died");
	}


}
