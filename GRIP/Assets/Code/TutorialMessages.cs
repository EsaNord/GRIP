using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GRIP
{
    public class TutorialMessages : MonoBehaviour
    {
        [SerializeField]
        private GameObject _window;
        [SerializeField]
        private Text _tutorialText;
                
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _window.SetActive(true);
                _tutorialText.text = TutorialMessage();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _window.SetActive(false);
            }
        }

        private string TutorialMessage()
        {
            string message = "";

            if (GameManager.instance.currentLevel == 0)
            {
                message = "Placeholder text for story" +
                    " (short version: you fell into hole and now you" +
                    " must get back up into surface)";
            }
            else if (GameManager.instance.currentLevel == 1)
            {
                message = "Grapling hook power up ahead, mouse controlled." +
                    " you can adjust 'rope' lengt with W & S." +
                    "You can also reaim the hook while hanging from previous target.";
            }
            else
            {
                message = "No Text Found";
            }

            return message;
        }
    }
}