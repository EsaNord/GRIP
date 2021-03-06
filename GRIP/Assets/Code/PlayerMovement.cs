﻿using System.Collections;
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
        [SerializeField, Tooltip("Distance from ground")]
        private float _distance = 1.0f;
        [SerializeField, Tooltip("Distance from wall")]
        private float _wallDistance = 0.75f;
        [SerializeField]
        private LayerMask _groundLayer;
        [SerializeField]
        private LayerMask _wallLayer;        

        private bool _grounded;
        private Animator _playerAnimator;
        private SpriteRenderer _playerRenderer;
        private PlayerAbilities _abillities;
        private PlayerCollision _collisionDet;
        private Rigidbody2D _playerBody;
        private bool _wallLeft = false;
        private bool _wallRight = false;        

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _abillities = GetComponent<PlayerAbilities>();
            _collisionDet = GetComponent<PlayerCollision>();
            _playerRenderer = GetComponent<SpriteRenderer>();
            _playerBody = GetComponent<Rigidbody2D>();            
        }        

        private void Update()
        {
            
            GroundCheck();
            WallCheck();
            if (!_abillities.RopeConnected)
            {
                Move();
            }
            else
            {
                _abillities.RopeMove(_speed);
            }            
        }        

        private void Move()
        {            
            if (Input.GetKeyDown(KeyCode.Space) && !_collisionDet.Ladder)
            {
                if (_grounded)
                {
                    _playerBody.velocity = Vector3.up * _jumpForce;
                    SFXPlayer.Instance.Play(Sound.Jump);
                    _playerAnimator.SetTrigger("Jumped");
                }
                else
                {                    
                    if (_wallLeft && !_wallRight && !_grounded)
                    {
                        _playerBody.velocity = (Vector3.up + Vector3.right) * _jumpForce;
                        SFXPlayer.Instance.Play(Sound.Jump);
                        _playerAnimator.SetTrigger("Jumped");
                    }
                    else if (!_wallLeft && _wallRight && !_grounded)
                    {
                        _playerBody.velocity = (Vector3.up + Vector3.left) * _jumpForce;
                        SFXPlayer.Instance.Play(Sound.Jump);
                        _playerAnimator.SetTrigger("Jumped");
                    }                     
                }                               
            }
            if (_collisionDet.Ladder)
            {
                _playerBody.gravityScale = 0;
                _playerBody.velocity = new Vector2(0, 0);
                _playerAnimator.SetBool("Moving", false);

                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.up * _speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.down * _speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!_collisionDet.Ladder)
                {
                    _playerBody.gravityScale = 1;
                }
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
                if (_grounded)
                {                    
                    _playerAnimator.SetBool("Moving", true);
                    _playerRenderer.flipX = true;
                }                              
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (!_collisionDet.Ladder)
                {
                    _playerBody.gravityScale = 1;
                }
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                if (_grounded)
                {                    
                    _playerAnimator.SetBool("Moving", true);
                    _playerRenderer.flipX = false;
                }                
            }
            else if (!_collisionDet.Ladder)
            {
                _playerBody.gravityScale = 1;
                _playerAnimator.SetBool("Moving", false);
            }                      
        }

        private void GroundCheck()
        {
            bool left = false;
            bool right = false;
            
            Debug.DrawRay(transform.position + -transform.right * 0.2f, -Vector2.up * _distance, Color.blue);
            RaycastHit2D hit = Physics2D.Raycast(transform.position + -transform.right * 0.2f, -Vector2.up, _distance, _groundLayer);
            if (hit.collider != null)
            {
                left = true;
            }
            Debug.DrawRay(transform.position + transform.right * 0.2f, -Vector2.up * _distance, Color.red);
            hit = Physics2D.Raycast(transform.position + transform.right * 0.2f, -Vector2.up, _distance, _groundLayer);
            if (hit.collider != null)
            {
                right = true;
            }
            if (right || left)
            {
                _grounded = true;
                _playerAnimator.SetBool("Ground", true);                
            }
            else
            {
                _grounded = false;
                _playerAnimator.SetBool("Ground", false);
            }            
        }

        private void WallCheck()
        {
            Debug.DrawRay(transform.position, Vector2.left * _wallDistance, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, _wallDistance, _wallLayer);
            if (hit.collider != null && !_wallLeft && !_grounded)
            {
                _wallLeft = true;
                _wallRight = false;                
            }
            else {
                Debug.DrawRay(transform.position, Vector2.right * _wallDistance, Color.yellow);
                hit = Physics2D.Raycast(transform.position, Vector2.right, _wallDistance, _wallLayer);
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
            if (_wallRight && !_grounded)
            {
                _playerAnimator.SetBool("Wall", true);
                _playerRenderer.flipX = false;
            }
            else if (_wallLeft && !_grounded)
            {
                _playerAnimator.SetBool("Wall", true);
                _playerRenderer.flipX = true;
            }
            else
            {
                _playerAnimator.SetBool("Wall", false);
            }
        }
    }
}