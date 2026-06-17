namespace Supinfo.Internals.Game
{
	using TMPro;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	public sealed class ButtonLevel : MonoBehaviour
	{
		#region Fields
		[SerializeField] private Button _button = null;
		[SerializeField] private TMP_Text _text = null;
		private string _levelName = string.Empty;
		#endregion Fields
		
		#region Methods
		public void SetLevelName(string name)
		{
			_text.text = name;
			_levelName = name;
		}
		
		private void OnButtonPressed()
		{
			SceneManager.LoadScene(_levelName);
		}
		
		private void Start()
		{
			_button.onClick.AddListener(OnButtonPressed);
		}
		
		private void OnDestroy()
		{
			_button.onClick.RemoveListener(OnButtonPressed);
		}
		#endregion Methods
	}
}