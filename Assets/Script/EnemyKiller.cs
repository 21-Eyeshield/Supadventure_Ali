using UnityEngine;
using UnityEngine.Events;

public sealed class EnemyKiller : MonoBehaviour
{
	#region Fields
	// Objet parent à détruire lorsque l'ennemi est tué.
	public GameObject _origin = null;

	// Événement déclenché lors de la destruction de l'ennemi.
	public UnityEvent _deathEvent = null;
	#endregion Fields

	#region Methods
	private void OnCollisionEnter2D(Collision2D other)
	{
		// Action quand le joueur touche l'ennemi.
	}
	#endregion Methods
}