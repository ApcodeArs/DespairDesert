using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyType2 : EnemyBase
{
    [BoxGroup("EnemyType2 Params")]
    public float CurveAmplitude;
    [BoxGroup("EnemyType2 Params")]
    public float CurveSpeed;

    private float _duration;

    protected override void FixedUpdateEnemy()
    {
        SetEnemyScaleDirection();

        _duration += Time.deltaTime * CurveSpeed;

        var vSin = new Vector3(Mathf.Sin(_duration) * CurveAmplitude, -Mathf.Sin(_duration) * CurveAmplitude, 0);
        var vLin = Direction * Speed;

        transform.position += (vSin + vLin) * Time.deltaTime;
    }
}