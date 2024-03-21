using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	public class Action : Task
	{
		Func<NodeStatus> action;

		public Action(Func<NodeStatus> methodAction)
		{
			action = methodAction;
		}

		public override NodeStatus RunNode() => action();
	}
}
