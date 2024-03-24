using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[Serializable, RequireComponent(typeof(SplineAnimate))]
public class EnemyPatrolGroup : MonoBehaviour
{
	private SplineAnimate _splineAnimate;

	private void Awake()
	{
		_splineAnimate = GetComponent<SplineAnimate>();
	}

	public void SetGroup(PatrolGroupSO patrolGroupSO, SplineContainer splineContainer)
	{
		///	Instantiate enemies
		InstantiateEnemies(patrolGroupSO);
		///	Set Spline
		SetSpline(splineContainer, patrolGroupSO.patrolDuration);

		///	Update() => Check if some enemy has exited the SplineAnimated Circle, if so it should be stopped
	}

	private void InstantiateEnemies(PatrolGroupSO patrolGroupSO)
	{
		for (int i = 0; i < patrolGroupSO.groupSize; i++)
		{
			//Instantiate();
		}
	}

	private void SetSpline(SplineContainer splineContainer, int duration)
	{
		_splineAnimate.Container = splineContainer;
		_splineAnimate.Duration = duration;
	}
}
