using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneManager : MonoSingleton<SceneManager>
{
    /// <summary>
    /// �޲z���������Ҧ�road
    /// </summary>

    public HashSet<Road> sceneRoads = new HashSet<Road>();
}
