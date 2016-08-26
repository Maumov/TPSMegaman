using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab{
	public Color32 color;
	public GameObject prefab;
}


public class LevelLoader : MonoBehaviour {

	public string levelFileName;

	//public Texture2D levelMap;

	public ColorToPrefab[] colorToPrefab;

	// Use this for initialization
	void Start () {
		LoadMap();
	}

	void EmptyMap(){
		// clear the map
		while(transform.childCount > 0){
			Transform c = transform.GetChild(0);
			c.parent = null;
			Destroy(c.gameObject);
		}
	}

	void LoadMap(){
		EmptyMap();

		//read file from streaming assets
		string filepath = Application.dataPath + "StreamingAssets/" + levelFileName;
		byte[] bytes= System.IO.File.ReadAllBytes(filepath);
		Texture2D levelMap = new Texture2D(2,2);
		levelMap.LoadImage(bytes);

		//get the raw pixels from the level imagemap
		Color32[] allPixels = levelMap.GetPixels32();
		int width = levelMap.width;
		int height = levelMap.height;
		for(int x = 0; x< width; x++){
			for(int y = 0; y< height; y++){
				
				SpawnTileAt(allPixels[(y * width) + x] ,x,y);

			}
		}


	}
	void SpawnTileAt(Color32 c, int x , int y){

		//if this is a transparent pixel, then its meant to just be empty

		if(c.a <= 0){
			return;
		}

		//find the right color 

		//TODO: Should be a dictionary to lookup the prefab
		foreach(ColorToPrefab ctp in colorToPrefab){
			if(ctp.color.Equals(c)){
			//if(ctp.color.r == c.r && ctp.color.g == c.g  && ctp.color.b == c.b && ctp.color.a == c.a ){
				//Spawn the prefab at the right location
				GameObject go  = (GameObject)Instantiate(ctp.prefab, new Vector3(x ,y , 0), Quaternion.identity);
				go.transform.parent = transform.parent;
				return;
			}
		}
		Debug.LogError("No color to prefab found for: " + c.ToString());

	}
}
