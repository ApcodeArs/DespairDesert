using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemiesSpawnHelper
{
    private const int HindranceXCapacity = 5;
    private const int HindranceYCapacity = 4;

    private static List<Vector2> _hindranceSpawnPositions;

    public static Vector2 GetRandomSpawnPosition()
    {
        if (_hindranceSpawnPositions == null)
            _hindranceSpawnPositions = GenerateSpawnPositions();

        return _hindranceSpawnPositions[Random.Range(0, _hindranceSpawnPositions.Count)];
    }

    private static List<Vector2> GenerateSpawnPositions()
    {
        var hindranceSpawnPositions = new List<Vector2>();

        var xStep = 1f / (HindranceXCapacity - 1);
        var yStep = 1f / (HindranceYCapacity - 1);

        for (var i = 0; i < HindranceYCapacity; i++)
            for (var j = 0; j < HindranceXCapacity; j++)
            {
                if ((i == 0 || i == HindranceYCapacity - 1) ||
                    (j == 0 || j == HindranceXCapacity - 1))
                {
                    hindranceSpawnPositions.Add(new Vector2(j * xStep, i * yStep));
                }
            }

        return hindranceSpawnPositions;
    }
}
