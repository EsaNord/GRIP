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
            "TESTLEVEL", "TESTLEVEL2"
        };

        private int _level;

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
                Debug.Log("Level: " + _level);
                GameManager.instance.currentLevel = _level;
                SceneManager.LoadScene(_levels[_level]);
            }    
            else if (collision.gameObject.tag == "End")
            {
                Debug.Log("THE END");
            }
        }

        private void NextLevel ()
        {
            _level = GameManager.instance.currentLevel + 1;

            // FOR TESTLEVELS 
            if (_level > 1)
            {
                _level = 0;
            }
        }
    }
}