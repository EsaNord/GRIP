using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private int _playerHealth = 3;

        private string _lastCheckPointName;
        private Vector2 _checkPoint;
        private GameObject _lastCheckPoint;
       
        private void RespawnPoint()
        {
            Debug.Log("RESPAWN SEARCH");
            if (_lastCheckPointName != null)
            {
                _lastCheckPoint = GameObject.Find(_lastCheckPointName);                
                _checkPoint = new Vector2(_lastCheckPoint.transform.position.x,
                    _lastCheckPoint.transform.position.y + 1.3f);
            }
            else
            {                
                _checkPoint = FindObjectOfType<PlayerSpawner>().LevelSpawn;
            }            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Killer")
            {
                Debug.Log("DEAD");
                if (_playerHealth > 1)
                {
                    _playerHealth--;
                    RespawnPoint();
                    transform.position = _checkPoint;                  
                }
                else
                {
                    Destroy(this.gameObject);
                    Debug.Log("FINISHED");
                }
            }
            if (collision.tag == "Checkpoint")
            {
                Debug.Log("SPAWNSAVED");
                
                if (_lastCheckPointName != null)
                {
                    _lastCheckPoint = GameObject.Find(_lastCheckPointName);
                    _lastCheckPoint.GetComponent<Animator>().SetBool("visited", false);
                }                

                _lastCheckPointName = collision.name;
                _lastCheckPoint = GameObject.Find(_lastCheckPointName);
                _lastCheckPoint.GetComponent<Animator>().SetBool("visited", true);
            }
        }
    }
}