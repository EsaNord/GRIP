using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class PlayerAbilities : MonoBehaviour {

        [SerializeField]
        private float _maxDistance = 10f;
        [SerializeField]
        private LayerMask _targetLayer;
        [SerializeField]
        private float _adjustDistance = 1f;
        [SerializeField]
        private GameObject _hook;
        [SerializeField]
        private GameObject _crosshair;

        private DistanceJoint2D _joint2d;
        private Vector3 _targetPos;
        private Vector3 _crosshairPos;
        private RaycastHit2D _hit;
        private bool _connected;
        private bool _createdCrosshair = false;
        private bool _createdHook = false;
        private Vector3[] _ropePoints = new Vector3[2];
        private LineRenderer _ropeRenderer;
        private Vector3 _direction;
        private float _angle;

        private void Awake()
        {
            _joint2d = GetComponent<DistanceJoint2D>();
            _ropeRenderer = GetComponent<LineRenderer>();
            _joint2d.enabled = false;
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (GameManager.instance.powerUpArray[0])
            {
                Crosshair();
                GrapplingHook();
            } 
        }

        private void Crosshair()
        {        
            if (!_createdCrosshair)
            {
                Debug.Log("Crosshair Created");
                Instantiate(_crosshair);
                _crosshair = GameObject.FindGameObjectWithTag("Crosshair");
                _createdCrosshair = true;
            }
            else if (_crosshair == null)
            {
                _crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            }

            _crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _crosshairPos.z = 0f;
            _direction = _crosshairPos - transform.position;
            _angle = Mathf.Atan2(_direction.y, _direction.x);

            if (_angle < 0f)
            {
                _angle = Mathf.PI * 2 + _angle;
            }

            SetCrosshairPos();
            _crosshair.transform.position = _crosshairPos;            
        }

        private void SetCrosshairPos()
        {
            float x = transform.position.x + _maxDistance * Mathf.Cos(_angle);
            float y = transform.position.y + _maxDistance * Mathf.Sin(_angle);

            _crosshairPos = new Vector3(x, y, 0);
        }

        // Grappling hook abilty. 
        private void GrapplingHook()
        {
            // Detects if mouse's left button is pressed down and held
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed");
                _targetPos = _crosshairPos;
                

                _hit = Physics2D.Raycast(transform.position, _targetPos - transform.position, _maxDistance, _targetLayer);
                if (_hit.collider != null && _hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    _joint2d.enabled = true;
                    _joint2d.connectedBody = _hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    _joint2d.connectedAnchor = new Vector2(0, (0 - (_hit.collider.bounds.size.y / 3)));
                    _joint2d.distance = Vector2.Distance(transform.position, _hit.transform.position);

                    Hook();
                    RopeRendering();
                    
                    _connected = true;

                    //Debug.Log("Target PosX: " + _Hit.transform.position.x);
                    //Debug.Log("Target PosY: " + (_Hit.transform.position.y - _Hit.collider.bounds.size.y / 2));
                }                
            }
            if (_connected)
            {
                Vector3 anchor = new Vector3(_hit.collider.transform.position.x,
                    (_hit.collider.transform.position.y - _hit.collider.bounds.size.y / 2), 0);
                Debug.DrawRay(transform.position, (anchor - transform.position), Color.red);
                Debug.Log("Up or Down");
                if (Input.GetKey(KeyCode.W))
                {
                    Debug.Log("Up");
                    _joint2d.distance -= _adjustDistance * Time.deltaTime;
                    
                }
                if (Input.GetKey(KeyCode.S))
                {
                    Debug.Log("Down");
                    _joint2d.distance += _adjustDistance * Time.deltaTime;
                    
                }

                RopeRendering();

                //Debug.Log("Anchor: " + (_Hit.point - new Vector2(0, _Hit.collider.transform.position.y)));
                //Debug.Log("Distance: " + _Joint2d.distance);
                //Debug.Log("DistanceInt: " + Vector2.Distance(transform.position, _Hit.transform.position));
            }

            // Detects if left mouse button is lifted
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Released");
                _joint2d.enabled = false;
                _connected = false;
                _hook.SetActive(false);
                _ropeRenderer.enabled = false;
            }
        }

        // Sets rope's starting and ending points for LineRenderer.
        private void RopeRendering()
        {
            _ropePoints[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _ropePoints[1] = new Vector3(_hit.collider.transform.position.x,
                (_hit.collider.transform.position.y - _hit.collider.bounds.size.y / 2), 0);

            Debug.Log("pos1: " + _ropePoints[0]);
            Debug.Log("pos2: " + _ropePoints[1]);

            _ropeRenderer.SetPositions(_ropePoints);
            _ropeRenderer.enabled = true;
        }

        // Creates grappling hooks hook and changes its position.
        private void Hook()
        {
            if (!_createdHook)
            {
                _hook.SetActive(true);
                Instantiate(_hook);
                _hook = GameObject.FindGameObjectWithTag("Hook");
                _createdHook = true;
            }

            _hook.SetActive(true);
            _hook.transform.position = new Vector3(_hit.collider.transform.position.x,
                    (_hit.collider.transform.position.y - _hit.collider.bounds.size.y / 2), 0);            
        }
    }
}