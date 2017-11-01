using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GRIP
{
    public class DialogueWindow : MonoBehaviour
    {

        [SerializeField, Tooltip("Panel in canvas")]
        private GameObject _dialogueObject;
        [SerializeField, Tooltip("Text in panel")]
        private GameObject _textObject;

        private GameObject _dialogueChar;
        private Text _textField;

        // Use this for initialization
        void Start()
        {
            _dialogueChar = this.gameObject;
            _textField = _textObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay2D(Collider2D collision)
        {            
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("PRESS F");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("OPEN");
                    _dialogueObject.SetActive(true);
                    _textObject.SetActive(true);

                    if (_dialogueChar.name == "DialoqueTester")
                    {
                        _textField.text = "This should apper when F is pressed near this blue box";
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("LEFT");
                _dialogueObject.SetActive(false);
                _textObject.SetActive(false);
                _textField.text = "";
            }
        }
    }
}