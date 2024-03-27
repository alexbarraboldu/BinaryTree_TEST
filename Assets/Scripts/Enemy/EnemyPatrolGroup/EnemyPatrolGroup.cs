using System;
using UnityEngine;
using UnityEngine.Splines;

///	AnimatedSplineGroup
[Serializable, RequireComponent(typeof(SplineAnimate))]
public class EnemyPatrolGroup : MonoBehaviour
{
	private SplineAnimate _splineAnimate;

	private void Awake()
	{
		_splineAnimate = GetComponent<SplineAnimate>();
	}

	public void InstantiateEnemies(int id, Transform parent, Transform destination, PatrolGroupSO patrolGroupSO)
	{
		GameObject parentGO = new GameObject("PatrolGroup_" + id);
		parentGO.transform.SetParent(parent);

		int size = patrolGroupSO.enemies.Count;
		for (int i = 0; i < size; i++)
		{
			string enemyTypePath = "Enemies/";
			switch (patrolGroupSO.enemies[i])
			{
				case EnemyType.NO_WEAPON:
					enemyTypePath += "NoWeaponEnemy";
					break;
				case EnemyType.AXE:
					enemyTypePath += "AxeEnemy";
					break;
				case EnemyType.FORK:
					enemyTypePath += "ForkEnemy";
					break;
				default:
					break;
			}
			GameObject enemy = Resources.Load<GameObject>(enemyTypePath);
			enemy = Instantiate(enemy, parentGO.transform);
			enemy.GetComponent<EnemyController>().SetPatrolDestination(destination);
		}
	}

	public void SetSpline(SplineContainer splineContainer, int duration)
	{
		_splineAnimate.Container = splineContainer;
		_splineAnimate.Duration = duration;
	}
}
