﻿using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {


	public float rotationSpeed;
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
	}
}
