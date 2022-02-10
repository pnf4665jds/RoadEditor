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
        PreviewModeButton.onValueChanged.AddListener((isPreviewMode) => SetPreviewMode(isPreviewMode));
    }

    /// <summary>
    /// 在相機前方產生Road物件
    /// </summary>
    private void CreateRoad()
    {
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 10));
        Road road = Instantiate(RoadPrefab, spawnPos, Quaternion.identity).GetComponentInChildren<Road>();
        road.OnNodeInit();
    }

    private void SetPreviewMode(bool isPreviewMode)
    {
        if (isPreviewMode)
        {
            EditManager.Instance.isPreviewMode = true;
            foreach (INode node in SceneManager.Instance.sceneNodes)
            {
                node.OnPreviewMode();
            }
        }
        else
        {
            EditManager.Instance.isPreviewMode = false;
            foreach (INode node in SceneManager.Instance.sceneNodes)
            {
                node.OnEditMode();
            }
        }
    }
}
