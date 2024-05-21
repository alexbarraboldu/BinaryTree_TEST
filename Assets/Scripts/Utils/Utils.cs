using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static bool IsNear(this Vector3 a, Vector3 b, float difference)
	{
		bool r1 = (a.x <= b.x + difference) && (a.x >= b.x - difference);
		bool r2 = (a.z <= b.z + difference) && (a.z >= b.z - difference);
		return r1 && r2;
	}

	public static bool IsBetween(this float number, float first, float second, bool inclusive = false)
	{
		return inclusive ? ((number >= first && number <= second) || (number <= first && number >= second)) : ((number > first && number < second) || (number < first && number > second));
	}


	public static bool CompareLayer(this GameObject gameObject, string layerName)
	{
		return gameObject.layer.Equals(LayerMask.NameToLayer(layerName));
	}

	public static bool CompareLayer(this Collider2D collider2D, string layerName)
	{
		return collider2D.gameObject.layer.Equals(LayerMask.NameToLayer(layerName));
	}

	public static bool CompareLayers(this Collider2D collider2D, string layerName1, string layerName2)
	{
		int cF = 1 << collider2D.gameObject.layer;
		int lF = 1 << LayerMask.NameToLayer(layerName1) | 1 << LayerMask.NameToLayer(layerName2);
		return (cF & lF) != 0;
	}

	public static float FixFloatPointError(this float value, int precision = 100)
	{
		float p = precision;
		return ((int)(value * p)) / p;
	}

	public static Vector2 FixFloatPointError(this Vector2 value, int precision = 100)
	{
		float p = precision;
		return new Vector2(((int)(value.x * p)) / p, ((int)(value.y * p)) / p);
	}
}
