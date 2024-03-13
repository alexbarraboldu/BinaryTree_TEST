using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubclassAttribute : PropertyAttribute
{
	Type type;
	public Type Type => type;

	public SubclassAttribute(Type type)
	{
		this.type = type;
	}
}
