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
        private float _CameraMovementX = 17.75f;
        [SerializeField]
        private float _CameraMovementY = 10f;
        [SerializeField]
        private GameObject _Camera;        

        private float _CameraPositionX;
        private float _CameraPositionY;        

        private void Start()
        {
            _Player = GameObject.FindGameObjectWithTag("Player");            

            _CameraPositionX = _Camera.transform.position.x;
            _CameraPositionY = _Camera.transform.position.y;            
        }

        // Update is called once per frame
        void Update()
        {  
            if (_Player == null)
            {
                Debug.Log("SEARCHING PLAYER....");
                _Player = GameObject.FindGameObjectWithTag("Player");
            }

            // Horizontal movement
            if (_Player.transform.position.x > _CameraPositionX + 9f)
            {
                _CameraPositionX += _CameraMovementX;            
            }
            if (_Player.transform.position.x < _CameraPositionX - 9f)
            {
                _CameraPositionX -= _CameraMovementX;            
            }

            // Vertical movement            
            if (_Player.transform.position.y > _CameraPositionY + 5f)
            {
                _CameraPositionY += _CameraMovementY;
            }
            if (_Player.transform.position.y < _CameraPositionY - 5f)
            {
                _CameraPositionY -= _CameraMovementY;
            }

            // Camera position update
            _Camera.transform.position = new Vector3(_CameraPositionX, _CameraPositionY, -10);            
        }        
    }
}