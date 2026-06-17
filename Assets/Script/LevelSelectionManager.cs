using Supinfo.Internals.Game;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelSelectionManager : MonoBehaviour
{
	#region Fields
	public GameObject _root = null;

	public ButtonLevel _levelButtonPrefab = null;

	public GameObject _buttonGroup = null;

	public List<string> _levels = null;
	#endregion Fields

	#region Methods
	private void Start()
	{
		foreach (string level in _levels)
		{
			Instantiate(_levelButtonPrefab, _buttonGroup.transform).SetLevelName(level);
		}
	}

	public void Hide()
	{
		_root.SetActive(false);
	}

	public void Show()
	{
		_root.SetActive(true);
	}

	public void OnDifficultyButtonPressed(int difficulty)
	{
		// Ajouter la sauvegarde de la difficultée dans le jeu.
	}
	#endregion Methods
}