using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int currentLevel;
        public int exitPoint;        
        public bool firstSpawn;        
        public bool playerWon;
        public int score = 0;
        public int playerLives;
        public int finalLevel;
        public string lastCheckpointName;
        public bool changeLevel;

        public string[] levels = new string[]
        {
            "Level 1",  "Level 2", "Level 3"
        };

        public bool[] powerUpArray = new bool[1];
        
        public bool[] lvl1Col = new bool[3];
        public bool[] lvl2Col = new bool[4];
        public bool[] lvl3Col = new bool[5];

        private void Awake()
        {
            finalLevel = levels.Length;

            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Reset()
        {            
            powerUpArray[0] = false;
            firstSpawn = false;
            score = 0;
            currentLevel = 0;
            lastCheckpointName = null;

            ResetCollectables();
        }

        private void ResetCollectables()
        {
            for (int i = 0; i < lvl1Col.Length; i++)
            {
                lvl1Col[i] = false;
            }
            for (int i = 0; i < lvl2Col.Length; i++)
            {
                lvl2Col[i] = false;
            }
            for (int i = 0; i < lvl3Col.Length; i++)
            {
                lvl3Col[i] = false;
            }
        }
    }
}