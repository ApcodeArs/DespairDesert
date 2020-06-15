using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : EnemyBase
{
    public override void AwakeEnemy() { }

    public override void FixedUpdateEnemy()
    {
        transform.position += Vector3.Normalize(Target.transform.position - transform.position) * Speed * Time.deltaTime;
    }
}