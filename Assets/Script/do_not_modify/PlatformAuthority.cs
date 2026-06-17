namespace Supinfo.Internals.Game
{
	using UnityEngine;

	/// <summary>
	/// Class for managing platform-specific authority or control within the game.
	/// </summary>
	public sealed class PlatformAuthority : MonoBehaviour
	{
		#region Fields
		private Transform _oldGameObjectTransform = null;
		#endregion Fields

		#region Methods
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				_oldGameObjectTransform = other.gameObject.transform.parent;
				other.gameObject.transform.parent = gameObject.transform;
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			other.gameObject.transform.parent = _oldGameObjectTransform;
			_oldGameObjectTransform = null;
		}
		#endregion Methods
	}
}