using NUnit.Framework.Internal;
using Supinfo.Internals.Game;
using UnityEngine;
using UnityEngine.Events;

public sealed class Player : MonoBehaviour
{
	#region Fields
	// Instance du composant CharacterController2D utilisé pour gérer les mécaniques de mouvement du joueur.
	public CharacterController2D _characterController = null;

	// Référence au composant Animator responsable du contrôle des états d'animation et des transitions pour le personnage du joueur.
	public Animator _animator = null;

	// La vitesse à laquelle le personnage du joueur se déplace.
	public float _moveSpeed = 12f;

	// La limite de position verticale en dessous de laquelle le personnage du joueur est considéré comme étant tombé hors de la zone jouable.
	public float _worldLowLimit = -3f;

	// Événement déclenché lors de la mort du joueur pour exécuter les actions UnityEvent attachées.
	public UnityEvent _deathEvent = null;

	// Instance de la classe GameInputSystem responsable de la gestion des mappings, actions et liaisons des entrées du joueur.
	private GameInputSystem _gameInputSystem = null;

	// Indique si le personnage du joueur est en train d'effectuer un saut.
	private bool _isJumping = false;

	// Valeur booléenne indiquant si le joueur est actuellement accroupi.
	private bool _isCrouching = false;

	// Indicateur booléen indiquant si le joueur est actuellement en train de courir, basé sur les entrées traitées par le jeu.
	private bool _isRunning = false;

	// Représente la direction de mouvement du joueur, définie comme un vecteur 2D basé sur l'entrée des contrôles du joueur.
	private Vector2 _moveDirection = Vector2.zero;
	#endregion Fields

	#region Methods
	private void Update()
    {
		// Ajouter partie gestion des mouvements.
		 // Si l'utilisateur appuie sur la barre espace la méthode retourne `true` et notre condition`if` est validée
																										
		if (_gameInputSystem.Player.Jump.WasPerformedThisFrame())
		{
			_isJumping = true;
            _animator.SetBool("IsJumping", true); // Le personnage est en train de sauter

        }

        _moveDirection = _gameInputSystem.Player.Move.ReadValue<Vector2>();
        _animator.SetFloat("Speed", Mathf.Abs(_moveDirection.x)); // Vitesse de déplacement horizontale

        // Pour savoir si le joueur est en train de s'accroupir, on va utiliser
        _isCrouching = _gameInputSystem.Player.Crouch.IsPressed(); // Touche "C"
        _animator.SetBool("IsCrouching", _isCrouching); // Est-ce que le personnage se baisse

        // Et de la même manière, pour savoir s'il est en train de courir, on utilise
        _isRunning = _gameInputSystem.Player.Sprint.IsPressed(); // Touche "Maj (⇧)"

        if (_characterController.IsGrounded == false) // Si le joueur n'est pas sur le sol
		{
            _animator.SetBool("IsFalling", true);
        }

        // sinon, si l'utilisateur n'a pas pressé la barre espace on ne fait rien
        // Ajouter la limite du monde.
    }

    private void FixedUpdate()
	{
		// Demander la mise à jour de la position du joueur.
		_characterController.Move(_moveDirection.x * _moveSpeed * (_isRunning && _isCrouching == false ? 1.5f : 1.0f) * (_isCrouching && _isCrouching == false ? 0.5f : 1), _isCrouching, _isJumping);

		_isJumping = false;
	}

	public void OnLanding()
	{
		_isJumping = false;
		_animator.SetBool("IsJumping", false);
		_animator.SetBool("IsFalling", false);
	}

	#region Ne pas toucher
	private void Awake()
	{
		_gameInputSystem = new GameInputSystem();
	}

	private void Start()
	{
		_gameInputSystem.Enable();
	}

	private void OnDisable()
	{
		_gameInputSystem.Disable();
	}

	private void OnDestroy()
	{
		_gameInputSystem = null;
	}

	public void DisableInputs()
	{
		_gameInputSystem.Player.Disable();
	}

	public void EnableInputs()
	{
		_gameInputSystem.Player.Enable();
	}
	#endregion Ne pas toucher
	#endregion Methods
}