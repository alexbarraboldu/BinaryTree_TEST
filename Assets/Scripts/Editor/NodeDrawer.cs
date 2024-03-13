using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEditor;

using UnityEngine;

//[CustomPropertyDrawer(typeof(Node))]
public class NodeDrawer : PropertyDrawer
{
	//public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	//{
	//	EditorGUI.BeginProperty(position, label, property);

	//	position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

	//	var statusRect = new Rect(position.x, position.y, 30, position.height);

	//	EditorGUI.PropertyField(statusRect, property.FindPropertyRelative("status"), GUIContent.none);

	//	EditorGUI.EndProperty();
	//}
}
