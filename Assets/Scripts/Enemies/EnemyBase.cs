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

    //[BoxGroup("BaseEnemy Params")]
    //public string Name;

    [BoxGroup("BaseEnemy Params")]
    public float Health;
    public float _health;
    [BoxGroup("BaseEnemy Params")]
    public float Protection;

    [BoxGroup("BaseEnemy Params")]
    public float Speed;
    [BoxGroup("BaseEnemy Params")]
    public float Damage;

    protected Collider2D Collider;

    protected Vector3 Direction;
    protected Vector3 LocalScale;

    protected static DespairDesertController DespairDesertController;
    protected static Tank Target;
    protected static Camera MainCamera;

    private const float ScreenIndentation = 0.05f;

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Collider.enabled = false;

        _health = Health;

        if (DespairDesertController == null)
            DespairDesertController = FindObjectOfType<DespairDesertController>();

        if (Target == null)
            Target = FindObjectOfType<Tank>();

        if (MainCamera == null)
            MainCamera = Camera.main;

        //AwakeEnemy();
    }

    private void FixedUpdate()
    {
        Direction = Vector3.Normalize(Target.transform.position - transform.position);

        SetColliderActivity();

        FixedUpdateEnemy();
    }

    public void SetHealth(float weaponDamage)
    {
        _health -= weaponDamage * (1 - Protection);

        if (_health <= 0)
        {
            DespairDesertController.SetPoints(Health);

            DespairDesertController.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    protected void SetEnemyScaleDirection()
    {
        LocalScale = transform.localScale;
        LocalScale.x *= ((Direction.x > 0.0f && LocalScale.x > 0) || (Direction.x < 0.0f && LocalScale.x < 0)) ? 1 : -1;
        transform.localScale = LocalScale;
    }

    private void SetColliderActivity()
    {
        if (Collider.enabled)
            return;

        var hindrancePosition = MainCamera.WorldToViewportPoint(transform.position);

        Collider.enabled =
            ((hindrancePosition.x > (0 + ScreenIndentation) && hindrancePosition.x < (1 - ScreenIndentation)) &&
             (hindrancePosition.y > (0 + ScreenIndentation) && hindrancePosition.y < (1 - ScreenIndentation)));
    }

    //public abstract void AwakeEnemy();

    protected abstract void FixedUpdateEnemy();
}