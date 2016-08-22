using UnityEngine;
using System.Collections;

public interface IStats{
	float HealthPoints {
		get;
		set;
	}
	void Damage (float val);
	void Die ();
}
