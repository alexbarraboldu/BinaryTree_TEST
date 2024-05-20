using TMPro;

using UnityEngine;

public class ShowFPSCanvas : MonoBehaviour
{
	private TMP_Text _text;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}

	private void Start()
	{
		Application.targetFrameRate = -1;
		QualitySettings.vSyncCount = 0;
	}

	private float _timer = 0f;
	private float _timerRate = 1f;
	public void Update()
	{
		float deltaTime = Time.deltaTime;
		if (_timer >= _timerRate)
		{
			_text.text = (1f / deltaTime).ToString();
			_timer = 0f;
		}
		else _timer += deltaTime;
	}
}
