namespace Supinfo.Internals.Game
{
	using UnityEngine;

	public sealed class FireBall : MonoBehaviour
	{
		#region Fields
		[SerializeField] private float _speed = 10f;

		[SerializeField] private float _maxLifetime = 5f;

		private int _direction = 1;

		private float _lifetime = 0.0f;
		#endregion Fields

		#region Properties
		public int Direction
		{
			set
			{
				if (value >= 1)
				{
					_direction = 1;
				}
				else
				{
					_direction = -1;
				}
			}
		}
		#endregion Properties

		#region Methods
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				GameManager.ResetGame();
			}
			else if (other.gameObject.CompareTag("Ground"))
			{
				Destroy(gameObject);
			}
		}

		private void Update()
		{
			gameObject.transform.Translate(Vector3.right * _speed * _direction * Time.deltaTime);
			_lifetime += Time.deltaTime;

			if (_lifetime > _maxLifetime)
			{
				Destroy(gameObject);
			}
		}
		#endregion Methods
	}
}