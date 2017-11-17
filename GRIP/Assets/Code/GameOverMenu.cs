using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GRIP
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _winText;
        [SerializeField]
        private GameObject _defeatText;
        [SerializeField]
        private Text _scoreText;

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void Awake()
        {
            if (GameManager.instance.playerDied)
            {
                _defeatText.SetActive(true);
            }
            else
            {
                _winText.SetActive(true);
            }

            _scoreText.text = "Final Score: " + GameManager.instance.score;
        }
    }
}