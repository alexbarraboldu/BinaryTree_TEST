using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolGroup", menuName = "Custom/PatrolGroup")]
public class PatrolGroupSO : ScriptableObject
{
	public int groupSize;
	public int patrolDuration;
}
