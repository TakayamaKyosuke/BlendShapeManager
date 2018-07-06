using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Blendshape))]
public class BlendshapeEditor : Editor {

	public override void OnInspectorGUI ()
	{
		Blendshape _bs = target as Blendshape;
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Set")) {
			_bs.GetBlendshape ();
		}
	}



}
