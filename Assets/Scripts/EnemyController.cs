using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private EnemyBT _enemyBT;

	public bool IsAttack;
	public bool IsChase;

	private void Awake()
	{
		_enemyBT = new EnemyBT(this);
	}

	float timer = 0f;
	private void FixedUpdate()
	{
		if (timer >= (Time.timeScale / 10f))
		{
			RunBT();
			timer = 0f;
		}
		else
		{
			timer += Time.fixedDeltaTime;
		}
	}

	private void RunBT()
	{
		_enemyBT.RunBehaviourTree();
	}

	#region TASKS

	#region ACTIONS
	public BehaviourTree.NodeStatus Attack()
	{
		Debug.Log("Attack");
		return BehaviourTree.NodeStatus.SUCCESS;
	}
	public BehaviourTree.NodeStatus Chase()
	{
		Debug.Log("Chase");
		return BehaviourTree.NodeStatus.SUCCESS;
	}

	public BehaviourTree.NodeStatus Patrol()
	{
		Debug.Log("Patrol");
		return BehaviourTree.NodeStatus.RUNNING;
	}
	#endregion

	#region CONDITIONS
	public bool CheckAttack() => IsAttack;
	public bool CheckChase() => IsChase;
	#endregion

	#endregion


	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);

		if (collision.collider.CompareTag("Player"))
		{
			IsAttack = true;
		}
	}
}
