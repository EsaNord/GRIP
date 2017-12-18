using UnityEngine;

namespace GRIP
{
    public class PlayerAbilities : MonoBehaviour {

        [SerializeField]
        private float _maxDistance = 10f;
        [SerializeField]
        private LayerMask _targetLayer;
        [SerializeField]
        private LayerMask _obstacleLayer;
        [SerializeField]
        private float _adjustDistance = 1f;
        [SerializeField]
        private GameObject _hook;
        [SerializeField]
        private GameObject _crosshair;
        [SerializeField]
        private GameObject _handStart;
        [SerializeField]
        private float _shoulderPosY = 0.35f;        

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
        private float _distance;
        private bool _blocked;
        private bool _createdHandTile = false;
        private Animator _playerAnimator;
        private SpriteRenderer _playerRenderer;

        public bool RopeConnected
        {
            get { return _connected; }
        }

        public void RopeMove(float speed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);                             
                _playerRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime); 
                _playerRenderer.flipX = false;
            }
        }

        private void Awake()
        {
            _playerAnimator = GetComponent<Animator>();
            _playerRenderer = GetComponent<SpriteRenderer>();
            _joint2d = GetComponent<DistanceJoint2D>();
            _ropeRenderer = GetComponent<LineRenderer>();
            _joint2d.enabled = false;
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
            _distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
            if (_distance > _maxDistance)
            {
                _distance = _maxDistance;
            }

            float x = transform.position.x + _distance * Mathf.Cos(_angle);
            float y = transform.position.y + _distance * Mathf.Sin(_angle);

            _crosshairPos = new Vector3(x, y, 0);
        }

        // Grappling hook abilty. 
        private void GrapplingHook()
        {
            Vector3 playerPos = transform.position;
            playerPos.y += _shoulderPosY;

            // Detects if mouse's left button is pressed down and held
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Pressed");
                _targetPos = _crosshairPos;                

                _hit = Physics2D.Raycast(playerPos, _targetPos - playerPos, _distance, _targetLayer);
                RaycastHit2D _obstacle = Physics2D.Raycast(playerPos, _targetPos - playerPos, _distance, _obstacleLayer);                
                
                if (_obstacle.collider != null)
                {
                    Debug.Log("Hitpos?: " + _obstacle.collider.transform.position);
                    Debug.Log("Blocked");
                    _blocked = true;
                }
                else
                {
                    Debug.Log("Clear");
                    _blocked = false;
                }

                if (_hit.collider != null && _hit.collider.gameObject.GetComponent<Rigidbody2D>() != null && !_blocked)
                {
                    _joint2d.enabled = true;
                    _joint2d.connectedBody = _hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    _joint2d.connectedAnchor = new Vector2(0, (0 - (_hit.collider.bounds.size.y / 4)));
                    _joint2d.distance = Vector2.Distance(playerPos, _hit.transform.position);

                    Hook();
                    RopeRendering();
                    
                    _connected = true;
                    SFXPlayer.Instance.Play(Sound.HookHit);
                }                
            }
            if (_connected)
            {
                Vector3 anchor = new Vector3(_hit.collider.transform.position.x,
                    (_hit.collider.transform.position.y - _hit.collider.bounds.size.y / 4), 0);
                Debug.DrawRay(transform.position, (anchor - playerPos), Color.red);
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

                _playerAnimator.SetBool("InRope", true);
                HandStart(playerPos, anchor);
                RopeRendering();
                CheckRope(anchor, playerPos);                
            }

            // Detects if left mouse button is lifted
            if (Input.GetMouseButtonUp(0) || !_connected)
            {
                Debug.Log("Released");
                _joint2d.enabled = false;
                _connected = false;
                _hook.SetActive(false);
                _handStart.SetActive(false);
                _ropeRenderer.enabled = false;
                _playerAnimator.SetBool("InRope", false);
            }            
        }

        private void CheckRope(Vector3 target, Vector3 playerPos) 
        {            
            RaycastHit2D broken = Physics2D.Raycast(playerPos, _targetPos - playerPos,
                Vector2.Distance(target, playerPos), _obstacleLayer);
            if (broken.collider != null)
            {
                _connected = false;
            }
            else if (Vector2.Distance(target, playerPos) > _maxDistance)
            {
                _connected = false;
            }
        }

        // Sets rope's starting and ending points for LineRenderer.
        private void RopeRendering()
        {
            _ropePoints[0] = new Vector3(transform.position.x, transform.position.y + _shoulderPosY, transform.position.z);
            _ropePoints[1] = new Vector3(_hit.collider.transform.position.x,
                (_hit.collider.transform.position.y - _hit.collider.bounds.size.y / 4), 0);
            
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
                    (_hit.collider.transform.position.y - _hit.collider.bounds.size.y / 4), 0);            
        }

        private void HandStart(Vector3 playerPos, Vector3 target)
        {
            if (!_createdHandTile)
            {
                _handStart.SetActive(true);                
                _handStart = Instantiate(_handStart);
                _createdHandTile = true;
            }

            _handStart.SetActive(true);
            _handStart.transform.position = playerPos;

            Vector3 rotationTarget = target;
            rotationTarget.z = 0f;
            rotationTarget.x = rotationTarget.x - playerPos.x;
            rotationTarget.y = rotationTarget.y - playerPos.y;

            float angle = Mathf.Atan2(rotationTarget.y, rotationTarget.x) * Mathf.Rad2Deg;
            _handStart.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}