using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
	LOAD, MAIN_MENU, GAME
}

public class SceneController : BaseSingleton<SceneController>
{
	[SerializeField] private GameObject loaderCanvas;
	[SerializeField] private UnityEngine.UI.Image progressBar;

	private float target;

	[SerializeField]
	[Tooltip("Delay (in milliseconds) after the scene is loaded to set it active")]
	[Range(0, 500)] private int FinalisedDelay = 100;

	public override void Awake()
	{
		Destroyable = false;
		base.Awake();
	}

	private void Start()
	{
		LoadSceneAsync("MainMenuScene");
	}

	void Update()
	{
		if (progressBar != null) progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 2 * Time.deltaTime);
	}

	public async void LoadSceneAsync(string sceneName)
	{
		target = 0;
		if (progressBar != null) progressBar.fillAmount = 0;

		var sceneToLoad = SceneManager.LoadSceneAsync(sceneName);
		sceneToLoad.allowSceneActivation = false;

		if (loaderCanvas != null) loaderCanvas.SetActive(true);

		do
		{
			await Task.Delay(100);

			target = 0.9f / sceneToLoad.progress;

		} while (sceneToLoad.progress < 0.9f);

		await Task.Delay(FinalisedDelay);

		sceneToLoad.allowSceneActivation = true;
		if (loaderCanvas != null) loaderCanvas.SetActive(false);
	}
}
