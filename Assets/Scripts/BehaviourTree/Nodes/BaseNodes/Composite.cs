using System;

namespace BehaviourTree
{
	public abstract class Composite : Node
	{
		public Composite(params Node[] nodes) : base()
		{
			this.nodes = nodes;
		}

		public Node[] nodes = Array.Empty<Node>();
	}
}
