using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float MoveSpeed = 1.5f;
    public float ZoomSpeed = 1.5f;
    static Vector3 Thisposition;
    static Quaternion ThisRotation;

    public static Vector3 StartPosition
    {
        get
        {
            return Thisposition;
        }
    }
    public static Quaternion StartRotation
    {
        get
        {
            return ThisRotation;
        }
    }

    private void Start()
    {
        Thisposition = transform.position;
        ThisRotation = transform.rotation;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, 1, 0) * MoveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -1, 0) * MoveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.forward * ZoomSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.back * ZoomSpeed * Time.deltaTime);
        }
    }

}


