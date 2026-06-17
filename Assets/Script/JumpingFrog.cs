using Supinfo.Internals.Game;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class JumpingFrog : MonoBehaviour
{
	#region Fields
	// Temps minimum d'attente entre deux sauts.
	public float _minWaitingJump = 1.0f;

	// Temps maximum d'attente entre deux sauts.
	public float _maxWaitingJump = 5.0f;

	// Contrôleur des animations de la grenouille.
	public Animator _animator = null;

	// Contrôleur de mouvement pour gérer les sauts.
	public CharacterController2D _characterController = null;
	#endregion Fields

	#region Methods
	private void OnJumpTimerEnd()
	{
		// Ajouter la logique de saut.
	}

	public void OnLanded()
	{
		// Ajouter le changement d'animation.
	}

	private IEnumerator WaitingForJump()
	{
		// Ajouter la logique d'attente de saut.
		yield return new WaitForSeconds(0);
		StartCoroutine(WaitingForJump());
	}

	private void OnEnable()
	{
		// Check la difficulté du jeu.
		StartCoroutine(WaitingForJump());
	}

	private void OnDisable()
	{
		StopCoroutine(WaitingForJump());
	}
	#endregion Methods
}