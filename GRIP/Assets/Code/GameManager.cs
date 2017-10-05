﻿using System.Collections;
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
    }
}