using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SubclassAttribute))]
public class SubclassPropertyDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		///	Create property container element.
		var container = new VisualElement();

		///	Create property field.
		var nodeField = new PropertyField(property.FindPropertyRelative("status"));
		var nodeField2 = new PropertyField(property.FindPropertyRelative("Lol"));

		///	Add field to container.
		container.Add(nodeField);
		container.Add(nodeField2);

		return container;
	}
}
