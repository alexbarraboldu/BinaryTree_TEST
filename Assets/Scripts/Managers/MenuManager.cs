using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuName
{
	CONTORLS, CREDITS, DEAD, GAME_PAUSE, OPTIONS
}

public class MenuManager : MonoBehaviour
{
	[SerializeField] private Dictionary<MenuName, GameObject> menus = new Dictionary<MenuName, GameObject>();

	private Stack<MenuName> menuNames = new Stack<MenuName>();

	public void Awake()
	{
		menus.Add(MenuName.CONTORLS,	Resources.Load<GameObject>("Menus/ControlsMenu"));
		menus.Add(MenuName.CREDITS,		Resources.Load<GameObject>("Menus/CreditsMenu"));
		menus.Add(MenuName.DEAD,		Resources.Load<GameObject>("Menus/DeadMenu"));
		menus.Add(MenuName.GAME_PAUSE,	Resources.Load<GameObject>("Menus/PauseMenu"));
		menus.Add(MenuName.OPTIONS,		Resources.Load<GameObject>("Menus/OptionsMenu"));
	}

	public void OpenMenu(MenuName menuName)
	{
		if (!menus[menuName].activeSelf)
		{
			menus[menuName].SetActive(true);
			menuNames.Push(menuName);
		}
		else Debug.LogAssertion("Cannot open this menu.");
	}

	public void CloseMenu()
	{
		if (menus[menuNames.Peek()].activeSelf)
		{
			menus[menuNames.Peek()].SetActive(false);
			menuNames.Pop();
		}
		else Debug.LogAssertion("Cannot close this menu. Is it really active?");
	}
}
