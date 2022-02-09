using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditUI : MonoBehaviour
{
    [Header("Road")]
    public Button CreateRoadButton;
    public Toggle PreviewModeButton;
    public GameObject RoadPrefab;

    private void Awake()
    {
        CreateRoadButton.onClick.AddListener(() => CreateRoad());
        PreviewModeButton.onValueChanged.AddListener((isPreviewMode) => SetVisibility(isPreviewMode));
    }

    /// <summary>
    /// 在相機前方產生Road物件
    /// </summary>
    private void CreateRoad()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 10));
        Road road = Instantiate(RoadPrefab, spawnPos, Quaternion.identity).GetComponentInChildren<Road>();
    }

    private void SetVisibility(bool isVisible)
    {
        foreach(Road road in SceneManager.Instance.sceneRoads)
        {
            road.SetVisibility(isVisible);
        }
    }
}
