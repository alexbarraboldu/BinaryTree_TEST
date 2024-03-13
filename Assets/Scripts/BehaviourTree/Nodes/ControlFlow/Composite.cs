using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using System;

namespace BehaviourTree
{
	[Serializable]
	public abstract class Composite : Node
	{
		[SerializeReference, SubclassSelector]
		public Node[] nodes = Array.Empty<Node>();

		public Composite() { }
		public Composite(params Node[] nodes) : base()
		{
			this.nodes = nodes;
		}
	}
}
