using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    protected override void FixedUpdateEnemy()
    {
        SetEnemyScaleDirection();

        transform.position += Direction * Speed * Time.deltaTime;
    }
}