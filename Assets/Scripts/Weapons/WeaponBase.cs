﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [BoxGroup("WeaponBase Params")]
    public string Name;
    [BoxGroup("WeaponBase Params")]
    public float Damage;

    protected static DespairDesertController DespairDesertController;

    private void Awake()
    {
        if (DespairDesertController == null)
            DespairDesertController = FindObjectOfType<DespairDesertController>();

        AwakeWeapon();
    }

    private void FixedUpdate()
    {
        FixedUpdateWeapon();
    }

    public abstract void AwakeWeapon();

    public abstract void FixedUpdateWeapon();

    public abstract void Init(GameObject parent);
}
