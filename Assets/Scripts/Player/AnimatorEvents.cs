using UnityEngine;
using System.Collections;

public class AnimatorEvents : MonoBehaviour {
	public Shooting shooting;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Shoot(){
		shooting.Shoot ();
	}
}
