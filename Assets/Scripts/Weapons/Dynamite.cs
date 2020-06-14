using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Dynamite : WeaponBase
{
    [BoxGroup("Dynamite")]
    public float LesionRadius;
    [BoxGroup("Dynamite")]
    public float TimerTime = 5.0f; //sec

    private Coroutine _dynamiteCoroutine;

    public ParticleSystem Smoke;

    public override void AwakeWeapon() { }

    public override void Init(GameObject parent)
    {
        transform.rotation = parent.transform.rotation;
        transform.position = parent.transform.position + new Vector3(0.0f, 0.0f, 0.1f);

        _dynamiteCoroutine = StartCoroutine(TimerCoroutine());
    }

    public override void FixedUpdateWeapon() { }

    private IEnumerator TimerCoroutine()
    {
        var tempTimerTime = TimerTime;

        while (tempTimerTime > 0)
        {
            tempTimerTime -= Time.deltaTime;
            yield return null;
        }

        //todo add interaction with tank

        var smoke = Instantiate(Smoke);
        smoke.transform.position = transform.position;

        for (var i = DespairDesertController.Enemies.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(transform.position, DespairDesertController.Enemies[i].transform.position) < LesionRadius)
            {
                DespairDesertController.Enemies[i].GetComponent<EnemyBase>().SetHealth(Damage);
            }
        }

        Destroy(gameObject);
    }
}
