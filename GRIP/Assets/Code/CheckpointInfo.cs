using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CheckpointInfo : MonoBehaviour
    {

        private bool _active;

        public bool Activated
        {
            set { _active = value; }
            get { return _active; }
        }
    }
}