using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	public enum NodeStatus { FAILURE, SUCCESS, READY, RUNNING };

	public abstract class Node
	{
		public NodeStatus status;

		public Node()
		{
			status = NodeStatus.READY;
		}

		public abstract NodeStatus RunNode();
	}
}
