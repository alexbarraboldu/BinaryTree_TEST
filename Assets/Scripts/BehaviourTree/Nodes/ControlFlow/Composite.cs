using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

namespace BehaviourTree
{
	public abstract class Composite : Node
	{
		public Node[] nodes;

		public Composite(params Node[] nodes) : base()
		{
			this.nodes = nodes;
		}
	}
}
