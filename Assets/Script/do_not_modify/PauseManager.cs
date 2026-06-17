namespace Supinfo.Internals.Game
{
	using UnityEngine;
	using UnityEngine.InputSystem;
	using UnityEngine.SceneManagement;

	public sealed class PauseManager : MonoBehaviour
	{
		#region Fields
		[SerializeField] private GameObject _pauseMenu = null;

		private GameInputSystem _gameInputSystem = null;

		private bool _isPaused = false;

		private bool _isShown = false;
		#endregion Fields

		#region Methods
		private void Awake()
		{
			_gameInputSystem = new GameInputSystem();
			_gameInputSystem.UI.Pause.performed += OnPauseButtonPressed;
		}

		private void Start()
		{
			_gameInputSystem.Enable();
		}

		private void OnDisable()
		{
			_gameInputSystem.Disable();
		}

		private void OnDestroy()
		{
			_gameInputSystem.UI.Pause.performed -= OnPauseButtonPressed;
			_gameInputSystem = null;
		}

		private void OnPauseButtonPressed(InputAction.CallbackContext _)
		{
			if (_isPaused && _isShown == false)
			{
				return;
			}

			TogglePause();
		}

		public void OnMainMenuButtonPressed()
		{
			SceneManager.LoadScene("MainMenu");
		}

		public void TogglePause()
		{
			if (_isPaused)
			{
				_pauseMenu.SetActive(false);
				_isShown = false;
				Time.timeScale = 1;
				_isPaused = false;
			}
			else
			{
				_pauseMenu.SetActive(true);
				_isShown = true;
				Time.timeScale = 0;
				_isPaused = true;
			}
		}

		public void Hide()
		{
			_pauseMenu.SetActive(false);
			_isShown = false;
		}

		public void Show()
		{
			_pauseMenu.SetActive(true);
			_isShown = true;
		}
		#endregion Methods
	}
}