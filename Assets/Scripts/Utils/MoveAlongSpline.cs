using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
	public enum LoopMode
	{
		LOOP, PING_PONG
	}

	private SplineContainer _splineContainer;

	[SerializeField] private LoopMode loopMode = LoopMode.PING_PONG;

	private float _speed = 1f;

	private float _distancePercentage = 0f;
	private float _splineLength;

	private bool _goingForward = true;


	void Start()
	{
		_splineLength = _splineContainer.CalculateLength();
	}

	void Update()
	{
		float dP = _speed * Time.deltaTime / _splineLength;
		if (_goingForward) _distancePercentage += dP;
		else _distancePercentage -= dP;

		Vector3 currentPosition = _splineContainer.EvaluatePosition(0, _distancePercentage);
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

		Vector3 nextPosition = _splineContainer.EvaluatePosition(0, _distancePercentage + 0.05f);
		Vector3 direction = nextPosition - currentPosition;
		transform.rotation = Quaternion.LookRotation(direction, transform.up);
	}

	public void SetSpline(SplineContainer splineContainer, float speed)
	{
		_splineContainer = splineContainer;
		_speed = speed;
	}
}
