using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerSpawner : MonoBehaviour
    {

        private GameObject _player;        
        private Vector2 _playerSpawn;
        
        public Vector2 LevelSpawn
        {
            get { return _playerSpawn; }
        }
            
        private void Start()
        {
            _player = GameManager.instance.player;

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
            _playerSpawn = new Vector2(2, 3);
            
            Instantiate(_player, _playerSpawn, Quaternion.identity);            
        }

        private void SpawnPlayer()
        {
            // Player exited level from entrance point
            if (GameManager.instance.exitPoint == 0)
            {      
                _playerSpawn = GameObject.FindGameObjectWithTag("Exit").transform.position;
                _playerSpawn.x -= 2f;

                Instantiate(_player, _playerSpawn, Quaternion.identity);                
            }

            // Player exited level from exit point
            else if (GameManager.instance.exitPoint == 1)
            {                
                _playerSpawn = GameObject.FindGameObjectWithTag("Entrance").transform.position;
                _playerSpawn.x += 2f;

                Instantiate(_player, _playerSpawn, Quaternion.identity);                
            }
        }
    }
}