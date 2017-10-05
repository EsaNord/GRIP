using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField]
        private float speed = 2;
        [SerializeField]
        private float jumpForce = 15;
        [SerializeField]
        private float _Distance = 1.0f;
        [SerializeField]
        private LayerMask _GroundLayer;        

        private bool _Grounded;

        // Update is called once per frame
        void Update()
        {
            Move();            
        }

        private void Move()
        {            
            GroundCheck();
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);                
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);                
            }
            if (Input.GetKeyDown(KeyCode.Space) && _Grounded)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.up * jumpForce * Time.deltaTime;               
            }
        }

        private void GroundCheck()
        {
            Debug.DrawRay(transform.position, -Vector2.up, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _Distance, _GroundLayer);
            if (hit.collider != null)
            {
                _Grounded = true;
                Debug.Log("GROUND");
            }
            else
            {
                _Grounded = false;
                Debug.Log("AIR");
            }
        }
    }
}