using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolutionController : MonoBehaviour
{
    private int _width;
    private int _height;

    private static ScreenResolutionController _instance;
    public static ScreenResolutionController Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ScreenResolutionController>();
            return _instance;
        }
    }

    //событие изменения размеров игрового окна
    public Action<int, int> WindowResolutionChanged;

    void Start()
    {
        _width = Screen.width;
        _height = Screen.height;
    }

    void Update()
    {
        if (_width != Screen.width || _height != Screen.height)
        {
            _width = Screen.width;
            _height = Screen.height;

            WindowResolutionChanged.Invoke(_width, _height);
        }
    }
}
