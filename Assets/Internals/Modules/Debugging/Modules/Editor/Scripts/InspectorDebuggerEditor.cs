using UnityEngine;
using UnityEditor;

using BasicLegionInfected.Debugging.Core;

[CustomEditor(typeof(InspectorDebugger))]
public class InspectorDebuggerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		InspectorDebugger inspectorDebugger = (InspectorDebugger)target;
		if(GUILayout.Button("Do Action"))
		{
			inspectorDebugger.DoAction();
		}
	}
}
