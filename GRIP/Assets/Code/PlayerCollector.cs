using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerCollector : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // If object is grappling hook power up
            if (collision.gameObject.tag == "HookPU")
            {
                Debug.Log("Grappling Hook Power Up  Found!");
                GameManager.instance.powerUpArray[0] = true;
                Destroy(collision.gameObject);
            }
        }
    }
}