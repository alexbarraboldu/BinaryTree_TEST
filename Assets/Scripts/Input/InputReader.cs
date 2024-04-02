using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Custom/Input Reader")]
public class InputReader : ScriptableObject
{
	///	User
	public class ActionsUser : GameInputActions.IUserActions
	{
		public bool isEnable;

		public event UnityAction<InputAction.CallbackContext> PauseEvent = delegate { };

		public void OnPause(InputAction.CallbackContext context)
		{
			PauseEvent.Invoke(context);
		}

	}

	private ActionsUser _userActionMap;
	public ActionsUser UserActionMap => _userActionMap;

	/// UI
	public class ActionUI : GameInputActions.IUIActions
	{
		public bool isEnable;

		public event UnityAction<InputAction.CallbackContext> CancelEvent = delegate { };

		public void OnCancel(InputAction.CallbackContext context)
		{
			CancelEvent.Invoke(context);
		}
		public void OnClick(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnMiddleClick(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnNavigate(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnPoint(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnRightClick(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnScrollWheel(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnSubmit(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
		public void OnTrackedDevicePosition(InputAction.CallbackContext context)
		{
			//throw new System.NotImplementedException();
		}
	}

	private ActionUI _uiActionMap;
	public ActionUI UIActionMap => _uiActionMap;

	///
	private GameInputActions _gameInputActions;

	private void OnEnable()
	{
		if (_gameInputActions == null)
		{
			_gameInputActions = new GameInputActions();

			_userActionMap	= new ActionsUser();
			_uiActionMap	= new ActionUI();

			_gameInputActions.User.SetCallbacks(_userActionMap);
			_gameInputActions.UI.SetCallbacks(_uiActionMap);
		}
	}

	private void OnDisable()
	{
		DisableAllInput();
	}

	private void OnDestroy()
	{
		_gameInputActions.User.RemoveCallbacks(_userActionMap);
		_gameInputActions.UI.RemoveCallbacks(_uiActionMap);
	}

	public void DisableAllInput()
	{
		SetUserInput(false);
		SetUIInput(false);
	}

	public void SetUserInput(bool value)
	{
		_userActionMap.isEnable = value;
		if (value) _gameInputActions.User.Enable();
		else _gameInputActions.User.Disable();
	}
	public void SetUIInput(bool value)
	{
		_userActionMap.isEnable = value;
		if (value) _gameInputActions.UI.Enable();
		else _gameInputActions.UI.Enable();
	}
}
