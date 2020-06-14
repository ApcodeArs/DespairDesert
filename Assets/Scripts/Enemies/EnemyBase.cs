using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [BoxGroup("Static Enemy Params")]
    public const int EnemiesCount = 10;
    [BoxGroup("Static Enemy Params")]
    public const float SpawnDelay = 1.5f; //sec

    [BoxGroup("BaseEnemy Params")]
    public string Name;

    [BoxGroup("BaseEnemy Params")]
    public float Health;
    [BoxGroup("BaseEnemy Params")]
    public float Protection;

    [BoxGroup("BaseEnemy Params")]
    public float Speed;
    [BoxGroup("BaseEnemy Params")]
    public float Damage;

    private const float ScreenIndentation = 0.05f;

    protected Collider2D Collider;

    protected static DespairDesertController DespairDesertController;
    protected static Tank Target;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Collider.enabled = false;

        if (DespairDesertController == null)
            DespairDesertController = FindObjectOfType<DespairDesertController>();

        if (Target == null)
            Target = FindObjectOfType<Tank>();

        AwakeEnemy();
    }

    private void FixedUpdate()
    {
        SetColliderActivity();
        FixedUpdateEnemy();
    }

    public void SetHealth(float weaponDamage)
    {
        Health -= weaponDamage * (1 - Protection);

        if (Health <= 0)
        {
            DespairDesertController.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void SetColliderActivity()
    {
        var hindrancePosition = Camera.main.WorldToViewportPoint(transform.position);

        if ((hindrancePosition.x > (0 + ScreenIndentation) && hindrancePosition.x < (1 - ScreenIndentation)) &&
            (hindrancePosition.y > (0 + ScreenIndentation) && hindrancePosition.y < (1 - ScreenIndentation)))
        {
            Collider.enabled = true;
        }
    }

    public abstract void AwakeEnemy();

    public abstract void FixedUpdateEnemy();
}
