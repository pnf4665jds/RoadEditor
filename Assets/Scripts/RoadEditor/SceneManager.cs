using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    /// <summary>
    /// 管理場景中的所有road
    /// </summary>

    public HashSet<Road> sceneRoads = new HashSet<Road>();
}
