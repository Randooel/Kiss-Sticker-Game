using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    #region Declaring References
    [Title("Script References")]
    private PlayerAnimations _playerAnimations;
    #endregion

    #region Movement Variables
    [SerializeField] private InputActionAsset _inputAsset;
    private InputAction _moveAction;

    [SerializeField] private bool _canMove = true;
    [SerializeField] [Range(0, 50)] private float _moveSpeed;

    public bool CanMove { get => _canMove; set => _canMove = value; }
    #endregion

    #region Setup Functions
    void OnEnable()
    {
        _moveAction = _inputAsset.FindActionMap("Player").FindAction("Move");

        Debug.Log(_moveAction);

        // Subscribing FlipVisualRotation function to the _moveAction event
        _moveAction.performed += FlipVisualRotation;
        _moveAction.performed += PlayWalkAnimation;

        _moveAction.canceled += PlayIdleAnimation;
    }

    private void OnDisable()
    {
        // Unsubscribing FlipVisualRotation function from the _moveAction event
        _moveAction.performed -= FlipVisualRotation;
        _moveAction.performed -= PlayWalkAnimation;

        _moveAction.canceled -= PlayIdleAnimation;
    }

    void Start()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
    }
    #endregion

    void FixedUpdate()
    {
        if(CanMove)
        {
            Move();
        }
    }

    private void Move()
    {
        // Gets the movement direction
        Vector2 direction = _moveAction.ReadValue<Vector2>();

        // Updates the player position based on the direction, moveSpeed and game time
        this.transform.position += new Vector3(direction.x, direction.y, 0) * _moveSpeed * Time.deltaTime;
    }

    // Activated once the _moveAction is performed
    private void FlipVisualRotation(InputAction.CallbackContext context)
    {
        var visual = _playerAnimations.visual;

        // Gets the current movement direction
        Vector2 direction = context.ReadValue<Vector2>();

        // If the direction is pointing to the left
        if (direction.x < 0)
        {
            // Turn the _visual Game Object (and all of its children) to the left (rotation Y = -180f)
            visual.transform.localRotation = Quaternion.Euler(visual.transform.localRotation.x, -180, 0);
        }
        else // In this case, it works like "if the direction is pointing to the right
        {
            // Turn the _visual Game Object (and all of its children) to the right (rotation Y = 180f)
            visual.transform.localRotation = Quaternion.Euler(visual.transform.localRotation.x, 0, 0);
        }
    }

    #region Functions for other entities to reference (like Animator Events)
    private void ToggleMovement(bool value)
    {
        CanMove = value;
    }

    public void EnableMovement()
    {
        ToggleMovement(true);

        Vector2 movementInput = _moveAction.ReadValue<Vector2>();

        // If player is walking
        if (movementInput.magnitude > 0)
        {
            // Play walk animation
            _playerAnimations.PlayWalk();
        }
        else
        {
            // Play idle animation
            _playerAnimations.PlayIdle();
        }
    }
    public void DisableMovement()
    {
        ToggleMovement(false);
    }
    #endregion

    #region Animation Functions
    public void PlayIdleAnimation(InputAction.CallbackContext context)
    {
        if(CanMove)
        {
            _playerAnimations.PlayIdle();
        }
    }

    public void PlayWalkAnimation(InputAction.CallbackContext context)
    {
        if (CanMove)
        {
            _playerAnimations.PlayWalk();
        }
    }
    #endregion
}
