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
        public GameObject player;
        public bool firstSpawn;
        public bool playerDied;
        public bool playerWon;
        public int score = 0;
        
        public bool[] powerUpArray = new bool[1];
        // 2d array? levelId,CollectableId?
        public bool[] lvl1Col = new bool[3];

        private void Awake()
        {
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
            playerDied = false;
            powerUpArray[0] = false;
            firstSpawn = false;
            score = 0;

            ResetCollectables();
        }

        private void ResetCollectables()
        {
            for (int i = 0; i < lvl1Col.Length; i++)
            {
                lvl1Col[i] = false;
            }
        }
    }
}