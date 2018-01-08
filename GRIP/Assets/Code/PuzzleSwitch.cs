using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PuzzleSwitch : MonoBehaviour
    {

        [SerializeField]
        private GameObject[] _door;

        private bool _open = false;
        private bool _played;

        public bool Activated
        {
            set { _open = value; }
            get { return _open; }
        }

        // Update is called once per frame
        void Update()
        {
            if (_open)
            {                
                this.gameObject.GetComponent<Animator>().SetBool("Activated", true);
                for (int i = 0; i < _door.Length; i++)
                {
                    _door[i].GetComponent<Animator>().SetBool("Open", true);
                    if (!_played)
                    {
                        SFXPlayer.Instance.Play(Sound.Door);
                        _played = true;
                    }
                }                             
            }
        }
    }
}