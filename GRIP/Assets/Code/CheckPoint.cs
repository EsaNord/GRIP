using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private int _PlayerHealth = 3;

        private string _LastCheckPointName;
        private Vector2 _CheckPoint;
        private GameObject _LastCheckPoint;
       
        private void RespawnPoint()
        {
            Debug.Log("RESPAWN SEARCH");
            if (_LastCheckPointName != null)
            {
                _LastCheckPoint = GameObject.Find(_LastCheckPointName);                
                _CheckPoint = new Vector2(_LastCheckPoint.transform.position.x,
                    _LastCheckPoint.transform.position.y + 1.3f);
            }
            else
            {                
                _CheckPoint = FindObjectOfType<PlayerSpawner>().LevelSpawn;
            }            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Killer")
            {
                Debug.Log("DEAD");
                if (_PlayerHealth > 1)
                {
                    _PlayerHealth--;
                    RespawnPoint();
                    transform.position = _CheckPoint;                  
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
                _LastCheckPointName = collision.name;
            }
        }
    }
}