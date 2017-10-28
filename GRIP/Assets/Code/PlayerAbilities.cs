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
        [SerializeField]
        private GameObject _hook;
        [SerializeField]
        private GameObject _crosshair;

        private DistanceJoint2D _Joint2d;
        private Vector3 _TargetPos;
        private Vector3 _crosshairPos;
        private RaycastHit2D _Hit;
        private bool _Connected;
        private bool _createdCrosshair = false;
        private bool _createdHook = false;
        private Vector3[] _ropePoints = new Vector3[2];
        private LineRenderer _ropeRenderer;

        private void Awake()
        {
            _Joint2d = GetComponent<DistanceJoint2D>();
            _ropeRenderer = GetComponent<LineRenderer>();
            _Joint2d.enabled = false;
        }

        // Update is called once per frame
        void Update() {
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
            _crosshair.transform.position = _crosshairPos;            
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

                    Hook();
                    RopeRendering();
                    
                    _Connected = true;

                    //Debug.Log("Target PosX: " + _Hit.transform.position.x);
                    //Debug.Log("Target PosY: " + (_Hit.transform.position.y - _Hit.collider.bounds.size.y / 2));
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

                RopeRendering();

                //Debug.Log("Anchor: " + (_Hit.point - new Vector2(0, _Hit.collider.transform.position.y)));
                //Debug.Log("Distance: " + _Joint2d.distance);
                //Debug.Log("DistanceInt: " + Vector2.Distance(transform.position, _Hit.transform.position));
            }

            // Detects if left mouse button is lifted
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Released");
                _Joint2d.enabled = false;
                _Connected = false;
                _hook.SetActive(false);
                _ropeRenderer.enabled = false;
            }
        }

        // Sets rope's starting and ending points for LineRenderer.
        private void RopeRendering()
        {
            _ropePoints[0] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _ropePoints[1] = new Vector3(_Hit.collider.transform.position.x,
                (_Hit.collider.transform.position.y - _Hit.collider.bounds.size.y / 2), 0);

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
            _hook.transform.position = new Vector3(_Hit.collider.transform.position.x,
                    (_Hit.collider.transform.position.y - _Hit.collider.bounds.size.y / 2), 0);            
        }
    }
}