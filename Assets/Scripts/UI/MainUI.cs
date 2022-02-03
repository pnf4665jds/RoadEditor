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
    /// �b�۾��e�貣��Bezier����
    /// </summary>
    private void CreateBezier()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 10));
        BezierCurve curve = Instantiate(BezierPrefab, spawnPos, Quaternion.identity).GetComponentInChildren<BezierCurve>();
        //RoadEditManager.Instance.SetCurrentBezier(curve);
    }
}
