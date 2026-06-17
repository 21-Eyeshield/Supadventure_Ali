namespace Supinfo.Internals.Game
{
	using UnityEngine;
	using UnityEngine.Audio;

	public sealed class OptionManager : MonoBehaviour
	{
		#region Fields
		[SerializeField] private GameObject _optionMenu = null;
		[SerializeField] private AudioMixer _audioMixer = null;
		private bool _isOpened = false;
		#endregion Fields
		
		#region Methods
		public void TogglePause()
		{
			if (_isOpened)
			{
				_optionMenu.SetActive(false);
				_isOpened = false;
			}
			else
			{
				_optionMenu.SetActive(true);
				_isOpened = true;
			}
		}
		public void OnMasterSliderChanged(float value)
		{
			_audioMixer.SetFloat("MasterVolume", value);
		}
		public void OnMusicSliderChanged(float value)
		{
			_audioMixer.SetFloat("MusicVolume", value);
		}
		public void OnSFXSliderChanged(float value)
		{
			_audioMixer.SetFloat("SFXVolume", value);
		}
		public void OnToggleFullScreenChanged(bool value)
		{
			Screen.fullScreen = value;
			Screen.fullScreenMode = value ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.MaximizedWindow;
		}
		#endregion Methods
	}
}