using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    //todo add to Scriptable Object

    [BoxGroup("StaticEnemyParams")]
    public const int EnemiesCount = 10;
    [BoxGroup("StaticEnemyParams")]
    public const int SpawnDelay = 1; //sec

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

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Collider.enabled = false;

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
}
