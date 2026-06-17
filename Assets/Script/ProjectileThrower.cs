using Supinfo.Internals.Game;
using System.Collections;
using UnityEngine;

public sealed class ProjectileThrower : MonoBehaviour
{
	#region Fields
	// Préfabriqué de la boule de feu à lancer.
	public FireBall _projectilePrefab = null;

	// Point d'apparition du projectile.
	public Transform _projectileSpawnPoint = null;

	// Temps minimum entre deux lancers de projectiles.
	public float _minWaitingJump = 1.0f;

	// Temps maximum entre deux lancers de projectiles.
	public float _maxWaitingJump = 5.0f;
	#endregion Fields

	#region Methods
	private void InstantiateProjectile()
	{
		Instantiate(_projectilePrefab, _projectileSpawnPoint.position, Quaternion.identity);
	}

	private IEnumerator WaitingForProjectile()
	{
		yield return new WaitForSeconds(Random.Range(_minWaitingJump, _maxWaitingJump));
		InstantiateProjectile();
		StartCoroutine(WaitingForProjectile());
	}

	private void OnEnable()
	{
		// Check la difficulté du jeu.

		StartCoroutine(WaitingForProjectile());
	}

	private void OnDisable()
	{
		StopCoroutine(WaitingForProjectile());
	}
	#endregion Methods
}