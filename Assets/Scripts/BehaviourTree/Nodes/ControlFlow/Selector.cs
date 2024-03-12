using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	public class Selector : Composite
	{
		public Selector(params Node[] nodes) : base(nodes)
		{

		}

		public override NodeStatus RunNode()
		{
			NodeStatus _status = NodeStatus.FAILURE;

			for (int i = 0; i < nodes.Length; i++)
			{
				_status = nodes[i].RunNode();

				switch (_status)
				{
					case NodeStatus.FAILURE:
						continue;
					case NodeStatus.SUCCESS:
						_status = NodeStatus.SUCCESS;
						break;
					case NodeStatus.RUNNING:
						_status = NodeStatus.RUNNING;
						break;
				}
			}

			status = _status;
			return _status;
		}
	}
}
