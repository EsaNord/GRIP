using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GRIP {
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Text _playerLives;
        [SerializeField]
        private Text _playerScore;
        [SerializeField]
        private Text _collectables;
        [SerializeField]
        private GameObject _defeatText;
        [SerializeField]
        private GameObject _pauseMenu;
        [SerializeField]
        private PlayerSpawner _spawner;
        [SerializeField]
        private float _spawnDelay = 2f;
        
        private GameObject _player;
        private Vector3 _checkpoint;        
        private bool _timerStarted;
        private int _nextLevel;
        private float _timePassed;        
        private int _levelCol;
        private int _collected;
        
        public GameObject Player
        {
            set { _player = value; }
        }                

        private void Start()
        {
            GetCollectableAmount();
            CheckComponents();           
        }

        // Update is called once per frame
        private void Update()
        {
            CheckComponents();

            if (_player != null)
            {
                _checkpoint = _player.GetComponent<PlayerCollision>().LastCheckpoint;                
            }            
            if (_player == null && GameManager.instance.firstSpawn)
            {
                DeathTimer();
                if (_timePassed >= _spawnDelay)
                {                    
                    PlayerRespawn();
                }
            }
            if (GameManager.instance.changeLevel)
            {
                GameManager.instance.changeLevel = false;
                GameManager.instance.collected = 0;
                NextLevel();
            }

            CollectableCheck();

            _playerLives.text = "Lives: " + GameManager.instance.playerLives;
            _playerScore.text = "" + GameManager.instance.score;
            _collectables.text = "" + _collected + " / " + _levelCol;            
        }

        private void CollectableCheck()
        {
            if (!GameManager.instance.justDied && this.gameObject.GetComponent<CollectableManager>().CheckCollected >
                _player.GetComponent<PlayerCollision>().ColCollected)
            {
                _collected = this.gameObject.GetComponent<CollectableManager>().CheckCollected;
            }
            else if (GameManager.instance.justDied)
            {
                this.gameObject.GetComponent<CollectableManager>().RespawnColCheck();
            }
            else
            {
                _collected = _player.GetComponent<PlayerCollision>().ColCollected;
            }
        }

        private void DeathTimer()
        {
            if (!_timerStarted)
            {
                _timerStarted = true;
                _defeatText.SetActive(true);
                _timePassed = 0;
            }
            else
            {
                _timePassed += Time.deltaTime;
            }
        }

        private void PlayerRespawn()
        {
            GameManager.instance.playerLives--;
            _defeatText.SetActive(false);
            if (GameManager.instance.playerLives > 0)
            {
                _spawner.Respawn(_checkpoint);
                _timerStarted = false;
            }
            else
            {                         
                GameManager.instance.playerWon = false;
                SceneManager.LoadScene("EndSceen");
            }
        }

        private void NextLevel()
        {
            if (GameManager.instance.exitPoint == 0)
            {
                _nextLevel = GameManager.instance.currentLevel - 1;
            }
            else if (GameManager.instance.exitPoint == 1)
            {
                _nextLevel = GameManager.instance.currentLevel + 1;
            }

            if (_nextLevel >= GameManager.instance.finalLevel)
            {                
                GameManager.instance.playerWon = true;
                SceneManager.LoadScene("EndSceen");
            }
            else
            {
                GameManager.instance.currentLevel = _nextLevel;
                GameManager.instance.lastCheckpointName = null;
                SceneManager.LoadScene(GameManager.instance.levels[_nextLevel]);
            }
        }

        private void CheckComponents()
        {            
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }
        }

        private void GetCollectableAmount()
        {
            if (GameManager.instance.currentLevel == 0)
            {
                _levelCol = GameManager.instance.lvl1Col.Length;
            }
            else if (GameManager.instance.currentLevel == 1)
            {
                _levelCol = GameManager.instance.lvl2Col.Length;
            }
            else if (GameManager.instance.currentLevel == 2)
            {
                _levelCol = GameManager.instance.lvl3Col.Length;
            }
            else if (GameManager.instance.currentLevel == 3)
            {
                _levelCol = GameManager.instance.lvl4Col.Length;
            }
        }
    }
}