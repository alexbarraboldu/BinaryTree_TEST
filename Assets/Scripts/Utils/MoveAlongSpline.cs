using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
	private SplineContainer _spline;
	public SplineContainer Container { get => _spline; set => _spline = value; }

	public float speed = 1f;

	private float _distancePercentage = 0f;
	private float _splineLength;


	void Start()
	{
		_splineLength = _spline.CalculateLength();
	}


	public LoopMode loopMode = LoopMode.PING_PONG;
	private bool _goingForward = true;
	void Update()
	{
		float dP = speed * Time.deltaTime / _splineLength;
		if (_goingForward) _distancePercentage += dP;
		else _distancePercentage -= dP;

		Vector3 currentPosition = _spline.EvaluatePosition(_distancePercentage);
		transform.position = currentPosition;

		if (_distancePercentage > 1f)
		{
			if (loopMode == LoopMode.LOOP) _distancePercentage = 0f;
			else
			{
				_distancePercentage = 1;
				_goingForward = false;
			}
		}
		else if (_distancePercentage < 0f)
		{
			_distancePercentage = 0f;
			_goingForward = true;
		}

		Vector3 nextPosition = _spline.EvaluatePosition(_distancePercentage + 0.05f);
		Vector3 direction = nextPosition - currentPosition;
		transform.rotation = Quaternion.LookRotation(direction, transform.up);
	}

	public enum LoopMode
	{
		LOOP, PING_PONG
	}
}
