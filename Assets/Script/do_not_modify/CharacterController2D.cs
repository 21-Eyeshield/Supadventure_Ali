namespace Supinfo.Internals.Game
{
	using UnityEngine;
	using UnityEngine.Events;

	/// <summary>
	/// A 2D character controller component for handling player movement, jumping, and crouching mechanics.
	/// Provides features like air control, movement smoothing, and ground detection.
	/// </summary>
	public sealed class CharacterController2D : MonoBehaviour
	{
		#region Fields
		#region Const
		/// <summary>
		/// Radius of the overlap circle to determine if grounded
		/// </summary>
		const float GROUND_RADIUS = .2f;

		/// <summary>
		/// Radius of the overlap circle to determine if the player can stand up.
		/// </summary>
		const float CEILING_RADIUS = .2f;
		#endregion Const

		/// <summary>
		/// Amount of force added when the player jumps.
		/// </summary>
		[SerializeField] private float _jumpForce = 400f;

		/// <summary>
		/// The amount of maxSpeed applied to crouching movement.
		/// <example>1 = 100%</example>
		/// </summary>
		[SerializeField, Range(0, 1)] private float _crouchSpeed = .36f;

		/// <summary>
		/// How much to smooth out the movement is.
		/// </summary>
		[SerializeField, Range(0, .3f)] private float _movementSmoothing = .05f;

		/// <summary>
		/// Whether a player can steer while jumping.
		/// </summary>
		[SerializeField] private bool _airControl = false;

		/// <summary>
		/// A mask determining what is ground to the character.
		/// </summary>
		[SerializeField] private LayerMask _whatIsGround;

		/// <summary>
		/// A position marking where to check if the player is grounded.
		/// </summary>
		[SerializeField] private Transform _groundCheck;

		/// <summary>
		/// A position marking where to check for ceilings.
		/// </summary>
		[SerializeField] private Transform _ceilingCheck;

		/// <summary>
		/// A collider that will be disabled when crouching.
		/// </summary>
		[SerializeField] private Collider2D _crouchDisableCollider;

		/// <summary>
		/// Event triggered when the character lands on the ground after being in the air.
		/// This can be used to handle actions or effects that should occur upon landing.
		/// </summary>
		[SerializeField, Header("Events"), Space] private UnityEvent _characterLandEvent;

		/// <summary>
		/// Event triggered when the character starts or stops crouching.
		/// The boolean parameter indicates whether the character is currently crouching (true) or not (false).
		/// This can be used to handle actions or effects related to the crouching state.
		/// </summary>
		[SerializeField] private UnityEvent<bool> _characterCrouchEvent;

		/// <summary>
		/// Whether the player is grounded.
		/// </summary>
		private bool _isOnGround;

		/// <summary>
		/// Character rigidbody.
		/// </summary>
		private Rigidbody2D _rigidbody2D;

		/// <summary>
		/// Indicates whether the character is currently facing right.
		/// Used to determine the player's orientation and to flip the character sprite or transform when needed.
		/// </summary>
		private bool _facingRight = true;

		/// <summary>
		/// Temporary storage for the current velocity of the character,
		/// used during movement calculations for smoothing transitions.
		/// </summary>
		private Vector3 _velocity = Vector3.zero;

		/// <summary>
		/// Tracks whether the character was previously crouching during the last movement update.
		/// Used to determine when to trigger crouch-related events or actions.
		/// </summary>
		private bool _wasCrouching = false;

		private Transform _target = null;
		#endregion Fields

		#region Properties
		/// <summary>
		/// <inheritdoc cref="_isOnGround"/>
		/// </summary>
		public bool IsGrounded
		{
			get { return _isOnGround; }
		}
		#endregion Properties

		#region Methods
		#region Lifetime
		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();

			if (_characterLandEvent == null)
			{
				_characterLandEvent = new UnityEvent();
			}

			if (_characterCrouchEvent == null)
			{
				_characterCrouchEvent = new UnityEvent<bool>();
			}
		}

		private void FixedUpdate()
		{
			bool wasGrounded = _isOnGround;
			_isOnGround = false;

			Collider2D colliders = Physics2D.OverlapCircle(_groundCheck.position, GROUND_RADIUS, _whatIsGround);


			if (colliders != null
			    && colliders.gameObject != gameObject)
			{
				_isOnGround = true;

				if (!wasGrounded)
				{
					_characterLandEvent.Invoke();
				}
			}
		}
		#endregion Lifetime

		#region Public
		/// <summary>
		/// Handles character movement, crouching, and jumping in a 2D environment.
		/// </summary>
		/// <param name="move">A float value representing the player's movement direction
		/// and speed.</param>
		/// <param name="crouch">A boolean value indicating whether the character
		/// is crouching.</param>
		/// <param name="jump">A boolean value indicating whether the character
		/// is attempting to jump.</param>
		public void Move(float move, bool crouch, bool jump)
		{
			if (crouch == false)
			{
				if (Physics2D.OverlapCircle(_ceilingCheck.position, CEILING_RADIUS, _whatIsGround))
				{
					crouch = true;
				}
			}

			if (_isOnGround || _airControl)
			{
				if (crouch)
				{
					if (_wasCrouching == false)
					{
						_wasCrouching = true;
						_characterCrouchEvent.Invoke(true);
					}

					move *= _crouchSpeed;

					if (_crouchDisableCollider != null)
					{
						_crouchDisableCollider.enabled = false;
					}
				}
				else
				{
					if (_crouchDisableCollider != null)
					{
						_crouchDisableCollider.enabled = true;
					}

					if (_wasCrouching)
					{
						_wasCrouching = false;
						_characterCrouchEvent.Invoke(false);
					}
				}

				if (_target is not null)
				{
					float distance = transform.position.x - _target.position.x;

					if (Mathf.Abs(distance) > 0.2)
					{
						move = distance > 0 ? move : move * -1;
					}
					else
					{
						move = 0;
					}
				}

				Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.linearVelocity.y);
				_rigidbody2D.linearVelocity = Vector3.SmoothDamp(_rigidbody2D.linearVelocity, targetVelocity, ref _velocity, _movementSmoothing);

				if (move > 0
				    && _facingRight == false)
				{
					Flip();
				}
				else if (move < 0 && _facingRight)
				{
					Flip();
				}
			}

			if (_isOnGround && jump)
			{
				_rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
			}
		}

		public void SetTarget(Transform target)
		{
			_target = target;
		}
		#endregion Public

		private void Flip()
		{
			_facingRight = !_facingRight;

			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		#endregion Methods
	}
}