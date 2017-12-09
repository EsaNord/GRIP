using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class TrapTrigger : MonoBehaviour
    {
        [SerializeField]
        private StalactiteTrap[] _traps;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                for (int i = 0; i < _traps.Length; i++)
                {
                    _traps[i].Triggered = true;
                }
            }
        }
    }
}