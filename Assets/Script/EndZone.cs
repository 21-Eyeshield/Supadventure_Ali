using TMPro;
using UnityEngine;

public sealed class EndZone : MonoBehaviour
{
	#region Fields
	// Objet qui contient l'interface utilisateur de fin de niveau.
	public GameObject _endZone = null;

	// Texte pour afficher le score final du joueur.
	public TMP_Text _scoreText = null;

	// Référence au gestionnaire de jeu pour récupérer le score.
	public GameManager _gameManager = null;

	// Référence au joueur pour désactiver ses contrôles en fin de niveau.
	public Player _player = null;
	#endregion Fields

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Ajouter la logique de fin de partie.X
	}
}