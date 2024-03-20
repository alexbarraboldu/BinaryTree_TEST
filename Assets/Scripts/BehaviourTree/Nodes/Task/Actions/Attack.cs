using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
	[Serializable]
	public class Attack : Action
	{
		private IAttack _attack;

		public Attack() { }
		public Attack(IAttack attack)
		{
			_attack = attack;
		}

		public override NodeStatus RunNode()
		{
			return status = _attack.Attack();
		}
	}
}
