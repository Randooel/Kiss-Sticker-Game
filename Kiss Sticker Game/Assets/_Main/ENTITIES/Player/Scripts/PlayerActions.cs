using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerActions : MonoBehaviour
{
    #region Script References
    private PlayerMovement _playerMovement;
    #endregion

    #region Declaring References
    [Title("Script References")]
    private PlayerAnimations _playerAnimations;
    #endregion


    #region Setting up New Input System variables and events
    private PlayerInput _playerInput;
    private InputAction _kissAction;

    private void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();
        _kissAction = _playerInput.actions.FindAction("Kiss");

        _kissAction.performed += Kiss;
    }
    private void OnDisable()
    {
        _kissAction.performed -= Kiss;
    }
    #endregion

    private void Start()
    {
        // Assigning script references
        _playerAnimations = GetComponent<PlayerAnimations>();
        _playerMovement = GetComponent<PlayerMovement>();
    }



    #region Kiss Action Related
    private void Kiss(InputAction.CallbackContext context)
    {
        _playerAnimations.PlayKiss();

        //Disables movement. It is activated again with an animation event in the Kiss animation
        _playerMovement.DisableMovement();

        CheckForContact();
    }

    private bool CheckForContact()
    {
        RaycastHit hit;

        var visual = _playerAnimations.visual;

        // Cast a raycast
        if (Physics.Raycast(this.transform.position, visual.transform.forward, out hit, 2f))
        {
            // If the collider hit has the IDuplicatable interface
            if(hit.collider.TryGetComponent<IDuplicatable>(out var duplicatable))
            {
                // WIP: Duplicate Logic
                Debug.Log(hit.collider.gameObject.name);
                return true;
            }
            else return false;
        }
        else return false;
    }

    #endregion
}
