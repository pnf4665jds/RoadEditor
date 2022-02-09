using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    // �o��Node�ұ��Road
    public Road ControlledRoad;

    public event Action<RoadNode> ActionOnMouseDown;

    private void OnMouseDown()
    {
        ActionOnMouseDown?.Invoke(this);
    }
}
