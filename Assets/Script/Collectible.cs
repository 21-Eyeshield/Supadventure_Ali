using UnityEngine;
using UnityEngine.Events;

public sealed class Collectible : MonoBehaviour
{
	#region Fields
	// Référence au gestionnaire de jeu pour mettre à jour le score.
	public GameManager _gameManager = null;

	// Collider du collectible pour désactiver les collisions.
	public Collider2D _collider = null;

	// Sprite du collectible.
	public SpriteRenderer _spriteRenderer = null;

	// Événement déclenché lorsque le collectible est ramassé.
	public UnityEvent _collectedEvent = null;
	#endregion Fields

	#region Methods
	private void OnTriggerEnter2D(Collider2D other)
	{
		// Action quand le joueur touche le collectible.
	}

	private void DestroyCollectible()
	{
		// Destruction du collectible.
	}
	#endregion Methods
}