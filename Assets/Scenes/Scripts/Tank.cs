using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Vector3 _tankRotation;
    private Vector3 _tankMovement;

    //todo add to Scriptable Object
    private const float Speed = 5.0f;
    private const float RotationSpeed = 2.5f;

    void Start()
    {
        _tankRotation = transform.rotation.eulerAngles;
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        #region Rotation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _tankRotation.z += RotationSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _tankRotation.z -= RotationSpeed;
        }

        transform.rotation = Quaternion.Euler(_tankRotation);
        #endregion

        #region Shifting
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.rotation * Vector3.up * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += transform.rotation * Vector3.down * Time.deltaTime * Speed;
        }
        #endregion
    }
}
