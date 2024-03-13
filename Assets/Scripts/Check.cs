using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{

	public bool Checker()
	{
		return 0 != Random.Range(0, 1);
	}
}
