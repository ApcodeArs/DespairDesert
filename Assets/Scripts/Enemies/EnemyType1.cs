﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : BaseEnemy
{
    public override void AwakeEnemy()
    {

    }

    public override void FixedUpdateEnemy()
    {
        transform.position += (Target.transform.position - transform.position) * Speed * Time.deltaTime;
    }

}
