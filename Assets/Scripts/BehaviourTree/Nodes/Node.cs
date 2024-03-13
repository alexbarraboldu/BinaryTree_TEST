using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	public enum NodeStatus { FAILURE, SUCCESS, READY, RUNNING };

	[Serializable]
	public abstract class Node
	{
		public NodeStatus status;

		public int Lol;

		public Node()
		{
			status = NodeStatus.READY;
		}

		public abstract NodeStatus RunNode();
	}
}
