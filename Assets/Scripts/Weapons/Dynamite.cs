using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Dynamite : WeaponBase
{
    [BoxGroup("Dynamite")]
    public float LesionRadius;
    [BoxGroup("Dynamite")]
    public float TimerTime = 5.0f; //sec

    private Coroutine _dynamiteCoroutine;

    protected static DespairDesertController DespairDesertController;

    public override void AwakeWeapon()
    {
        if (DespairDesertController == null)
            DespairDesertController = FindObjectOfType<DespairDesertController>();
    }

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

        //todo improve
        var removedList = new List<int>();

        foreach (var enemy in DespairDesertController.Enemies)
        {
            //todo add interaction with tank
            //todo add hp calculation

            if (Vector3.Distance(transform.position, enemy.transform.position) < LesionRadius)
            {
                removedList.Add(DespairDesertController.Enemies.IndexOf(enemy));
            }
        }

        foreach (var removedElem in removedList)
        {
            var t = DespairDesertController.Enemies.ElementAt(removedElem);
            DespairDesertController.Enemies.Remove(t);
            Destroy(t);
        }

        Destroy(gameObject);
    }
}
