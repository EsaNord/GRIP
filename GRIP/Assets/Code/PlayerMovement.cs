using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float jumpForce = 15;
	
	// Update is called once per frame
	void Update () {
        Move();
        Debug.Log("HERE");
	}

    private void Move()
    {
        Debug.Log("MOVE");
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            Debug.Log("LEFT");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            Debug.Log("RIGHT");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            Debug.Log("JUMP");
        }
    }
}
