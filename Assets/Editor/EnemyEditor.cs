using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(Enemy))]
public class EnemyEditor : Editor {
	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		Enemy e = (Enemy)target;
		EditorGUILayout.BeginVertical();
		e.Name = EditorGUILayout.TextField("Name", "Dummy");
		e.HealthPoints = EditorGUILayout.FloatField("HP",10f);
		EditorGUILayout.EndVertical();

	}
}
