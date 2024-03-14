using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack : IAction
{
	public virtual void Action() { Attack(); }
	public void Attack();
}
