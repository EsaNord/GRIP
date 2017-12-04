using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PuzzleSwitch : MonoBehaviour
    {

        [SerializeField]
        private GameObject _door;

        private bool _open = false;

        public bool Activated
        {
            set { _open = value; }
        }

        // Update is called once per frame
        void Update()
        {
            if (_open)
            {
                Debug.Log("Open door");
                this.gameObject.GetComponent<Animator>().SetBool("Activated", true);
                _door.GetComponent<Animator>().SetBool("Open", true);
                _door.GetComponent<BoxCollider2D>().enabled = false;                
            }
        }
    }
}