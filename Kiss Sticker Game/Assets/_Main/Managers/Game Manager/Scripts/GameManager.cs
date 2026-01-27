using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Script References
    private PlayerMovement _playerMovement;
    #endregion

    [SerializeField] private InputActionAsset _inputAsset;
    private InputAction _restartAction;

    #region Puzzle Result UI
    [TabGroup("Puzzle Result UI")] [SerializeField] private GameObject _puzzleResultUI;
    [TabGroup("Puzzle Result UI")] [SerializeField] private TextMeshProUGUI _resultText;
    [TabGroup("Puzzle Result UI")] [SerializeField] private List<ParticleSystem> _sparksParticles = new List<ParticleSystem>();
    #endregion

    #region Setup Functions (Awake, Enable, Start...)
    private void OnEnable()
    {
        _restartAction = _inputAsset.FindActionMap("Player").FindAction("Restart Level");

        _restartAction.performed += RestartCurrentLevel;
    }
    private void OnDisable()
    {
        _restartAction.performed -= RestartCurrentLevel;
    }

    private void Start()
    {
        // Assigning references
        _playerMovement = FindAnyObjectByType<PlayerMovement>();

        _puzzleResultUI.SetActive(false);
    }
    #endregion



    #region Game States
    public void OnResult(bool win)
    {
        _puzzleResultUI.SetActive(true);

        _playerMovement.DisableMovement();

        if (win)
        {
            _resultText.text = "Puzzle Solved!";
            foreach(var particle in _sparksParticles)
            {
                particle.Play();
            }
        }
        else
        {
            _resultText.text = "Press [R] to try again! :)";
        }
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
