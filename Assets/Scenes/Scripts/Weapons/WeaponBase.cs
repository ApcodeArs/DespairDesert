using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //todo add to Scriptable Object
    public string Name;
    public float Damage;
    public Sprite DefSprite;

    private void Awake()
    {
        AwakeWeapon();
    }

    public abstract void AwakeWeapon();
}
