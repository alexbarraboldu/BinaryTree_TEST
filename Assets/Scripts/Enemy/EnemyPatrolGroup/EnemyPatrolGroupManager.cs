using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Splines;

public class EnemyPatrolGroupManager : MonoBehaviour, IGetNameByEnumType<EnemyType>
{
	[SerializeField] private Transform _enemyGroups;

	[SerializeField] private List<PatrolData> _patrolGroupsData = new ();

	private void Awake()
	{
		if (_enemyGroups == null) Debug.LogError("Enemy Group is NULL.", gameObject);
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

			InstantiateEnemies(i, _enemyGroups, _patrolGroupsData[i]);
		}
	}

	public void InstantiateEnemies(int i, Transform parent, PatrolData patrolData)
	{
		GameObject parentGO = new GameObject("PatrolGroup_" + i);
		parentGO.transform.SetParent(parent);

		int size = patrolData.enemyGroupData.enemies.Count;
		for (int j = 0; j < size; j++)
		{
			string enemyName = GetNameByEnumType(patrolData.enemyGroupData.enemies[j]);
			string enemyTypePath = "Enemies/" + enemyName;

			GameObject enemy = Resources.Load<GameObject>(enemyTypePath);
			enemy = Instantiate(enemy, parentGO.transform);

			SetEnemyPatrolPoints(enemy, patrolData.splineContainer);
			enemy.name = enemyName + "_" + j;
		}
	}

	private void SetEnemyPatrolPoints(GameObject enemy, SplineContainer splineContainer)
	{
		int splineKnotsLength	= splineContainer.Spline.Count;
		Vector3 start			= splineContainer.Spline[0].Position;
		Vector3 end				= splineContainer.Spline[splineKnotsLength - 1].Position;

		enemy.GetComponent<EnemyController>().SetPatrolPoints(start, end);
	}

	public string GetNameByEnumType(EnemyType enemyType)
	{
		string result = "Default";
		switch (enemyType)
		{
			case EnemyType.NO_WEAPON:
				result = "NoWeaponEnemy";
				break;
			case EnemyType.AXE:
				result = "AxeEnemy";
				break;
			case EnemyType.FORK:
				result = "ForkEnemy";
				break;
		}
		return result;
	}
}

[Serializable]
public class PatrolData
{
	public PatrolGroupSO enemyGroupData;
	public SplineContainer splineContainer;
}
