using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class StalactiteTrap : MonoBehaviour
    {
        [SerializeField]
        private float _delayTime = 1.25f;
        [SerializeField]
        private float _fallSpeed = 7f;

        private float _timePassed = 0f;
        private bool _triggered = false;

        public bool Triggered
        {
            set { _triggered = value; }
        }

        // Update is called once per frame
        void Update()
        {
            if (_triggered)
            {
                _timePassed += Time.deltaTime;
            }
            if (_timePassed >= _delayTime)
            {
                Move();
            }
        }        

        private void Move()
        {
            transform.Translate(-Vector3.up * _fallSpeed * Time.deltaTime);
        }        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collision: " + collision.gameObject.tag);

            if (collision.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }

            if (collision.gameObject.tag == "Ground")
            {
                Destroy(this.gameObject);
            }
        }
    }
}