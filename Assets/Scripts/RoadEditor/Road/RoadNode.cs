using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    // 這個Node所控制的Road
    public Road ControlledRoad;

    public event Action<RoadNode> ActionOnMouseDown;

    private void OnMouseDown()
    {
        ActionOnMouseDown?.Invoke(this);
    }
}
