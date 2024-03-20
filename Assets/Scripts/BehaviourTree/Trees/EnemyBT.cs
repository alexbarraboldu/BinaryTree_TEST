using System;
using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEngine;

[Serializable]
public class EnemyBT
{
	public EnemyBT(GameObject gameObject)
	{
		_node = new Selector(
			new Sequence(
				new Condition(),
				new Attack(gameObject.GetComponent<IAttack>())),
			new Sequence(
				new Condition(),
				new Chase(gameObject.GetComponent<IChase>())),
			new Patrol(gameObject.GetComponent<IPatrol>()));
	}

	private Node _node;

	public void RunBehaviourTree()
	{
		_node.RunNode();
	}
}
