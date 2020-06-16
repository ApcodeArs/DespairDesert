using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EasterEgg : MonoBehaviour
{
    private readonly TimeSpan _startSiestaHour = new TimeSpan(14,0,0); 
    private readonly TimeSpan _endSiestaHour = new TimeSpan(16, 0, 0);

    private const float EntryThreshold = 0.5f;

    public SpriteRenderer SombreroPrefab;

    void Awake()
    {
        if (Random.value > EntryThreshold)
            return;

        //add TimeSpan(15, 0, 0) to check
        var currentTime = DateTime.Now.TimeOfDay;

        if (currentTime > _startSiestaHour && currentTime < _endSiestaHour)
        {
            Debug.Log("Siesta!!!");
            Instantiate(SombreroPrefab, GameObject.Find("Tank").transform);
        }
    }
}