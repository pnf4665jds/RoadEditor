using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    // ==inRoad==>(Node)==outRoad==>

    // ��V�¦V�o��node��road
    public SceneRoad inRoad;
    // ��V�����o��node��road
    public SceneRoad outRoad;

    public event Action<RoadNode> ActionOnMouseDown;

    private void Awake()
    {
        SceneManager.Instance.roadNodes.Add(this);
    }

    private void OnMouseDown()
    {
        ActionOnMouseDown?.Invoke(this);
    }
}
