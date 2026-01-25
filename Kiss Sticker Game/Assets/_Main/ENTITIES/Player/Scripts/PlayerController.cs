using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;

    #region Movement Variables
    PlayerInput _playerInput;
    InputAction _moveAction;

    [SerializeField] private bool _canMove = true;
    [SerializeField] [Range(1, 50)] private float _moveSpeed;
    #endregion

    [SerializeField] private GameObject _visual;

    #region Setup Functions
    void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");

        // Subscribing FlipVisualRotation function to the _moveAction event
        _moveAction.performed += FlipVisualRotation;
        _moveAction.performed += PlayWalk;

        _moveAction.canceled += PlayIdle;
    }

    private void OnDisable()
    {
        // Unsubscribing FlipVisualRotation function from the _moveAction event
        _moveAction.performed -= FlipVisualRotation;
        _moveAction.performed -= PlayWalk;

        _moveAction.canceled -= PlayIdle;
    }

    void Start()
    {
        _animator = GetComponent<Animator>();

        if(_visual == null)
        {
            Debug.LogError("Visual object not found! Please assign it to the _visual variable in the " + GetType().Name);
        }
    }
    #endregion

    void FixedUpdate()
    {
        if(_canMove)
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
        // Gets the current movement direction
        Vector2 direction = context.ReadValue<Vector2>();

        // If the direction is pointing to the left
        if (direction.x < 0)
        {
            // Turn the _visual Game Object (and all of its children) to the left (rotation Y = -180f)
            _visual.transform.localRotation = Quaternion.Euler(_visual.transform.localRotation.x, -180, 0);
        }
        else // In this case, it works like "if the direction is pointing to the right
        {
            // Turn the _visual Game Object (and all of its children) to the right (rotation Y = 180f)
            _visual.transform.localRotation = Quaternion.Euler(_visual.transform.localRotation.x, 0, 0);
        }
    }

    #region Animation Functions
    private void PlayIdle(InputAction.CallbackContext context)
    {
        _animator.Play("Idle");
    }

    private void PlayWalk(InputAction.CallbackContext context)
    {
        _animator.Play("Walk");
    }
    #endregion
}
