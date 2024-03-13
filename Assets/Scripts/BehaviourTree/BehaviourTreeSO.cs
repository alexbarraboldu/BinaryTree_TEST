using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	[CreateAssetMenu(fileName = "BehaviourTreeData", menuName = "Custom/BehaviourTree/BehaviourTreeSO")]
	public class BehaviourTreeSO : ScriptableObject
	{
		[SerializeReference, SubclassSelector]
		public Node node;
	}
}
