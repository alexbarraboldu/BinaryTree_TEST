using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public void Attack(/*BehaviourTree.NodeStatus nodeStatus*/)
	{
		Debug.Log("Attack");
		//nodeStatus = BehaviourTree.NodeStatus.SUCCESS;
	}

	public void Patrol(out BehaviourTree.NodeStatus nodeStatus)
	{
		nodeStatus = BehaviourTree.NodeStatus.SUCCESS;
	}

	public void Chase(out BehaviourTree.NodeStatus nodeStatus)
	{
		nodeStatus = BehaviourTree.NodeStatus.SUCCESS;
	}
}
