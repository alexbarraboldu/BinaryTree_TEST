using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class BTContext : MonoBehaviour
{
	[SerializeField] private BehaviourTreeSO _behaviourTreeSO;

	[SerializeField] private BlackboardSO _blackboardSO;

	///	Logic to manage the state of a the behaviour tree


	public void Start()
	{
		RunBehaviourTree();
	}

	public void FixedUpdate()
	{
		//RunBehaviourTree();
	}

	public void RunBehaviourTree()
	{
		_behaviourTreeSO.node.RunNode();
	}

	public void ResetToReadyAllNonRunningStates()
	{
		///	Navegar por todo el arbol y cambiar todos los estado que no sean RUNNING a READY
	}
}
