using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneManager : MonoSingleton<SceneManager>
{
    /// <summary>
    /// 紀錄場景中的所有road
    /// </summary>

    public HashSet<Road> sceneRoads = new HashSet<Road>();
}
