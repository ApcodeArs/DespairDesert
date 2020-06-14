using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //todo add to Scriptable Object

    [BoxGroup("StaticEnemyParams")]
    public const int EnemiesCount = 10;
    [BoxGroup("StaticEnemyParams")]
    public const float SpawnDelay = 2f; //sec

    [BoxGroup("BaseEnemyParams")]
    public string Name;

    [BoxGroup("BaseEnemyParams")]
    public float Health;
    [BoxGroup("BaseEnemyParams")]
    public float Protection;

    [BoxGroup("BaseEnemyParams")]
    public float Speed;
    [BoxGroup("BaseEnemyParams")]
    public float Damage;

    protected GameObject Target;

    private const float ScreenIndentation = 0.05f;

    protected Collider2D Collider;

    protected static DespairDesertController DespairDesertController;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Collider.enabled = false;

        if (DespairDesertController == null)
            DespairDesertController = FindObjectOfType<DespairDesertController>();

        AwakeEnemy();
    }

    private void FixedUpdate()
    {
        FixedUpdateEnemy();

        var hindrancePosition = Camera.main.WorldToViewportPoint(transform.position);

        if ((hindrancePosition.x > (0 + ScreenIndentation) && hindrancePosition.x < (1 - ScreenIndentation)) &&
            (hindrancePosition.y > (0 + ScreenIndentation) && hindrancePosition.y < (1 - ScreenIndentation)))
        {
            Collider.enabled = true;
        }
    }

    public abstract void AwakeEnemy();
    public abstract void FixedUpdateEnemy();

    public virtual void Init(GameObject target)
    {
        Target = target;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            var weaponBaseScript = collision.gameObject.GetComponent<WeaponBase>();

            Health -= weaponBaseScript.Damage * (1 - Protection);

            if (Health <= 0)
            {
                DespairDesertController.Enemies.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
