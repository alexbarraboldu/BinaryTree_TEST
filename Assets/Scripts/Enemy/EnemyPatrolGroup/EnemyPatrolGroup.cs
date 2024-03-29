using System;
using UnityEngine;
using UnityEngine.Splines;

///	AnimatedSplineGroup
[Serializable, RequireComponent(typeof(MoveAlongSpline))]
public class EnemyPatrolGroup : MonoBehaviour
{
	private MoveAlongSpline _moveAlongSpline;

	private void Awake()
	{
		_moveAlongSpline = GetComponent<MoveAlongSpline>();
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
		_moveAlongSpline.Container = splineContainer;
		_moveAlongSpline.speed = 7f;
	}
}
