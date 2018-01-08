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
        public float effectVolume;
        public float musicVolume;
        public bool justDied;
        public bool checkDone;
        public int collected;

        public string[] levels;

        public bool[] powerUpArray = new bool[1];

        public bool[] lvl1Col;
        public bool[] lvl2Col;
        public bool[] lvl3Col;
        public bool[] lvl4Col;
        public bool[] lvl5Col;

        private void Awake()
        {
            levels = new string[]
            {
                "Level 1",  "Level 2", "Level 3", "Level 4", "Level 5"
            };

            lvl1Col = new bool[25];
            lvl2Col = new bool[25];
            lvl3Col = new bool[25];
            lvl4Col = new bool[25];
            lvl5Col = new bool[25];

            effectVolume = 0.5f;
            musicVolume = 0.5f;

            finalLevel = levels.Length;
            Debug.Log("final: " + finalLevel + "/" + levels.Length);

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
            justDied = false;
            checkDone = false;

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
            for (int i = 0; i < lvl4Col.Length; i++)
            {
                lvl4Col[i] = false;
            }
            for (int i = 0; i < lvl5Col.Length; i++)
            {
                lvl5Col[i] = false;
            }
        }
    }
}