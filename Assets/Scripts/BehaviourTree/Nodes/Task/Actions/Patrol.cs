using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	[Serializable]
	public class Patrol : Action
	{
		private IPatrol _patrol;

		public Patrol() { }
		public Patrol(IPatrol patrol)
		{
			_patrol = patrol;
		}

		public override NodeStatus RunNode()
		{
			return status = _patrol.Patrol();
		}
	}
}
