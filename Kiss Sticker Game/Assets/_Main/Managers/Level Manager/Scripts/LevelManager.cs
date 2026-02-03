using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region References
    private GameManager _gameManager;
    private PlayerMovement _playerMovement;
    private Camera _camera;
    #endregion

    [SerializeField] private int _currentSpawn;
    [SerializeField] private List<SpawnPoint> _spawnPoint = new List<SpawnPoint>();

    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _playerMovement = FindFirstObjectByType<PlayerMovement>();
        _camera = Camera.main;

        OnNextLevel();
    }

    // TEST CODE. It will teleport player to another location as an easy way to prototype multiple levels in the same scene
    public void OnNextLevel()
    {
        if(_currentSpawn  < _spawnPoint.Count)
        {
            _gameManager.ExitResult(); // Hides the Result UI
                        
            var nextPos = _spawnPoint[_currentSpawn].transform.position;

            var cameraZ = _camera.transform.position.z;
            _camera.transform.position = new Vector3(nextPos.x, nextPos.y, cameraZ);
            _playerMovement.transform.position = nextPos;


            _currentSpawn++; // Updates the current pos index
        }
        else return;
    }

    
    private void OnDrawGizmos() // Responsible for drawing the Spawn Points ONLY in the Editor view
    {
        Gizmos.color = Color.green;
        foreach (var p in _spawnPoint)
        {
            Gizmos.DrawSphere(p.transform.position, 0.25f);
        }
    }
}
