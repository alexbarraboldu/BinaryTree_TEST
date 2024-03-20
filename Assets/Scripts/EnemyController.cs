using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyController : MonoBehaviour, IAttack, IChase, IPatrol
{
	private EnemyBT _enemyBT;

	private void Awake()
	{
		_enemyBT = new EnemyBT(this.gameObject);
	}

	private void Start()
	{
		InvokeRepeating("RunBT", 0f, 1f);
	}

	private void RunBT()
	{
		_enemyBT.RunBehaviourTree();
	}

	///	Action Interfaces
	public BehaviourTree.NodeStatus Attack()
	{
		Debug.Log("IAttack");
		return BehaviourTree.NodeStatus.SUCCESS;
	}
	public BehaviourTree.NodeStatus Chase()
	{
		Debug.Log("IChase");
		return BehaviourTree.NodeStatus.SUCCESS;
	}

	public BehaviourTree.NodeStatus Patrol()
	{
		Debug.Log("IPatrol");
		return BehaviourTree.NodeStatus.SUCCESS;
	}
	///------------------
}
