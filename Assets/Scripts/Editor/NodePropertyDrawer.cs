using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class NodePropertyDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		base.OnGUI(position, property, label);
	}
}
