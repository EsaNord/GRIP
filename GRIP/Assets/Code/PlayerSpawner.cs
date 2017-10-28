using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerSpawner : MonoBehaviour
    {

        private GameObject _Player;        
        private Vector2 _PlayerSpawn;
        
        public Vector2 LevelSpawn
        {
            get { return _PlayerSpawn; }
        }
            
        private void Start()
        {
            _Player = GameManager.instance.player;

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
            _PlayerSpawn = new Vector2(2, 3);
            
            Instantiate(_Player, _PlayerSpawn, Quaternion.identity);            
        }

        private void SpawnPlayer()
        {
            // Player exited level from entrance point
            if (GameManager.instance.exitPoint == 0)
            {      
                _PlayerSpawn = GameObject.FindGameObjectWithTag("Exit").transform.position;
                _PlayerSpawn.x -= 2f;

                Instantiate(_Player, _PlayerSpawn, Quaternion.identity);                
            }

            // Player exited level from exit point
            else if (GameManager.instance.exitPoint == 1)
            {                
                _PlayerSpawn = GameObject.FindGameObjectWithTag("Entrance").transform.position;
                _PlayerSpawn.x += 2f;

                Instantiate(_Player, _PlayerSpawn, Quaternion.identity);                
            }
        }
    }
}