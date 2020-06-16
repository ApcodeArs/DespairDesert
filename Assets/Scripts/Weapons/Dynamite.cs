using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Dynamite : WeaponBase
{
    [BoxGroup("Dynamite Params")]
    public float LesionRadius;
    [BoxGroup("Dynamite Params")]
    public float TimerTime = 5.0f; //sec

    public ParticleSystem Smoke;

    private Coroutine _dynamiteCoroutine;

    private GameObject _parent;

    public override void Init(GameObject parent)
    {
        _parent = parent;

        transform.rotation = _parent.transform.rotation;
        transform.position = _parent.transform.position + new Vector3(0.0f, 0.0f, 0.1f);

        _dynamiteCoroutine = StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        var tempTimerTime = TimerTime;

        while (tempTimerTime > 0)
        {
            tempTimerTime -= Time.deltaTime;
            yield return null;
        }

        BangEffects();

        TankInteraction();
        EnemyInteraction();

        Destroy(gameObject);
    }

    private bool IsInAffectedArea(GameObject target) => Vector3.Distance(transform.position, target.transform.position) < LesionRadius;

    private void TankInteraction()
    {
        if (IsInAffectedArea(_parent))
        {
            _parent.GetComponent<Tank>().SetHealth(Damage);
        }
    }

    private void EnemyInteraction()
    {
        for (var i = DespairDesertController.Enemies.Count - 1; i >= 0; i--)
        {
            if (IsInAffectedArea(DespairDesertController.Enemies[i]))
            {
                DespairDesertController.Enemies[i].GetComponent<EnemyBase>().SetHealth(Damage);
            }
        }
    }

    private void BangEffects()
    {
        var smoke = Instantiate(Smoke);
        smoke.transform.position = transform.position;
    }
}
