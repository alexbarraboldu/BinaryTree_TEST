using UnityEngine;
using UnityEngine.EventSystems;

public class SetFirstSelectedButton : MonoBehaviour
{
	[SerializeField] private GameObject _firstSelectedGameObject;
	private EventSystem _eventSystem;

	private void Awake()
	{
		_eventSystem = EventSystem.current;
	}

	private void OnEnable()
	{
		_eventSystem.SetSelectedGameObject(_firstSelectedGameObject);
	}
}
