using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatrol
{
	public BehaviourTree.NodeStatus Patrol();
}
