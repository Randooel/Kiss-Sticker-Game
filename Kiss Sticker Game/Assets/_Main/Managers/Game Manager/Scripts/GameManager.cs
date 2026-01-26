using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _restartAction;



    #region Setup Functions (Awake, Enable, Start...)
    private void OnEnable()
    {
        _playerInput = FindAnyObjectByType<PlayerInput>();
        _restartAction = _playerInput.actions.FindAction("Restart Level");

        _restartAction.performed += RestartCurrentLevel;
    }
    private void OnDisable()
    {
        _restartAction.performed -= RestartCurrentLevel;
    }
    #endregion



    #region Game States
    public void OnWin()
    {
        Debug.Log("Puzzle Solved!");
    }
    #endregion



    #region Load Scenes
    public void RestartCurrentLevel(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    public void LoadNextScene()
    {
        //SceneManager.LoadScene();
    }
    #endregion
}
