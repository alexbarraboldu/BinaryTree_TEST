using BehaviourTree;

using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
	NO_WEAPON, AXE, FORK
}

///	Hacer una NavMesh a partir de SplineExtrude, para hacer Patrol().
///	Hacer 2 puntos de patrulla, principio y fin para navegar.
///	Así no tendremos AnimatedSplineGroups (con MoveAlongSplin, etc.).
///	Tener una NavMesh general para poder hacer Chase().

public class EnemyController : MonoBehaviour
{
	private EnemyBT _enemyBT;

	public bool IsAttack;
	public bool IsChase;

	private NavMeshAgent _navMeshAgent;

	///	CHASE
	private Collider _chaseCollider;
	private Transform _chasePoint;

	///	PATROL
	private Vector3 _patrolStartPoint;
	private Vector3 _patrolEndPoint;

	[SerializeField] private string DEBUG_BehaviourTreeNodeStatus = "";

	private void Awake()
	{
		_enemyBT = new EnemyBT(this);
		_navMeshAgent = GetComponent<NavMeshAgent>();

		Debug.LogWarning("Agent " + gameObject.name + " is on NavMesh: " + _navMeshAgent.isOnNavMesh);
	}

	private void Start()
	{
		SetAgentDestination(_patrolEndPoint);
	}

	private void FixedUpdate()
	{
		_enemyBT.RunBehaviourTree(Time.fixedDeltaTime);
	}

	private void SetAgentDestination(Vector3 destination)
	{
		Debug.LogWarning("Agent " + gameObject.name + " destination set correctly: " + _navMeshAgent.SetDestination(destination));
	}

	public void SetPatrolPoints(Vector3 start, Vector3 end)
	{
		_patrolStartPoint	= start;
		_patrolEndPoint		= end;
	}

	#region TASKS
	///	ACTIONS
	///	
	///	Se podría convertir estas funciones en Interfaces dentro de EnemyBT.cs.
	public NodeStatus Attack()
	{
		DEBUG_BehaviourTreeNodeStatus = "Attack";

		return NodeStatus.SUCCESS;
	}

	public NodeStatus Chase()
	{
		DEBUG_BehaviourTreeNodeStatus = "Chase";

		switch (_navMeshAgent.path.status)
		{
			case NavMeshPathStatus.PathComplete:
				return NodeStatus.SUCCESS;
			case NavMeshPathStatus.PathPartial:
				return NodeStatus.FAILURE;
			case NavMeshPathStatus.PathInvalid:
				return NodeStatus.FAILURE;
			default:
				return NodeStatus.RUNNING;
		}
	}

	public NodeStatus Patrol()
	{
		DEBUG_BehaviourTreeNodeStatus = "Patrol";

		///	Comprobar que el enemigo a llegado hasta el destino, y si es así
		///	cambiar el destino al otro destino que haya.

		NodeStatus status = NodeStatus.RUNNING;

		if (_navMeshAgent.remainingDistance <= 1f)
		{
			status = NodeStatus.SUCCESS;
			ChangePatrolPoint();
		}
		///	Hacer que si se para retorne FAILURE (esto de ahora no funciona)
		else if (_navMeshAgent.isStopped)
		{
			status = NodeStatus.FAILURE;
		}

		return status;
	}
	private void ChangePatrolPoint()
	{

	}

	///	CONDITIONS
	public bool CheckAttack() => IsAttack;
	public bool CheckChase() => IsChase;
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
