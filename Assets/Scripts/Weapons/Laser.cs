using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Laser : WeaponBase
{
    [BoxGroup("Laser Params")]
    public float Force = 10f;

    public Collider2D Collider;
    public Rigidbody2D Rigidbody;

    public override void Init(GameObject parent)
    {
        transform.rotation = parent.transform.rotation;
        Rigidbody.AddForce(Force * transform.up, ForceMode2D.Impulse);

        Physics2D.IgnoreCollision(Collider, parent.GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var enemyScript = collision.gameObject.GetComponent<EnemyBase>();
            enemyScript.SetHealth(Damage);
        }

        Destroy(gameObject);
    }
}
