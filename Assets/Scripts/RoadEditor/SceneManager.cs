using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoSingleton<SceneManager>
{
    /// <summary>
    /// �޲z���������Ҧ�road
    /// </summary>

    public HashSet<Road> sceneRoads = new HashSet<Road>();

    public HashSet<INode> sceneNodes = new HashSet<INode>();
}
