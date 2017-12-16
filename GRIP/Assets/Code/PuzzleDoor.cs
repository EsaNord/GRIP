using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PuzzleDoor : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _switch;

        private bool _open;
        private bool _closed;

        private void Update()
        {
            Cleared();

            if (_open)
            {
                this.gameObject.GetComponent<Animator>().SetBool("Open", true);
            }
        }

        private void Cleared()
        {
            for (int i = 0; i < _switch.Length; i++)
            {
                if (_switch[i].GetComponent<PuzzleSwitch>().Activated == false)
                {
                    _open = false;
                    _closed = true;
                } 
                else if (_switch[i].GetComponent<PuzzleSwitch>().Activated == true && !_closed)
                {
                    _open = true;
                }
            }
            _closed = false;
        }
    }
}