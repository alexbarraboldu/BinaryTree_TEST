using System.Collections;
using System.Collections.Generic;

using Unity.Collections;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : BaseSingleton<MenuManager>
{
	private Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
	private Stack<string> loadedMenus = new Stack<string>();

	private Transform _canvas;

	[SerializeField] private InputReader _inputReader;

	public override void Awake()
	{
		Destroyable = false;
		base.Awake();

		_canvas = transform.GetChild(0);
		AddAllMenus();

		SceneManager.sceneLoaded				+= OnLevelLoaded;
		_inputReader.UserActionMap.PauseEvent	+= OnPauseEvent;
		_inputReader.UIActionMap.CancelEvent	+= OnCancelEvent;
	}
	private void OnDestroy()
	{
		SceneManager.sceneLoaded				-= OnLevelLoaded;
		_inputReader.UserActionMap.PauseEvent	-= OnPauseEvent;
		_inputReader.UIActionMap.CancelEvent	-= OnCancelEvent;
	}

	public bool TryPeekOpenMenu(out GameObject menuGameObject)
	{
		if (loadedMenus.TryPeek(out string menuName))
		{
			menuGameObject = menus[menuName];
			return true;
		}
		else
		{
			menuGameObject = null;
			return false;
		}
	}

	public void OpenMenu(string menuName)
	{
		if (!menus[menuName].activeSelf)
		{
			if (loadedMenus.Count != 0) menus[loadedMenus.Peek()].SetActive(false);

			menus[menuName].SetActive(true);
			loadedMenus.Push(menuName);
		}
		else
		{
			Debug.LogAssertion("Cannot open this menu.");
		}
	}

	public void GoToLastMenu()
	{
		if (loadedMenus.TryPeek(out string menuName))
		{
			menus[menuName].SetActive(false);
			loadedMenus.Pop();

			if (loadedMenus.TryPeek(out string lastMenuName))
			{
				menus[lastMenuName].SetActive(true);
			}
			else Debug.LogAssertion("No last menu to open. End reached.");
		}
		else Debug.LogAssertion("No curent menu opened. Cannot close anything.");
	}

	public void CloseAllMenus()
	{
		while (loadedMenus.TryPeek(out string menuPeeked))
		{
			menus[menuPeeked].SetActive(false);
			loadedMenus.Pop();
		}
	}
	

	private void AddAllMenus()
	{
		menus.Add("Main", _canvas.transform.Find("MainMenu").gameObject);//-

		menus.Add("Dead", _canvas.transform.Find("DeadMenu").gameObject);//*
		menus.Add("Pause", _canvas.transform.Find("PauseMenu").gameObject);//*

		menus.Add("Controls", _canvas.transform.Find("ControlsMenu").gameObject);//
		menus.Add("Credits", _canvas.transform.Find("CreditsMenu").gameObject);//
		menus.Add("Options", _canvas.transform.Find("OptionsMenu").gameObject);//
	}

	private SceneName _currentScene;
	private void OnLevelLoaded(Scene scene, LoadSceneMode sceneMode)
	{
		_currentScene = (SceneName)scene.buildIndex;

		if (_currentScene == SceneName.MAIN_MENU)
		{
			Debug.Log("MainMenu Scene");

			CloseAllMenus();

			OpenMenu("Main");

			_inputReader.SetUIInput(true);
		}
		else if (_currentScene == SceneName.GAME)
		{
			Debug.Log("Game Scene");

			CloseAllMenus();

			_inputReader.SetUIInput(false);
			_inputReader.SetUserInput(true);
		}
	}

	private void OnPauseEvent(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (loadedMenus.Contains("Pause"))
			{
				CloseAllMenus();
				_inputReader.SetUIInput(false);
			}
			else
			{
				OpenMenu("Pause");
				_inputReader.SetUIInput(true);
			}
		}
	}
	private void OnCancelEvent(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (loadedMenus.Contains("Pause") || (loadedMenus.Contains("Main") && loadedMenus.Count > 1))
			{
				GoToLastMenu();
			}
		}
	}

	#region PRINT MENUS
	public void Update()
	{
		PrintAllOpenMenus();
	}

	[SerializeField] private string allMenus = "";
	private void PrintAllOpenMenus()
	{
		allMenus = "";

		string[] allLoadedMenus = loadedMenus.ToArray();
		for (int i = allLoadedMenus.Length - 1; i > -1; i--)
		{
			allMenus += allLoadedMenus[i] + " -> ";
		}
	}
	#endregion
}
