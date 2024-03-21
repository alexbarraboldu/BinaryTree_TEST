using System;

namespace BehaviourTree
{
	public class EnemyBT
	{
		public EnemyBT(EnemyController enemy)
		{
			_node = new Selector(
				new Sequence(
					new Condition(enemy.CheckAttack),
					new Action(enemy.Attack)),
				new Sequence(
					new Condition(enemy.CheckChase),
					new Action(enemy.Chase)),
				new Action(enemy.Patrol));
		}

		private Node _node;

		public void RunBehaviourTree()
		{
			_node.RunNode();
		}
	}
}
