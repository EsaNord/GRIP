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
            if (collision.gameObject.name == "GHookP")
            {
                Debug.Log("Grappling Hook Power Up  Foud!");
                GameManager.instance.grapplingHook = true;
                Destroy(collision.gameObject);
            }
        }
    }
}