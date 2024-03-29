using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroupData", menuName = "Custom/EnemyData/EnemyGroup")]
public class EnemyGroupSO : ScriptableObject
{
	public List<EnemyType> enemies = new List<EnemyType>();
}
