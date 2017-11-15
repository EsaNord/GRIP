using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CollectableInfo : MonoBehaviour
    {
        [SerializeField, Tooltip("Corresponding value to boolean array")]
        private int _position;
        [SerializeField, Tooltip("How many points does this give")]
        private int _points;

        public int GetValue
        {
            get { return _position; }
        }

        public int GetPoints
        {
            get { return _points; }
        }
    }
}