using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
	public BehaviourTree.NodeStatus Attack();
}
