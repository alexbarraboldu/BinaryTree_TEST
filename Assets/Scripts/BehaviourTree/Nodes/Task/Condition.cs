using System;
using System.Collections;
using System.Collections.Generic;


namespace BehaviourTree
{
	[Serializable]
	public class Condition : Task
	{
		public Condition() { }

		public override NodeStatus RunNode()
		{
			throw new NotImplementedException();
		}
	}
}
