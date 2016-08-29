using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ColorToPrefab{
	public Color32 color;
	public GameObject prefab;
}


public class LevelLoader : MonoBehaviour {
	
	public static string MapSelected;
	public string levels = "Worlds";
	public ColorToPrefab[] colorToPrefab;

	// Use this for initialization
	void Start () {
		LoadDirectory();
	}

	void EmptyMap(){
		// clear the map
		while(transform.childCount > 0){
			Transform c = transform.GetChild(0);
			c.parent = null;
			Destroy(c.gameObject);
		}
	}
	void LoadDirectory(){
		EmptyMap();
		string Directory = MapSelected;
		string[] files = System.IO.Directory.GetFiles(Directory);
//		for(int i = 0; i< files.Length; i++){
//			Debug.Log(files[i]);
//		}
		int i = 0;
		foreach(string s in files){
			if(!s.Contains(".meta")){
				LoadMap(s,i);
				i++;
			}
		}

	}
	void LoadMap(string fileName,int y){
		byte[] bytes= System.IO.File.ReadAllBytes(fileName);

		Texture2D levelMap = new Texture2D(2,2);
		levelMap.LoadImage(bytes);

		//get the raw pixels from the level imagemap
		Color32[] allPixels = levelMap.GetPixels32();
		int width = levelMap.width;
		int height = levelMap.height;
		for(int x = 0; x< width; x++){
			for(int z = 0; z< height; z++){
				SpawnTileAt(allPixels[(z * width) + x] ,x,y,z);
			}
		}
		
	}
	void SpawnTileAt(Color32 c, int x , int y, int z){

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
				GameObject go  = (GameObject)Instantiate(ctp.prefab, new Vector3(x , y , z), Quaternion.identity);
				go.transform.parent = transform;
				return;
			}
		}
		Debug.LogError("No color to prefab found for: " + c.ToString() + " ,Position :"+x+","+y+","+z+".");

	}
	public static string[] GetScenarios(){
		string Directory = Application.dataPath + "/StreamingAssets/";
		string[] paths = System.IO.Directory.GetDirectories(Directory);
		List<string> strings = new List<string>();
		foreach(string s in paths){
			if(!s.Contains(".meta")){
				strings.Add(s);
			}
		}
		return strings.ToArray();
	}
}
