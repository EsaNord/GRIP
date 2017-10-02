using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GRIP
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject _Player;
        [SerializeField]
        private float _cameraMovement = 17.75f;
        [SerializeField]
        private GameObject _Camera;
        [SerializeField]
        private float _MovementBuffer = 1f;

        private float _CameraPositionX;
        private float _PlayerPosX;
        private bool _JustMoved;

        private void Awake()
        {
            _CameraPositionX = _Camera.transform.position.x;
            _JustMoved = false;
        }

        // Update is called once per frame
        void Update()
        {                        
            if (_Player.transform.position.x > _CameraPositionX + 9f && !_JustMoved)
            {
                _CameraPositionX += _cameraMovement;
                _JustMoved = true;
                _PlayerPosX = _Player.transform.position.x;
            }
            if (_Player.transform.position.x < _CameraPositionX - 9f && !_JustMoved)
            {
                _CameraPositionX -= _cameraMovement;
                _JustMoved = true;
                _PlayerPosX = _Player.transform.position.x;
            }
            
            _Camera.transform.position = new Vector3(_CameraPositionX, 0, -10);
            CameraMovementBuffer();
        }

        private void CameraMovementBuffer()
        {
            if (_PlayerPosX + _MovementBuffer < _Player.transform.position.x ||
                _PlayerPosX - _MovementBuffer > _Player.transform.position.x)
            {
                Debug.Log("CHECK");
                _JustMoved = false;
            }
        }
    }
}