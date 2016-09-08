using UnityEngine;
using System.Collections;
[System.Serializable]
public abstract class IStats : MonoBehaviour{

	[SerializeField]
	string _name;

	public string Name{
		get{
			return _name;
		}
		set{
			_name = value;
		}
	}
	[SerializeField]
	float _healthPoints;

	public float HealthPoints{
		get{
			return _healthPoints;
		} 
		set{
			_healthPoints = value;
		} 

	}

	public virtual void Damage (float val){
		HealthPoints -= val;
		Debug.Log ("received damage: "+val);
		if(HealthPoints <= 0){
			Die();
		}
	}
	public virtual void Die(){
		Debug.Log(name + ", died.");
		Destroy (gameObject);
	}
}
