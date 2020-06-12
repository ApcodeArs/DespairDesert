using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BackgroundGeneration : MonoBehaviour
{
    [BoxGroup("Size"), OnValueChanged("SetTexture")]
    public int Width = 256;
    [BoxGroup("Size"), OnValueChanged("SetTexture")]
    public int Height = 256;

    [OnValueChanged("SetTexture")]
    public float Scale = 30f;

    [BoxGroup("Offset"), OnValueChanged("SetTexture")]
    public float OffsetX = 10f;
    [BoxGroup("Offset"), OnValueChanged("SetTexture")]
    public float OffsetY = 10f;

    public Renderer Renderer;

    public List<Color> Colors = new List<Color>()
        {
            new Color(244f/255, 229f/255,188f/255),
            new Color(230f/255, 194f/255,118f/255),
            new Color(246f/255, 211f/255,117f/255),
            new Color(201f/255, 181f/255,163f/255),
            new Color(217f/255, 186f/255,139f/255),
            new Color(189f/255, 157f/255,118f/255),
        };

    [Button("Generate", ButtonSizes.Medium)]
    private void Generate()
    {
        OffsetX = Random.Range(0, 10000);
        OffsetY = Random.Range(0, 10000);

        SetTexture();
    }

    public void Start()
    {
        Generate();
    }

    public void SetTexture()
    {
        Renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        var texture = new Texture2D(Width, Height);

        for (var i = 0; i < Width; i++)
        {
            for (var j = 0; j < Height; j++)
            {
                texture.SetPixel(i, j, CalculateColor(i, j));
            }
        }

        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y)
    {
        var xCoord = (float)x / Width * Scale + OffsetX;
        var yCoord = (float)y / Height * Scale + OffsetY;

        var value = Mathf.PerlinNoise(xCoord, yCoord);

        if (value < 0)
            value = 0;
        else if (value > 1)
            value = 1;

        var ind = Mathf.RoundToInt((value) * (Colors.Count - 1));

        return Colors[ind];
    }
}
