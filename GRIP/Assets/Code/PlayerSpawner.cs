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
            _PlayerSpawn = new Vector2(2, 2);
            
            Instantiate(_Player, _PlayerSpawn, Quaternion.identity);            
        }

        private void SpawnPlayer()
        {
            // Entrance point
            if (GameManager.instance.exitPoint == 0)
            {
                if (GameManager.instance.currentLevel == 0)
                {
                    _PlayerSpawn = new Vector2(20, 2);                   
                }
                else if (GameManager.instance.currentLevel == 1)
                {
                    _PlayerSpawn = new Vector2(38, -3);                    
                }
                
                Instantiate(_Player, _PlayerSpawn, Quaternion.identity);                
            }

            // Exit point
            else if (GameManager.instance.exitPoint == 1)
            {
                if (GameManager.instance.currentLevel == 0)
                {
                    _PlayerSpawn = new Vector2(-28, 4.5f);                    
                }
                else if (GameManager.instance.currentLevel == 1)
                {
                    _PlayerSpawn = new Vector2(-24, -3);                    
                }
                
                Instantiate(_Player, _PlayerSpawn, Quaternion.identity);                
            }
        }
    }
}