using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        private int _playerHealth = 3;

        private Vector2 _checkPoint;
        private GameObject _lastCheckPoint;
        private int _collected = 0;

        public int ColCollected
        {
            get { return _collected; }
            set { _collected = value; }
        }

        public int PlayerLives
        {
            get { return _playerHealth; }
        }

        public Vector3 LastCheckpoint
        {
            get { return _checkPoint; }
        }

        // Saves respawn point
        private void RespawnPoint()
        {
            Debug.Log("RESPAWN SEARCH");
            if (GameManager.instance.lastCheckpointName != null)
            {
                _lastCheckPoint = GameObject.Find(GameManager.instance.lastCheckpointName);
                _checkPoint = new Vector2(_lastCheckPoint.transform.position.x,
                    _lastCheckPoint.transform.position.y + 1.3f);
            }
        }

        // Checks player's collision with trigger objects
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If collision is with level exit or entrance
            if (collision.gameObject.tag == "Entrance" ||
                collision.gameObject.tag == "Exit")
            {
                if (collision.gameObject.tag == "Entrance")
                {
                    GameManager.instance.exitPoint = 0;
                    Debug.Log("Entrance point");
                }
                else if (collision.gameObject.tag == "Exit")
                {
                    GameManager.instance.exitPoint = 1;
                    Debug.Log("Exit point");
                }                
                GameManager.instance.changeLevel = true;
            }

            // If collision if with death plane
            if (collision.tag == "Killer")
            {
                SFXPlayer.Instance.Play(Sound.Death);
                Debug.Log("DEAD");
                GameManager.instance.justDied = true;
                GameManager.instance.checkDone = false;
                Destroy(this.gameObject);
            }

            // If collision is with checkpoint
            if (collision.tag == "Checkpoint")
            {
                Debug.Log("SPAWNSAVED");

                SFXPlayer.Instance.Play(Sound.Checkpoint);

                if (GameManager.instance.lastCheckpointName != null)
                {
                    _lastCheckPoint = GameObject.Find(GameManager.instance.lastCheckpointName);
                    _lastCheckPoint.GetComponent<Animator>().SetBool("visited", false);
                }

                GameManager.instance.lastCheckpointName = collision.name;
                _lastCheckPoint = GameObject.Find(GameManager.instance.lastCheckpointName);
                _lastCheckPoint.GetComponent<Animator>().SetBool("visited", true);
                RespawnPoint();
            }

            // If object is grappling hook power up
            if (collision.gameObject.tag == "HookPU")
            {
                SFXPlayer.Instance.Play(Sound.Collect);
                MusicPlayer.Instance.Stop();
                MusicPlayer.Instance.PlayTrack(1);
                GameManager.instance.powerUpArray[0] = true;
                Destroy(collision.gameObject);
            }

            // If object is collectable
            if (collision.gameObject.tag == "Collectable")
            {
                SFXPlayer.Instance.Play(Sound.Collect);
                CollectableInfo collectable = collision.gameObject.GetComponent<CollectableInfo>();
                GameManager.instance.score += collectable.GetPoints;
                if (GameManager.instance.currentLevel == 0)
                {
                    GameManager.instance.lvl1Col[collectable.GetValue] = true;
                }
                _collected++;
                collision.gameObject.SetActive(false);
            }

            if (collision.gameObject.tag == "Dialoque" &&
                !this.gameObject.GetComponent<PlayerMovement>().isActiveAndEnabled)
            {
                this.gameObject.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Switch")
            {                
                if (Input.GetKeyDown(KeyCode.F))
                {                    
                    collision.gameObject.GetComponent<PuzzleSwitch>().Activated = true;
                }
            }
        }
    }
}