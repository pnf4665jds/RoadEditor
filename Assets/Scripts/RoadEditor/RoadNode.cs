using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    // ==inRoad==>(Node)==outRoad==>

    // 方向朝向這個node的road
    public SceneRoad inRoad;
    // 方向指離這個node的road
    public SceneRoad outRoad;
}
