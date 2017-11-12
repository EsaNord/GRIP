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
        [SerializeField]
        private LayerMask _wallLayer;

        private bool _grounded;
        private Animator _playerAnimator;
        private SpriteRenderer _playerRenderer;
        private Rigidbody2D _playerBody;
        private bool _wallLeft = false;
        private bool _wallRight = false;
        //private bool;

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
        void FixedUpdate()
        {
            CheckComponents();
            Move();            
        }

        private void Move()
        {            
            GroundCheck();
            WallCheck();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_grounded)
                {
                    _playerBody.velocity = Vector3.up * _jumpForce * Time.deltaTime;
                }
                else
                {
                    Debug.Log("Walljump?");
                    if (_wallLeft && !_wallRight && !_grounded)
                    {
                        _playerBody.velocity = (Vector3.up + Vector3.right) * _jumpForce * Time.deltaTime;
                    }
                    else if (!_wallLeft && _wallRight && !_grounded)
                    {
                        _playerBody.velocity = (Vector3.up + Vector3.left) * _jumpForce * Time.deltaTime;
                    }
                }            
                _playerAnimator.SetBool("Moving", false);
                _playerAnimator.SetBool("Jumped", true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                if (_grounded)
                {                    
                    _playerAnimator.SetBool("Moving", true);
                }
                _playerRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                if (_grounded)
                {                    
                    _playerAnimator.SetBool("Moving", true);
                }
                _playerRenderer.flipX = false;
            }                        
            else
            {                
                _playerAnimator.SetBool("Moving", false);                
            }
        }

        private void GroundCheck()
        {
            bool left = false;
            bool right = false;

            Debug.DrawRay(transform.position + -transform.right * 0.4f, -Vector2.up, Color.blue);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + -transform.right * 0.4f, -Vector2.up, _distance, _groundLayer);
            if (hit.collider != null)
            {
                left = true;
            }
            Debug.DrawRay(transform.position + transform.right * 0.4f, -Vector2.up, Color.red);
            hit = Physics2D.Raycast(transform.position + transform.right * 0.4f, -Vector2.up, _distance, _groundLayer);
            if (hit.collider != null)
            {
                right = true;
            }
            if (right || left)
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
            Debug.Log("Ground: " + "Right: " + right + " / " + "Left: " + left);
        }

        private void WallCheck()
        {
            Debug.DrawRay(transform.position, Vector2.left, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, _distance, _wallLayer);
            if (hit.collider != null && !_wallLeft && !_grounded)
            {
                _wallLeft = true;
                _wallRight = false;                
            }
            else {
                Debug.DrawRay(transform.position, Vector2.right, Color.yellow);
                hit = Physics2D.Raycast(transform.position, Vector2.right, _distance, _wallLayer);
                if (hit.collider != null && !_wallRight && !_grounded)
                {
                    _wallRight = true;
                    _wallLeft = false;                    
                }
                else
                {
                    _wallRight = false;
                    _wallLeft = false;
                }
            }            
            Debug.Log("Wall: " + "Right: " + _wallLeft + " / " + "Left: " + _wallRight);
        }
    }
}