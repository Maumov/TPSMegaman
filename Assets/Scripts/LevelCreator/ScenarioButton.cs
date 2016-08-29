using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenarioButton : MonoBehaviour {
	public void GotPressed(){
		LevelLoader.MapSelected = name;
		SceneManager.LoadScene("Scenario");
	}
}
