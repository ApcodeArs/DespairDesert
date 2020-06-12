﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DespairDesertController : MonoBehaviour
{
    [SerializeField, BoxGroup("Borders")] public GameObject LeftBorder;
    [SerializeField, BoxGroup("Borders")] public GameObject RightBorder;
    [SerializeField, BoxGroup("Borders")] public GameObject TopBorder;
    [SerializeField, BoxGroup("Borders")] public GameObject BottomBorder;


    void Awake()
    {
        SetBorders(Screen.width, Screen.height);

        ScreenResolutionController.Instance.WindowResolutionChanged += (width, height) =>
        {
            SetBorders(width, height);
        };
    }

    private void SetBorders(int width, int height, float borderThickness = 5f, float borderScaleFactor = 2f)
    {
        var cam = Camera.main;
        var screenSize = cam.ScreenToWorldPoint(new Vector3(width, height));

        #region Set Borders Size
        LeftBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(borderThickness, screenSize.y * 2 * borderScaleFactor);
        RightBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(borderThickness, screenSize.y * 2 * borderScaleFactor);
        TopBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(screenSize.x * 2 * borderScaleFactor, borderThickness);
        BottomBorder.GetComponent<RectTransform>().sizeDelta =
            new Vector2(screenSize.x * 2 * borderScaleFactor, borderThickness);
        #endregion

        #region Set Colliders Size
        LeftBorder.GetComponent<BoxCollider2D>().size = LeftBorder.GetComponent<RectTransform>().sizeDelta;
        RightBorder.GetComponent<BoxCollider2D>().size = RightBorder.GetComponent<RectTransform>().sizeDelta;
        TopBorder.GetComponent<BoxCollider2D>().size = TopBorder.GetComponent<RectTransform>().sizeDelta;
        BottomBorder.GetComponent<BoxCollider2D>().size = BottomBorder.GetComponent<RectTransform>().sizeDelta;
        #endregion

        #region Set Borders Positions
        LeftBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(0, 0.5f))
                                        + new Vector3(-LeftBorder.GetComponent<RectTransform>().rect.width / 2,
                                            0.0f);

        RightBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(1, 0.5f))
                                         + new Vector3(RightBorder.GetComponent<RectTransform>().rect.width / 2,
                                             0.0f);

        TopBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f))
                                       + new Vector3(0.0f, TopBorder.GetComponent<RectTransform>().rect.height / 2);

        BottomBorder.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0))
                                          + new Vector3(0.0f,
                                              -BottomBorder.GetComponent<RectTransform>().rect.height / 2);
        #endregion
    }
}
