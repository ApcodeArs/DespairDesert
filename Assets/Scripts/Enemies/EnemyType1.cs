using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    //public override void AwakeEnemy() { }

    protected override void FixedUpdateEnemy()
    {
        SetEnemyScaleDirection();

        transform.position += Direction * Speed * Time.deltaTime;
    }
}