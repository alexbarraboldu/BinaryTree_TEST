using System;
using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEngine;

namespace BehaviourTree
{
	[Serializable]
	public class Chase : Action
	{
		private IChase _chase;

		public Chase() { }
		public Chase(IChase chase)
		{
			_chase = chase;
		}

		public override NodeStatus RunNode()
		{
			return status = _chase.Chase();
		}
	}
}
