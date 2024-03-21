using BehaviourTree;

using UnityEngine;

public abstract class BehaviourTreeContext
{
	[SerializeField] private BlackboardSO _blackboardSO;

	protected Node _node;

	public virtual void RunBehaviourTree()
	{
		_node.RunNode();
	}

	public void ResetToReadyAllNonRunningStates()
	{
		///	Navegar por todo el arbol y cambiar todos los estado que no sean RUNNING a READY
	}
}
