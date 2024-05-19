using System;


namespace BehaviourTree
{
	public class Condition : Task
	{
		public Condition(Func<bool> methodCondition)
		{
			condition = methodCondition;
		}

		Func<bool> condition;

		public override NodeStatus RunNode() => condition() ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
	}
}
