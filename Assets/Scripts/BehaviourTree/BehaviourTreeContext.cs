using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

///	Logic to manage the state of a the behaviour tree
public abstract class BehaviourTreeContext
{
	[SerializeField] private BlackboardSO _blackboardSO;

	public void Start()
	{
		//RunBehaviourTree();
	}

	public void FixedUpdate()
	{
		//RunBehaviourTree();
	}

	public void RunBehaviourTree()
	{
		//_behaviourTreeSO.node.RunNode();
	}

	public void ResetToReadyAllNonRunningStates()
	{
		///	Navegar por todo el arbol y cambiar todos los estado que no sean RUNNING a READY
	}
}
