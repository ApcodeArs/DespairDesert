using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyType2 : EnemyBase
{
    public override void AwakeEnemy() { }

    [BoxGroup("EnemyType2")]
    public float CurveAmplitude;
    [BoxGroup("EnemyType2")]
    public float CurveSpeed;

    private float _duration;

    public override void FixedUpdateEnemy()
    {
        _duration += Time.deltaTime * CurveSpeed;

        var vSin = new Vector3(Mathf.Sin(_duration) * CurveAmplitude, -Mathf.Sin(_duration) * CurveAmplitude, 0);
        var vLin = (Target.transform.position - transform.position) * Speed;

        transform.position += (vSin + vLin) * Time.deltaTime;
    }
}
