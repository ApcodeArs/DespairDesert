using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //todo add to Scriptable Object
    public string Name;
    public float Damage;
    
    private void Awake()
    {
        AwakeWeapon();
    }

    private void FixedUpdate()
    {
        FixedUpdateWeapon();
    }

    public abstract void AwakeWeapon();

    public abstract void Init(GameObject parent);

    public abstract void FixedUpdateWeapon();
}
