using System.Collections;
using System.Collections.Generic;

using Unity.Collections;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : BaseSingleton<MenuManager>
{
	private Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
	private Stack<string> loadedMenus = new Stack<string>();

	private Transform _canvas;

	public override void Awake()
	{
		Destroyable = false;
		base.Awake();

		_canvas = transform.GetChild(0);
		AddAllMenus();

		SceneManager.sceneLoaded += OnLevelLoaded;
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
		for (int i = 0; i < loadedMenus.Count; i++)
		{
			menus[loadedMenus.Peek()].SetActive(false);
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

	private void OnLevelLoaded(Scene scene, LoadSceneMode sceneMode)
	{
		int level = scene.buildIndex;

		if (level == (int)SceneName.MAIN_MENU)
		{
			Debug.Log("MainMenu Scene");

			OpenMenu("Main");
		}
		else if (level == (int)SceneName.GAME)
		{
			Debug.Log("Game Scene");

			///	For testing purposes
			OpenMenu("Pause");
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
