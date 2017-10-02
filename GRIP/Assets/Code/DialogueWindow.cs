using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GRIP
{
    public class DialoqueWindow : MonoBehaviour
    {

        [SerializeField]
        private GameObject _DialogueObject;
        [SerializeField]
        private GameObject _TextObject;

        private GameObject _DialogueChar;
        private Text _TextField;

        // Use this for initialization
        void Start()
        {
            _DialogueChar = this.gameObject;
            _TextField = _TextObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("WTF");
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("PRESS F");
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("OPEN");
                    _DialogueObject.SetActive(true);
                    _TextObject.SetActive(true);

                    if (_DialogueChar.name == "DialoqueTester")
                    {
                        _TextField.text = "This should apper when F is pressed near this blue box";
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("LEFT");
                _DialogueObject.SetActive(false);
                _TextObject.SetActive(false);
                _TextField.text = "";
            }
        }
    }
}