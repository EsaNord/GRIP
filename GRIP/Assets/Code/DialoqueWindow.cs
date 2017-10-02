﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GRIP
{
    public class DialoqueWindow : MonoBehaviour
    {

        [SerializeField]
        private GameObject _DialoqueObject;
        [SerializeField]
        private GameObject _TextObject;

        private GameObject _DialoqueChar;
        private Text _TextField;

        // Use this for initialization
        void Start()
        {
            _DialoqueChar = this.gameObject;
            _TextField.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _DialoqueObject.SetActive(true);
                    _TextObject.SetActive(true);

                    if (_DialoqueChar.name == "DialoqueTester")
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
                _DialoqueObject.SetActive(false);
                _TextObject.SetActive(false);
                _TextField.text = "";
            }
        }
    }
}