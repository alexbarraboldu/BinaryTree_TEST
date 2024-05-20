using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class ReselectButton : MonoBehaviour
{
	private EventSystem _eventSystem;
	private InputSystemUIInputModule _inputSystemUIInputModule;

	[SerializeField] private GameObject _currentSelectedGO;

	private void Awake()
	{
		_eventSystem = GetComponent<EventSystem>();
		_inputSystemUIInputModule = GetComponent<InputSystemUIInputModule>();

		_currentSelectedGO = _eventSystem.firstSelectedGameObject;

		_inputSystemUIInputModule.move.action.performed += SetSelectedUIButton;
	}
	private void OnDestroy()
	{
		_inputSystemUIInputModule.move.action.performed -= SetSelectedUIButton;
	}

	private void Update()
	{
		if (_eventSystem.currentSelectedGameObject != null) _currentSelectedGO = _eventSystem.currentSelectedGameObject;
	}

	private void SetSelectedUIButton(InputAction.CallbackContext context)
	{
		if (_eventSystem.currentSelectedGameObject == null && _currentSelectedGO.activeInHierarchy)
		{
			_eventSystem.SetSelectedGameObject(_currentSelectedGO);
		}
		//else if (!_currentSelectedGO.activeInHierarchy)
		//{
		//	Debug.Log("Last current button is disabled");

		//	if (MenuManager.Instance.TryPeekOpenMenu(out GameObject openMenu))
		//	{
		//		Debug.Log("Found an open menu to assign a button");
		//		_currentSelectedGO = openMenu.transform.GetChild(0).gameObject;
		//		_eventSystem.SetSelectedGameObject(_currentSelectedGO);
		//	}
		//}
	}
}
