using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP {
    public class CollectableManager : MonoBehaviour {

        [SerializeField]
        private GameObject _powerUp;
        [SerializeField]
        private GameObject[] _collectables;

        private int _lvlPowerUp;
        private bool[] _colCheckList;
        private bool _hasCollectables = false;
        private bool _hasPowerUps = false;

        private void Awake()
        {
            if (GameManager.instance.currentLevel == 0)
            {
                _hasCollectables = true;
                _hasPowerUps = true;
                _lvlPowerUp = 0;
                _colCheckList = GameManager.instance.lvl1Col;
            }
            else
            {
                _hasCollectables = false;
                _hasPowerUps = false;
            }

            CheckCollectables();
        }

        private void CheckCollectables()
        {
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