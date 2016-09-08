using UnityEngine;
using System.Collections;

public class Pressed : MonoBehaviour {
	

	RectTransform trans;

	float screenHeight,screenWidth;

	bool busy = false;
	//Y
	public float pos;
	public float speed;
	public float[] Positions;
	int currentPosition;

	bool ShowingScenes;
	//prefabsbuttons
	public RectTransform scenariosHolder;
	public GameObject scenarioPrefab;

	void Start(){
		trans = GetComponent<RectTransform>();
		currentPosition = 0;
		StartCoroutine("MoveY");
	}

	void UpdateScreenValues(){
		screenHeight = Screen.height - trans.sizeDelta.y;
		screenWidth = Screen.width - trans.sizeDelta.x;		
	}

	void Update(){
		
	}

	public void IGotPressed(){
		if(!busy){
			UpdateScreenValues();
			currentPosition += 1;
			if(currentPosition == Positions.Length){
				currentPosition = 0;
			}
			if(!ShowingScenes){
				ShowingScenes = true;
				StartCoroutine("ShowScenariosButtons");	
			}else{
				ShowingScenes = false;
				HideScenariosButtons();			
			}
		}
	}

	IEnumerator MoveY(){
		UpdateScreenValues();
		while(true){
			if(enabled){
				pos = Mathf.Lerp(pos, Positions[currentPosition],Time.deltaTime * speed);
				trans.position = new Vector3(trans.position.x,(screenHeight * pos) + (trans.sizeDelta.y*0.5f),trans.position.z);	
			}
			yield return null;
		}	
	}
	IEnumerator ShowScenariosButtons(){
		busy = true;
		string[] ScenariosNames = LevelLoader.GetScenarios();
		foreach(string s in ScenariosNames){
			GameObject go = (GameObject)Instantiate(scenarioPrefab);
			yield return null;
			go.name = s;
			yield return null;
			go.transform.SetParent(scenariosHolder.transform);
			yield return null;
			RectTransform rt = go.GetComponent<RectTransform>();
			yield return null;
//			UnityEngine.UI.AspectRatioFitter arf = rt.gameObject.GetComponent<UnityEngine.UI.AspectRatioFitter>();
//			yield return null;
			scenariosHolder.GetComponent<UnityEngine.UI.VerticalLayoutGroup>().spacing = rt.rect.height;
			yield return null;
		}
		busy = false;
	}
	void HideScenariosButtons(){
		busy = true;
		while(scenariosHolder.transform.childCount > 0){
			Transform c = scenariosHolder.transform.GetChild(0);
			c.SetParent(null);
			Destroy(c.gameObject);
		}
		busy = false;
	}
}
