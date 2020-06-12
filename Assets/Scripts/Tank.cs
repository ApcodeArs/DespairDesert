using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    private Vector3 _tankRotation;
    private Vector3 _tankMovement;

    //todo add to Scriptable Object
    private const float Speed = 5.0f;
    private const float RotationSpeed = 2.5f;

    public List<GameObject> Weapons;
    private int _currentWeaponInd = 0;

    public Text Text;
    public Image WeaponImage;

    void Start()
    {
        _tankRotation = transform.rotation.eulerAngles;

        SetUi();
    }

    void Update()
    {
        MoveController();
        WeaponController();
    }

    private void MoveController()
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

    private void WeaponController()
    {
        //fire
        if (Input.GetKeyDown(KeyCode.X))
        {
            var shell = Instantiate(Weapons[_currentWeaponInd]);
            shell.transform.position = transform.position;
            shell.GetComponent<WeaponBase>().Init(gameObject);
        }

        //switch weapon
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentWeaponInd = (_currentWeaponInd == 0) ? Weapons.Count - 1 : _currentWeaponInd - 1;
            SetUi();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _currentWeaponInd = (_currentWeaponInd == Weapons.Count - 1) ? 0 : _currentWeaponInd + 1;
            SetUi();
        }
    }

    private void SetUi()
    {
        Text.text = Weapons[_currentWeaponInd].GetComponent<WeaponBase>().Name;
        WeaponImage.sprite = Weapons[_currentWeaponInd].GetComponent<SpriteRenderer>().sprite;
    }
}