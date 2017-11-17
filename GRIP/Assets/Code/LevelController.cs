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

        private CheckPoint _playerInfo;

        private void Start()
        {
            CheckComponents();
        }

        // Update is called once per frame
        private void Update()
        {
            CheckComponents();
            if (GameManager.instance.playerDied)
            {
                SceneManager.LoadScene("EndSceen");
            }

            _playerLives.text = "Lives: " + _playerInfo.PlayerLives;
            _playerScore.text = "" + GameManager.instance.score;
            Debug.Log(_playerLives.text);
            Debug.Log(_playerScore.text);
        }

        private void CheckComponents()
        {
            if (_playerInfo == null && !GameManager.instance.playerDied)
            {
                _playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CheckPoint>();
            }
        }
    }
}