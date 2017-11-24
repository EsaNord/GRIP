using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private LevelController _lc;
        [SerializeField]
        private GameObject _playerObject;

        private GameObject _player;     
        private Vector2 _playerSpawn;               
        
        public Vector2 LevelSpawn
        {
            get { return _playerSpawn; }
        }
            
        private void Start()
        {
            //_playerObject = GameManager.instance.player;

            if (!GameManager.instance.firstSpawn)
            {
                GameManager.instance.firstSpawn = true;
                InitialSpawn();
            }
            else
            {
                SpawnPlayer();
            }
        }                

        private void InitialSpawn()
        {
            _playerSpawn = new Vector2(0, -5);
            
            _player = Instantiate(_playerObject, _playerSpawn, Quaternion.identity);
            GameManager.instance.playerLives = _player.GetComponent<PlayerCollision>().PlayerLives;
            GameManager.instance.exitPoint = -1;
        }

        private void CheckSpawnLocation()
        {
            // If player has left from the first level
            if (GameManager.instance.exitPoint != -1)
            {
                // Player exited level from entrance point
                if (GameManager.instance.exitPoint == 0)
                {
                    _playerSpawn = GameObject.FindGameObjectWithTag("Exit").transform.position;
                    _playerSpawn.x -= 2f;
                }

                // Player exited level from exit point
                else if (GameManager.instance.exitPoint == 1)
                {
                    _playerSpawn = GameObject.FindGameObjectWithTag("Entrance").transform.position;
                    _playerSpawn.x += 2f;
                }
            }            
        }

        private void SpawnPlayer()
        {
            CheckSpawnLocation();
            Instantiate(_playerObject, _playerSpawn, Quaternion.identity);
        }

        public void Respawn(Vector3 spawnPoint)
        {
            if (GameManager.instance.lastCheckpointName != null)
            {
                Debug.Log("Checkpoint: " + GameManager.instance.lastCheckpointName);
                _player = Instantiate(_playerObject, spawnPoint, Quaternion.identity);
            }
            else
            {
                CheckSpawnLocation();
                Debug.Log("SpawnPoint: " + _playerSpawn);
                _player = Instantiate(_playerObject, _playerSpawn, Quaternion.identity);
            }

            _lc.Player = _player;
        }
    }
}