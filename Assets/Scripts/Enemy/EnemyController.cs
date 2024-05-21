using BehaviourTree;

using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
	NO_WEAPON, AXE, FORK
}

///	Tener una NavMesh general para poder hacer Chase().

public class EnemyController : MonoBehaviour
{
	private EnemyBT _enemyBT;

	public bool IsAttack;
	public bool IsChase;

	private NavMeshAgent _navMeshAgent;

	private bool _isCollidingWithObstacle = false;

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
		_navMeshAgent	= GetComponent<NavMeshAgent>();

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

		NodeStatus status = NodeStatus.RUNNING;

		if (_navMeshAgent.remainingDistance <= 1f)
		{
			status = NodeStatus.SUCCESS;
			ChangePatrolPoint();
		}
		
		if (_isCollidingWithObstacle)
		{
			_navMeshAgent.isStopped = true;
			status = NodeStatus.FAILURE;
		}
		else
		{
			_navMeshAgent.isStopped = false;
		}

		return status;
	}
	private void ChangePatrolPoint()
	{
		Vector3 newDestination = transform.position;
		if (transform.position.IsNear(_patrolEndPoint, 2f)) newDestination = _patrolStartPoint;
		else if (transform.position.IsNear(_patrolStartPoint, 2f)) newDestination = _patrolEndPoint;
		_navMeshAgent.SetDestination(newDestination);
	}

	///	CONDITIONS
	public bool CheckAttack() => IsAttack;
	public bool CheckChase() => IsChase;
	#endregion

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			IsAttack = true;
		}
		if (collision.collider.CompareTag("Obstacle"))
		{
			_isCollidingWithObstacle = true;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			IsAttack = false;
		}
		if (collision.collider.CompareTag("Obstacle"))
		{
			_isCollidingWithObstacle = false;
		}
	}
}
