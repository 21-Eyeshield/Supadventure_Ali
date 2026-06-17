using Supinfo.Internals.Game;
using UnityEngine;

public sealed class Opossum : MonoBehaviour
{
	#region Fields
	// Contrôleur de personnage pour gérer les mouvements.
	public CharacterController2D _characterController = null;

	// Limite droite du déplacement.
	public Transform _rightLimit = null;

	// Limite gauche du déplacement.
	public Transform _leftLimit = null;

	// Vitesse de déplacement en unités par seconde.
	public float _moveSpeed = 12.0f;

	// Direction actuelle du déplacement (1 = droite, -1 = gauche).
	private int _direction = 1;
	#endregion Fields

	#region Methods
	private void FixedUpdate()
	{
		// Ajouter la logique de mouvement.
	}
	#endregion Methods
}