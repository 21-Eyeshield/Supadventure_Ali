namespace Supinfo.Internals.Game
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public sealed class MainMenuManager : MonoBehaviour
	{
		#region Fields
		[SerializeField] private GameObject _root = null;
		#endregion Fields
		
		#region Methods
		public void OnPlayButtonPressed(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
		public void OnQuitButtonPressed()
		{
			Application.Quit();
		}
		public void Hide()
		{
			_root.SetActive(false);
		}
		public void Show()
		{
			_root.SetActive(true);
		}
		#endregion Methods
	}
}