using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	[Serializable]
	public abstract class Task : Node
	{
		public Task() { }
		///	You should be able to subscribe to a function to execute.
	}
}
