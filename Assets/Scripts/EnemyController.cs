using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IAttack, IChase
{

	private void Start()
	{
		IAction action = GetComponent<IAttack>();
		action.Action();
	}

	public void Patrol(out BehaviourTree.NodeStatus nodeStatus)
	{
		nodeStatus = BehaviourTree.NodeStatus.SUCCESS;
	}

	public void Chase(out BehaviourTree.NodeStatus nodeStatus)
	{
		nodeStatus = BehaviourTree.NodeStatus.SUCCESS;
	}

	public void Attack()
	{
		Debug.Log("IAttack");
	}
	public void Chase()
	{
		Debug.Log("IChase");
	}
}
