using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GRIP
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _winText;
        [SerializeField]
        private GameObject _defeatText;

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
        }
    }
}