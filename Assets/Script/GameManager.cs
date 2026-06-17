using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
	// Texte d'affichage du score dans l'interface utilisateur.
	public TMP_Text _scoreText = null;

	// Score actuel du joueur.
	private int _score = 0;

	public void AddScore(int score)
	{
		// Ajouter le score et mettre à jour le texte.
	}

	public int GetScore()
	{
		// Renvoyer le score.
		return int.MaxValue;
	}

	public static void ResetGame()
	{
		// Ajouter la recharge de la scène + le time Scale.
	}
}