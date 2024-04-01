using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject
{
	///	UI
	public class GameActionsUIActionMap : GameActions.IUI_ActionMapActions
	{
		public bool isEnable = false;

		public event UnityAction<InputAction.CallbackContext> PauseEvent = delegate { };

		public void OnPause(InputAction.CallbackContext context)
		{
			PauseEvent.Invoke(context);
		}

	}

	private GameActionsUIActionMap _uiActionMap;
	public GameActionsUIActionMap UiActionMap => _uiActionMap;

	///
	private GameActions _gameActions;

	private void OnEnable()
	{
		if (_gameActions == null)
		{
			_gameActions = new GameActions();

			_uiActionMap = new GameActionsUIActionMap();

			_gameActions.UI_ActionMap.SetCallbacks(_uiActionMap);

			EnableUIInput();
		}
	}

	private void OnDisable()
	{
		DisableAllInput();
	}

	private void OnDestroy()
	{
		_gameActions.UI_ActionMap.RemoveCallbacks(_uiActionMap);
	}

	public void DisableAllInput()
	{
		DisablePlayerInput();
		DisableUIInput();
	}

	public void DisablePlayerInput()
	{
	}


	public void EnableUIInput()
	{
		_uiActionMap.isEnable = true;
		//_gameActions.UI_ActionMap.Enable();
	}
	public void DisableUIInput()
	{
		_uiActionMap.isEnable= false;
		//_gameActions.UI_ActionMap.Disable();
	}
}
