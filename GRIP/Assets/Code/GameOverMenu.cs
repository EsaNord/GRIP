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
        private Text _scoreText;
        [SerializeField]
        private GameObject _badEndBg;

        public void MainMenu()
        {
            SFXPlayer.Instance.Play(Sound.MenuClick);
            SceneManager.LoadScene("MainMenu");
        }

        private void Awake()
        {
            if (GameManager.instance.playerWon)
            {                
                _winText.SetActive(true);
            }
            else
            {                
                _badEndBg.SetActive(true);
            }

            _scoreText.text = "Final Score: " + GameManager.instance.score;
        }
    }
}