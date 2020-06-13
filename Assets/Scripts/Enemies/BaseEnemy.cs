using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    //todo add to Scriptable Object

    [BoxGroup("BaseEnemy")]
    public const int EnemiesCount = 10;
   
    [BoxGroup("BaseEnemy")]
    public string Name;

    [BoxGroup("BaseEnemy")]
    public float Health;
    [BoxGroup("BaseEnemy")]
    public float Protection;

    [BoxGroup("BaseEnemy")]
    public float Speed;
    [BoxGroup("BaseEnemy")]
    public float Damage;
    
    private void Awake()
    {
        AwakeEnemy();
    }

    private void FixedUpdate()
    {
        FixedUpdateEnemy();
    }

    public abstract void AwakeEnemy();

    //public abstract void Init(GameObject parent);

    public abstract void FixedUpdateEnemy();
}
