using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
	public class Condition : Task
	{
		Func<bool> condition;

		public Condition(Func<bool> methodCondition)
		{
			condition = methodCondition;
		}

		public override NodeStatus RunNode() => condition() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
	}
}
