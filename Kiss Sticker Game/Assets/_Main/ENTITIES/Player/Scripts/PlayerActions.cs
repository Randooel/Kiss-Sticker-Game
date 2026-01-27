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



    #region Variables
    [SerializeField][Range(0, 10)] private float _raycastRange = 2;
    [SerializeField][Range(0, 5)] private float _raycastOffset = 1;
    /*
    [SerializeField] private GameObject _raycastVisual;

    private void OnValidate()
    {
        _raycastVisual.transform.position = new Vector3(this.transform.position.x + _raycastRange, 
            this.transform.position.y, this.transform.position.z);
    }
    */
    #endregion



    #region Setting up New Input System variables and events
    [SerializeField] private InputActionAsset _inputAsset;
    private InputAction _kissAction;
    private InputAction _kissSelfAction;

    private void OnEnable()
    {
        _kissAction = _inputAsset.FindActionMap("Player").FindAction("Kiss");
        _kissSelfAction = _inputAsset.FindActionMap("Player").FindAction("Kiss Self");

        _kissAction.performed += Kiss;
        _kissSelfAction.performed += KissSelf;
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
        if(_playerMovement.CanMove)
        {
            _playerAnimations.PlayKiss();

            //Disables movement. It is activated again with an animation event in the Kiss animation
            _playerMovement.DisableMovement();

            // Checks collision and activates the duplicate logic
            CheckForContact();
        }
        else return;
    }

    private void KissSelf(InputAction.CallbackContext context)
    {
        this.GetComponent<PlayerDuplicates>().OnDuplicate();
    }

    private void CheckForContact()
    {
        var visual = _playerAnimations.visual;

        Vector2 direction = visual.transform.right;
        Vector2 origin = (Vector2)this.transform.position + (_raycastOffset * direction);

        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _raycastRange);

        Debug.DrawRay(origin, direction * _raycastRange, Color.red);

        // Cast a raycast
        if (hit.collider != null)
        {
            // If the collider hit has the IDuplicatable interface
            if (hit.collider.TryGetComponent<IDuplicatable>(out var duplicatable))
            {
                // Duplicate Logic
                hit.collider.GetComponent<IDuplicatable>().OnDuplicate();
            }
        }
    }
    #endregion
}
