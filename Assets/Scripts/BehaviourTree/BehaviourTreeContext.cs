using BehaviourTree;

using UnityEngine;

public abstract class BehaviourTreeContext
{
	[SerializeField] private BlackboardSO _blackboardSO;

	protected Node node;

	private float timer = 0f;
	private float timerRate = 1f;
	protected float TimerRate
	{
		get => TimerRate;
		set => TimerRate = value;
	}

	public virtual void RunBehaviourTree(float deltaTime)
	{
		if (timer >= timerRate)
		{
			node.RunNode();
			timer = 0f;

		}
		else timer += deltaTime;
	}

	public void ResetToReadyAllNonRunningStates()
	{
		///	Navegar por todo el arbol y cambiar todos los estado que no sean RUNNING a READY
	}
}
