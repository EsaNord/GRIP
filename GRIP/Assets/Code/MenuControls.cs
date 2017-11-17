using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GRIP
{
    public class MenuControls : MonoBehaviour {

        public void NewGame()
        {
            SceneManager.LoadScene("Level 1");
            GameManager.instance.Reset();
        }

        public void QuitGame()
        {
            Application.Quit();
        }        
    }
}