using Supinfo.Internals.Game;
using UnityEngine;

public sealed class CharacterTarget : MonoBehaviour
{
	#region Fields
	// Contrôleur de personnage pour définir la cible à poursuivre.
	public CharacterController2D _characterController = null;
	#endregion Fields

	#region Methods
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Action quand le joueur rentre dans la zone de l'ennemi.
		// Check la difficulté du jeu.
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		// Action quand le joueur sort de la zone de l'ennemi.
	}
	#endregion Methods
}