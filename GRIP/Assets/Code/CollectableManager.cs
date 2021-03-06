﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CollectableManager : MonoBehaviour {

        [SerializeField]
        private GameObject _powerUp;
        [SerializeField]
        private GameObject[] _collectables;

        private int _lvlPowerUp;
        private bool[] _colCheckList;
        private bool _hasCollectables = false;
        private bool _hasPowerUps = false;
        private int _collected = 0;

        public int CheckCollected
        {
            get { return _collected; }
        }

        public void RespawnColCheck()
        {
            if (!GameManager.instance.checkDone)
            {
                _collected = 0;
                LevelInfo();
                for (int i = 0; i < _collectables.Length; i++)
                {
                    if (_colCheckList[i])
                    {                        
                        _collected++;
                    }                   
                }
                GameManager.instance.checkDone = true;
                GameManager.instance.collected = _collected;
            }
        }

        private void LevelInfo()
        {
            if (GameManager.instance.currentLevel == 0)
            {
                _hasCollectables = true;
                _hasPowerUps = false;
                _colCheckList = GameManager.instance.lvl1Col;
            }
            else if (GameManager.instance.currentLevel == 1)
            {
                _hasCollectables = true;
                _hasPowerUps = true;
                _lvlPowerUp = 0;
                _colCheckList = GameManager.instance.lvl2Col;
            }
            else if (GameManager.instance.currentLevel == 2)
            {
                _hasCollectables = true;
                _hasPowerUps = false;
                _colCheckList = GameManager.instance.lvl3Col;
            }
            else if (GameManager.instance.currentLevel == 3)
            {
                _hasCollectables = true;
                _hasPowerUps = false;
                _colCheckList = GameManager.instance.lvl4Col;
            }
            else if (GameManager.instance.currentLevel == 4)
            {
                _hasCollectables = true;
                _hasPowerUps = false;
                _colCheckList = GameManager.instance.lvl5Col;
            }
            else
            {
                _hasCollectables = false;
                _hasPowerUps = false;
            }
        }

        private void Awake()
        {
            LevelInfo();

            _collected = 0;
            CheckCollectables();
        }               

        private void CheckCollectables()
        {
            Debug.Log("Checking Collectables");
            if (_hasPowerUps)
            {                
                if (GameManager.instance.powerUpArray[_lvlPowerUp])
                {
                    _powerUp.SetActive(false);
                }
                else
                {
                    _powerUp.SetActive(true);
                }
            }

            if (_hasCollectables)
            {
                for (int i = 0; i < _collectables.Length; i++)
                {
                    if (_colCheckList[i])
                    {
                        _collectables[i].SetActive(false);
                        _collected++;
                    }
                    else
                    {
                        _collectables[i].SetActive(true);
                    }
                }
            }
        }
    }
}