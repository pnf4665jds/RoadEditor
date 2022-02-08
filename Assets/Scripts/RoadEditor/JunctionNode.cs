using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 參考: https://blog.csdn.net/lewif/article/details/78575840
/// </summary>
public class JunctionNode : MonoBehaviour
{
    // 每個junction包含好幾個connection
    public List<Connection> connections;
}

/// <summary>
/// 每個connection定義一組車道的連接關係
/// </summary>
public class Connection
{
    public SceneRoad incomingRoad;
    public SceneRoad connectingRoads;
    public RoadLane fromLane;
    public RoadLane toLane;
}
