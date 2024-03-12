using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(Node))]
public class SubclassPropertyDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		///	Create property container element.
		var container = new VisualElement();

		///	Create property field.
		var nodeField = new PropertyField(property.FindPropertyRelative("status"));

		///	Add field to container.
		container.Add(nodeField);

		return container;
	}
}
