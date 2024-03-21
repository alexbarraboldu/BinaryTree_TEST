using System;
using System.Collections;
using System.Collections.Generic;

using BehaviourTree;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private EnemyBT _enemyBT;

	public bool IsAttack;
	public bool IsChase;

	private void Awake()
	{
		_enemyBT = new EnemyBT(this);
	}

	//private void Start()
	//{
	//	InvokeRepeating("RunBT", 0f, 1f);
	//}

	float timer = 0f;
	private void FixedUpdate()
	{
		if (timer >= Time.timeScale)
		{
			RunBT();
			timer = 0f;
		}
		else
		{
			timer += Time.fixedDeltaTime;
		}
	}

	private void RunBT()
	{
		_enemyBT.RunBehaviourTree();
	}

	///	Action Interfaces
	public BehaviourTree.NodeStatus Attack()
	{
		Debug.Log("IAttack: " + IsAttack);
		return IsAttack ? BehaviourTree.NodeStatus.SUCCESS : BehaviourTree.NodeStatus.FAILURE;
	}
	public BehaviourTree.NodeStatus Chase()
	{
		Debug.Log("IChase: " + IsChase);
		return IsChase ? BehaviourTree.NodeStatus.SUCCESS : BehaviourTree.NodeStatus.FAILURE;
	}

	public BehaviourTree.NodeStatus Patrol()
	{
		Debug.Log("IPatrol");
		return BehaviourTree.NodeStatus.RUNNING;
	}

	public bool CheckAttack() => IsAttack;
	public bool CheckChase() => IsChase;
	///------------------
}
