using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerAbilities : MonoBehaviour {

        [SerializeField]
        private float _MaxDistance = 10f;
        [SerializeField]
        private LayerMask _TargetLayer;
        [SerializeField]
        private float _AdjustDistance = 1f;

        private DistanceJoint2D _Joint2d;
        private Vector3 _TargetPos;
        private RaycastHit2D _Hit;
        private bool _Connected;

        private void Awake()
        {
            _Joint2d = GetComponent<DistanceJoint2D>();
            _Joint2d.enabled = false;
        }

        // Update is called once per frame
        void Update() {
            if (GameManager.instance.grapplingHook)
            {
                GrapplingHook();
            } 
        }

        // Grappling hook abilty. 
        private void GrapplingHook()
        {
            // Detects if mouse's left button is pressed down and held
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed");
                _TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _TargetPos.z = 0;

                _Hit = Physics2D.Raycast(transform.position, _TargetPos - transform.position, _MaxDistance, _TargetLayer);
                if (_Hit.collider != null && _Hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    _Joint2d.enabled = true;
                    _Joint2d.connectedBody = _Hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    _Joint2d.connectedAnchor = new Vector2(0, (0 - (_Hit.collider.bounds.size.y / 3)));
                    _Joint2d.distance = Vector2.Distance(transform.position, _Hit.transform.position);

                    _Connected = true;
                    Debug.Log("Target PosX: " + _Hit.transform.position.x);
                    Debug.Log("Target PosY: " + (_Hit.transform.position.y - _Hit.collider.bounds.size.y / 2));
                }                
            }
            if (_Connected)
            {
                Vector3 anchor = new Vector3(_Hit.collider.transform.position.x,
                    (_Hit.collider.transform.position.y - _Hit.collider.bounds.size.y / 2), 0);
                Debug.DrawRay(transform.position, (anchor - transform.position), Color.red);
                Debug.Log("Up or Down");
                if (Input.GetKey(KeyCode.W))
                {
                    Debug.Log("Up");
                    _Joint2d.distance -= _AdjustDistance * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    Debug.Log("Down");
                    _Joint2d.distance += _AdjustDistance * Time.deltaTime;
                }

                //Debug.Log("Anchor: " + (_Hit.point - new Vector2(0, _Hit.collider.transform.position.y)));
                Debug.Log("Distance: " + _Joint2d.distance);
                Debug.Log("DistanceInt: " + Vector2.Distance(transform.position, _Hit.transform.position));
            }

            // Detects if left mouse button is lifted
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Released");
                _Joint2d.enabled = false;
                _Connected = false;
            }
        }
    }
}