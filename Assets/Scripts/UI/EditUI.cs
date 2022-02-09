using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditUI : MonoBehaviour
{
    [Header("Road")]
    public Button CreateRoadButton;
    public Button LinkNodeButton;
    public GameObject RoadPrefab;

    private void Awake()
    {
        CreateRoadButton.onClick.AddListener(() => CreateRoad());
    }

    /// <summary>
    /// �b�۾��e�貣��Road����
    /// </summary>
    private void CreateRoad()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 10));
        Road road = Instantiate(RoadPrefab, spawnPos, Quaternion.identity).GetComponentInChildren<Road>();
    }
}
