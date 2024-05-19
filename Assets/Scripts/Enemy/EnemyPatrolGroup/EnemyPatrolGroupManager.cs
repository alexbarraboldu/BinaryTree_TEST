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
		_enemyGroups			= transform.Find("EnemyGroups");
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
			EnemyPatrolGroupController EnemyPatrolGroupController = SpawnAnimatedSplineGroup();
			EnemyPatrolGroupController.gameObject.name = "EnemyGroup";
			EnemyPatrolGroupController.gameObject.name += "_" + i;

			EnemyPatrolGroupController.SetSpline(_patrolGroupsData[i].splineContainer, _patrolGroupsData[i].enemyGroupData.patrolSpeed);
			EnemyPatrolGroupController.InstantiateEnemies(i, _enemyGroups, EnemyPatrolGroupController.transform, _patrolGroupsData[i].enemyGroupData);
		}
	}

	private EnemyPatrolGroupController SpawnAnimatedSplineGroup()
	{
		return Instantiate(_enemyGroup.gameObject, _animatedSplineGroups).GetComponent<EnemyPatrolGroupController>();
	}
}

[Serializable]
public class PatrolData
{
	public PatrolGroupSO enemyGroupData;
	public SplineContainer splineContainer;
}
