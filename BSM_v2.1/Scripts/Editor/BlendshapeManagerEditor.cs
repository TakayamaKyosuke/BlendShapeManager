using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[CustomEditor(typeof(BlendshapeManager))]
public class BlendshapeManagerEditor : Editor {

	public override void OnInspectorGUI(){

		BlendshapeManager _bm = target as BlendshapeManager;

		base.OnInspectorGUI ();


		if (GUILayout.Button ("AutoSetting")) {
			_bm.AutoSetting();
		}

	}

}
