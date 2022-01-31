using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Bezier")]
    public Button CreateBezierButton;
    public GameObject BezierPrefab;

    private void Awake()
    {
        CreateBezierButton.onClick.AddListener(() => CreateBezier());
    }

    /// <summary>
    /// 在相機前方產生Bezier物件
    /// </summary>
    private void CreateBezier()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 10));
        Instantiate(BezierPrefab, spawnPos, Quaternion.identity);
    }
}
