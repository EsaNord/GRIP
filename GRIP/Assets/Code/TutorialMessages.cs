using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        private GameObject _dialoqueObject;
        private string _dialoqueFile = "Dialoque";
        private string _tutorialFile = "Tutorial";

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
            _dialoqueObject = this.gameObject;
            string message = "";

            if (_dialoqueObject.tag == "Tutorial")
            {
                _tutorialFile += (GameManager.instance.currentLevel + 1);                
                message = Resources.Load(_tutorialFile).ToString();
            }
            else if (_dialoqueObject.tag == "Dialoque")
            {
                _dialoqueFile += (GameManager.instance.currentLevel + 1);                
                message = Resources.Load(_dialoqueFile).ToString();
            }
            else
            {
                Debug.LogError("No Tag On Object");
            }

            ResetString();

            return message;
        }   
        
        private void ResetString()
        {
            _tutorialFile = "Tutorial";
            _dialoqueFile = "Dialoque";
        }
    }
}