using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GRIP
{
    public class LevelChange : MonoBehaviour
    {
        private string[] _levels = new string[]
        {
            "Level 1",  "Level 2"
        };

        private int _level;
        private int _finalLevel;       

        private void Awake()
        {
            _finalLevel = _levels.Length;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "End" &&
                collision.gameObject.tag == "Entrance" ||
                collision.gameObject.tag == "Exit")
            {
                if (collision.gameObject.tag == "Entrance")
                {
                    GameManager.instance.exitPoint = 0;
                    Debug.Log("Entrance point");
                }
                else if (collision.gameObject.tag == "Exit")
                {
                    GameManager.instance.exitPoint = 1;
                    Debug.Log("Exit point");
                }

                NextLevel(); 
            }            
        }

        private void NextLevel ()
        {
            if (GameManager.instance.exitPoint == 0)
            {
                _level = GameManager.instance.currentLevel - 1;
            }
            else if (GameManager.instance.exitPoint == 1)
            {
                _level = GameManager.instance.currentLevel + 1;
            }

            Debug.Log("Level: " + _level);
            Debug.Log("Final: " + _finalLevel);
                       
            if (_level >= _finalLevel)
            {
                GameManager.instance.playerWon = true;
                SceneManager.LoadScene("EndSceen");
            }
            else
            {
                GameManager.instance.currentLevel = _level;
                SceneManager.LoadScene(_levels[_level]);
            }
        }
    }
}