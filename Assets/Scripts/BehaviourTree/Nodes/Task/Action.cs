using System;
using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEngine;
using UnityEngine.Events;

namespace BehaviourTree
{
	[Serializable]
	public class Action : Task
	{
		public Action() { }

		public UnityEvent<bool> myEvent;

		public override NodeStatus RunNode()
		{
			throw new NotImplementedException();
		}
	}
}
