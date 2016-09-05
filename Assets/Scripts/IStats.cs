using UnityEngine;
using System.Collections;

public interface IStats{
	string Name {
		get;
		set;
	}
	float HealthPoints {
		get;
		set;
	}
	void Damage (float val);
	void Die ();
}
