using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GRIP {
    public class LevelController : MonoBehaviour
    {        
        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.playerDied)
            {
                SceneManager.LoadScene("EndSceen");
            }
    
        }
    }
}