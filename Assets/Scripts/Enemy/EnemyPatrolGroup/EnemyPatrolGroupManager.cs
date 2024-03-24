using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

///	Instantiate the required enemyGroups.
public class EnemyPatrolGroupManager : MonoBehaviour
{
	[SerializeField] private GameObject _enemyGroup;

	private Transform _patrolGroups;
	private Transform _enemyGroups;

	private void Awake()
	{
		_patrolGroups	= transform.Find("PatrolGroups");
		_enemyGroups	= transform.Find("EnemyGroups");
	}

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		foreach (Transform patrolGroupT in _patrolGroups)
		{
			patrolGroupT.GetComponent<MeshRenderer>().enabled = false;
			SplineContainer splineContainer	= patrolGroupT.GetComponent<SplineContainer>();
			PatrolGroup patrolGroup			= patrolGroupT.GetComponent<PatrolGroup>();

			SpawnEnemyGroup().SetGroup(patrolGroup.PatrolGroupSO, splineContainer);
		}
	}

	private EnemyPatrolGroup SpawnEnemyGroup()
	{
		GameObject group = Instantiate(_enemyGroup.gameObject, _enemyGroups);
		return group.GetComponent<EnemyPatrolGroup>();
	}
}

[Serializable]
public struct SEnemyGroup
{
	public int groupSize;
	public int patrolDuration;
}

