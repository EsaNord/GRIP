using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private float _cameraMovementX = 17.75f;
        [SerializeField]
        private float _cameraMovementY = 10f;
        [SerializeField]
        private GameObject _camera;        

        private float _cameraPositionX;
        private float _cameraPositionY;        

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _camera = this.gameObject;
            Camera.main.aspect = 4f / 3f;

            _cameraPositionX = _camera.transform.position.x;
            _cameraPositionY = _camera.transform.position.y;

            _cameraMovementY = 2f * Camera.main.orthographicSize;
            _cameraMovementX = _cameraMovementY * Camera.main.aspect;

            Debug.Log("Camera height: " + _cameraMovementY);
            Debug.Log("Camera width:  " + (_cameraMovementY * Camera.main.aspect));
        }

        // Update is called once per frame
        void Update()
        {  
            if (_player == null)
            {
                Debug.Log("SEARCHING PLAYER....");
                _player = GameObject.FindGameObjectWithTag("Player");
            }

            // Horizontal movement
            if (_player.transform.position.x > _cameraPositionX + (_cameraMovementX / 2))
            {
                _cameraPositionX += _cameraMovementX;            
            }
            if (_player.transform.position.x < _cameraPositionX - (_cameraMovementX / 2))
            {
                _cameraPositionX -= _cameraMovementX;            
            }

            // Vertical movement            
            if (_player.transform.position.y > _cameraPositionY + (_cameraMovementY / 2))
            {
                _cameraPositionY += _cameraMovementY;
            }
            if (_player.transform.position.y < _cameraPositionY - (_cameraMovementY / 2))
            {
                _cameraPositionY -= _cameraMovementY;
            }

            // Camera position update
            _camera.transform.position = new Vector3(_cameraPositionX, _cameraPositionY, -10);            
        }        
    }
}