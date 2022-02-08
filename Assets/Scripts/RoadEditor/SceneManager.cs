using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    /// <summary>
    /// 管理場景中的所有road跟node
    /// </summary>

    public HashSet<SceneRoad> sceneRoads = new HashSet<SceneRoad>();
    public HashSet<RoadNode> roadNodes = new HashSet<RoadNode>();
}
