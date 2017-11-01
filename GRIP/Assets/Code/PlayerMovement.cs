using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField]
        private float _speed = 2;
        [SerializeField]
        private float _jumpForce = 15;
        [SerializeField]
        private float _distance = 1.0f;
        [SerializeField]
        private LayerMask _groundLayer;        

        private bool _grounded;
        private Animator _playerAnimator;
        private SpriteRenderer _playerRenderer;
        private Rigidbody2D _playerBody;

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerRenderer = GetComponent<SpriteRenderer>();
            _playerBody = GetComponent<Rigidbody2D>();            
        }

        private void CheckComponents()
        {
            if (_playerAnimator == null)
            {
                Debug.Log("No Animator");
                _playerAnimator = GetComponent<Animator>();
            }
            if (_playerRenderer == null)
            {
                Debug.Log("No Renderer");
                _playerRenderer = GetComponent<SpriteRenderer>();
            }
            if (_playerBody == null)
            {
                Debug.Log("No Rigidbody2d");
                _playerBody = GetComponent<Rigidbody2D>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckComponents();
            Move();            
        }

        private void Move()
        {            
            GroundCheck();
            if (Input.GetKeyDown(KeyCode.Space) && _grounded)
            {
                _playerBody.velocity = Vector3.up * _jumpForce * Time.deltaTime;
                Debug.Log("Jumping");
                _playerAnimator.SetBool("Moving", false);
                _playerAnimator.SetBool("Jumped", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                if (_grounded)
                {
                    Debug.Log("Animateing movement left");
                    _playerAnimator.SetBool("Moving", true);
                }
                _playerRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                if (_grounded)
                {
                    Debug.Log("Animateing movement right");
                    _playerAnimator.SetBool("Moving", true);
                }
                _playerRenderer.flipX = false;
            }                        
            else
            {
                Debug.Log("No movement animation");
                _playerAnimator.SetBool("Moving", false);                
            }
        }

        private void GroundCheck()
        {
            Debug.DrawRay(transform.position, -Vector2.up, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _distance, _groundLayer);
            if (hit.collider != null)
            {
                _grounded = true;
                Debug.Log("GROUND");
                _playerAnimator.SetBool("Jumped", false);
            }
            else
            {
                _grounded = false;
                Debug.Log("AIR");
            }
        }
    }
}