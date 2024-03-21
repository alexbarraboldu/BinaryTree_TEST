namespace BehaviourTree
{
	public class Sequence : Composite
	{
		public Sequence(params Node[] nodes) : base(nodes)
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
						_status = NodeStatus.FAILURE;
						goto exit_loop;
					case NodeStatus.SUCCESS:
						_status = NodeStatus.SUCCESS;
						continue;
					case NodeStatus.RUNNING:
						_status = NodeStatus.RUNNING;
						break;
				}
			}

			exit_loop:;

			status = _status;
			return _status;
		}
	}
}
