using System.Collections;
using System.Collections.Generic;

using Unity.Collections;

using UnityEngine;

//public enum MenuName
//{
//	CONTORLS, CREDITS, DEAD, GAME_PAUSE, OPTIONS
//}

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
		menus[loadedMenus.Peek()].SetActive(false);
		loadedMenus.Pop();

		menus[loadedMenus.Peek()].SetActive(true);
	}

	public void CloseAllMenus()
	{
		for (int i = 0; i < loadedMenus.Count; i++)
		{
			menus[loadedMenus.Peek()].SetActive(false);
			loadedMenus.Pop();
		}
	}
	
	public void OnLevelWasLoaded(int level)
	{
		if (level == (int)SceneName.MAIN_MENU)
		{
			Debug.Log("MainMenu Scene");

			menus.Add("Main", _canvas.transform.Find("MainMenu").gameObject);//-

			menus.Add("Controls", _canvas.transform.Find("ControlsMenu").gameObject);//-
			menus.Add("Credits", _canvas.transform.Find("CreditsMenu").gameObject);//-
			menus.Add("Options", _canvas.transform.Find("OptionsMenu").gameObject);//-

			OpenMenu("Main");
		}
		else if (level == (int)SceneName.GAME)
		{
			Debug.Log("Game Scene");

			//CloseMenu("Main");
			menus.Remove("Main");
			menus.Remove("Controls");
			menus.Remove("Credits");
			menus.Remove("Options");

			menus.Add("Dead", _canvas.transform.Find("DeadMenu").gameObject);//*
			menus.Add("GamePause", _canvas.transform.Find("PauseMenu").gameObject);//*
		}
	}


	public void Update()
	{
		PrintAllOpenMenus();
	}

	[ReadOnly, SerializeField] private string allMenus = "";
	private void PrintAllOpenMenus()
	{
		allMenus = "";

		string[] allLoadedMenus = loadedMenus.ToArray();
		for (int i = allLoadedMenus.Length - 1; i > -1; i--)
		{
			allMenus += allLoadedMenus[i] + " -> ";
		}
	}
}
