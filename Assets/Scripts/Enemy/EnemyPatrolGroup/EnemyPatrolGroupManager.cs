using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

///	Instantiate the required enemyGroups.
public class EnemyPatrolGroupManager : MonoBehaviour
{
	[SerializeField] private GameObject _enemyGroup;

	[SerializeField] private List<PatrolData> _patrolGroupsData = new ();

	private Transform _animatedSplineGroups;
	private Transform _enemyGroups;

	private void Awake()
	{
		_animatedSplineGroups	= transform.Find("AnimatedSplineGroups");
		_enemyGroups		= transform.Find("EnemyGroups");
	}

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		for (int i = 0; i < _patrolGroupsData.Count; i++)
		{
			_patrolGroupsData[i].splineContainer.gameObject.GetComponent<MeshRenderer>().enabled = false;
			EnemyPatrolGroup enemyPatrolGroup = SpawnAnimatedSplineGroup();
			enemyPatrolGroup.gameObject.name = "EnemyGroup";
			enemyPatrolGroup.gameObject.name += "_" + i;

			enemyPatrolGroup.SetSpline(_patrolGroupsData[i].splineContainer, _patrolGroupsData[i].enemyGroupData.patrolDuration);
			enemyPatrolGroup.InstantiateEnemies(i, _enemyGroups, enemyPatrolGroup.transform, _patrolGroupsData[i].enemyGroupData);
		}
	}

	private EnemyPatrolGroup SpawnAnimatedSplineGroup()
	{
		return Instantiate(_enemyGroup.gameObject, _animatedSplineGroups).GetComponent<EnemyPatrolGroup>();
	}
}

[Serializable]
public class PatrolData
{
	public PatrolGroupSO enemyGroupData;
	public SplineContainer splineContainer;
}
