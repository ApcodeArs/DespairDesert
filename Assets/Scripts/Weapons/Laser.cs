using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Laser : WeaponBase
{
    [BoxGroup("Laser")]
    public float Force = 10f;

    public Rigidbody2D Rigidbody;
    public Collider2D Collider;

    public override void AwakeWeapon() { }

    public override void Init(GameObject parent)
    {
        transform.rotation = parent.transform.rotation;
        Rigidbody.AddForce(Force * transform.up, ForceMode2D.Impulse);

        //todo improve
        DOVirtual.DelayedCall(0.2f, () => Collider.isTrigger = false);
    }

    public override void FixedUpdateWeapon() { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
