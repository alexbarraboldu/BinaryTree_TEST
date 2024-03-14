using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChase : IAction
{
	public virtual void Action() { Chase(); }
	public void Chase();
}
