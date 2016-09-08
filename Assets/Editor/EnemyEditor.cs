using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(IStats))]
public class EnemyEditor : Editor {

	public override void OnInspectorGUI(){
		Enemy s = (Enemy)target;
		s.Name = EditorGUILayout.TextField("Name",s.Name);
	}
}
