using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CollectableSpawner : MonoBehaviour
    {
        private Vector2[] _levelCArray;
        private Vector2[] _levelPUArray;
        private GameObject[] _levelPU;
        private GameObject[] _levelC;

        private Vector2[] _level1PowerUps = new Vector2[] 
        {
            new Vector2(19,2)
        };

        [SerializeField]
        private GameObject[] _level1PU = new GameObject[1]; 

        private void Start()
        {
            Debug.Log("Array Size: " + _level1PowerUps.Length);

            if (GameManager.instance.currentLevel == 0)
            {
                Debug.Log("Spawning");
                _levelPUArray = _level1PowerUps;
                _levelPU = _level1PU;
            }
            else if (GameManager.instance.currentLevel == 1)
            {
                Debug.Log("No Collectables in lvl 2 yet");
            }

            SpawnCollectable();
        }

        private void SpawnCollectable()
        {
            for (int i = 0; i < _levelPUArray.Length; i++)
            {
                if (GameManager.instance.powerUpArray[i] != true)
                {
                    Instantiate(_levelPU[i], _levelPUArray[i], Quaternion.identity);
                    Debug.Log("Spawned Power Up");
                }
                else
                {
                    Debug.Log("Already Collected");
                }
            }
        }
    }
}