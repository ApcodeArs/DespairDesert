using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    [BoxGroup("Movement Params")] public float Speed = 5.0f;
    public float RotationSpeed = 2.5f;

    private Vector3 _tankMovement;
    private Vector3 _tankRotation;

    [BoxGroup("Safety Params")] public float Health;
    private float _currentHealth;
    [BoxGroup("Safety Params")] public float Protection;

    [BoxGroup("Weapons Params")] public List<GameObject> Weapons;
    private int _currentWeaponInd = 0;

    [BoxGroup("UI")] public Text Text;
    [BoxGroup("UI")] public Image WeaponImage;
    [BoxGroup("UI")] public Image HealthImage;

    void Start()
    {
        _tankRotation = transform.rotation.eulerAngles;
        _currentHealth = Health;

        SetHealthUi();
        SetWeaponUi();
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
            SetWeaponUi();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _currentWeaponInd = (_currentWeaponInd == Weapons.Count - 1) ? 0 : _currentWeaponInd + 1;
            SetWeaponUi();
        }
    }

    private void SetWeaponUi()
    {
        Text.text = Weapons[_currentWeaponInd].GetComponent<WeaponBase>().Name;
        WeaponImage.sprite = Weapons[_currentWeaponInd].GetComponent<SpriteRenderer>().sprite;
    }

    private void SetHealthUi()
    {
        HealthImage.fillAmount = _currentHealth / Health;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemyBaseScript = collision.gameObject.GetComponent<EnemyBase>();
            SetHealth(enemyBaseScript.Damage);
        }
    }

    public void SetHealth(float damage)
    {
        _currentHealth -= damage * (1 - Protection);
        
        SetHealthUi();

        if (_currentHealth <= 0)
        {
            Debug.Log("Your Die");
        }
    }
}