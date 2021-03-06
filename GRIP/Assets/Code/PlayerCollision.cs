﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField]
        private int _playerHealth = 5;

        private Vector2 _checkPoint;
        private GameObject _lastCheckPoint;
        private int _collected = 0;
        private bool _inLadder;

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

        public bool Ladder
        {
            get { return _inLadder; }
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

        private void PlayerParts()
        {
            GameObject hook = GameObject.FindGameObjectWithTag("Hook");
            GameObject handStart = GameObject.FindGameObjectWithTag("Shoulder");
            Destroy(hook);
            Destroy(handStart);
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
                }
                else if (collision.gameObject.tag == "Exit")
                {
                    GameManager.instance.exitPoint = 1;                    
                }
                SFXPlayer.Instance.Play(Sound.LevelEnd);
                GameManager.instance.changeLevel = true;
            }

            // If collision if with death plane
            if (collision.tag == "Killer")
            {
                SFXPlayer.Instance.Play(Sound.Death);                
                GameManager.instance.justDied = true;
                GameManager.instance.checkDone = false;
                PlayerParts();
                Destroy(this.gameObject);
            }

            // If collision is with checkpoint
            if (collision.tag == "Checkpoint")
            {                
                if (!collision.gameObject.GetComponent<CheckpointInfo>().Activated)
                {
                    SFXPlayer.Instance.Play(Sound.Checkpoint);
                }

                if (GameManager.instance.lastCheckpointName != null)
                {                    
                    _lastCheckPoint = GameObject.Find(GameManager.instance.lastCheckpointName);
                    _lastCheckPoint.GetComponent<Animator>().SetBool("visited", false);
                    _lastCheckPoint.GetComponent<CheckpointInfo>().Activated = false;
                }

                GameManager.instance.lastCheckpointName = collision.name;
                _lastCheckPoint = GameObject.Find(GameManager.instance.lastCheckpointName);
                _lastCheckPoint.GetComponent<Animator>().SetBool("visited", true);
                collision.gameObject.GetComponent<CheckpointInfo>().Activated = true;
                RespawnPoint();
            }

            // If object is grappling hook power up
            if (collision.gameObject.tag == "HookPU")
            {
                SFXPlayer.Instance.Play(Sound.PowerUp);
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
                else if (GameManager.instance.currentLevel == 1)
                {
                    GameManager.instance.lvl2Col[collectable.GetValue] = true;
                }
                else if (GameManager.instance.currentLevel == 2)
                {
                    GameManager.instance.lvl3Col[collectable.GetValue] = true;
                }
                else if (GameManager.instance.currentLevel == 3)
                {
                    GameManager.instance.lvl4Col[collectable.GetValue] = true;
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
                if (Input.GetKey(KeyCode.F)) //ATM WHEN HELD DOWN WORKS, EASIER FOR PRESENTATION
                {                    
                    collision.gameObject.GetComponent<PuzzleSwitch>().Activated = true;
                    SFXPlayer.Instance.Play(Sound.Lever);
                }
            }

            if (collision.gameObject.tag == "Ladder")
            {
                _inLadder = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ladder")
            {
                _inLadder = false;
            }
        }
    }
}