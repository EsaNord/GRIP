using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GRIP
{
    public class LevelChange : MonoBehaviour
    {
        private string[] _Levels = new string[]
        {
            "TESTLEVEL", "TESTLEVEL2"
        };

        private int level;

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
                Debug.Log("Level: " + level);
                GameManager.instance.currentLevel = level;
                SceneManager.LoadScene(_Levels[level]);
            }    
            else if (collision.gameObject.tag == "End")
            {
                Debug.Log("THE END");
            }
        }

        private void NextLevel ()
        {
            level = GameManager.instance.currentLevel + 1;

            // FOR TESTLEVELS 
            if (level > 1)
            {
                level = 0;
            }
        }
    }
}