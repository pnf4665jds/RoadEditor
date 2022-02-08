using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ѧ�: https://blog.csdn.net/lewif/article/details/78575840
/// </summary>
public class JunctionNode : MonoBehaviour
{
    // �C��junction�]�t�n�X��connection
    public List<Connection> connections;
}

/// <summary>
/// �C��connection�w�q�@�ը��D���s�����Y
/// </summary>
public class Connection
{
    public SceneRoad incomingRoad;
    public SceneRoad connectingRoads;
    public RoadLane fromLane;
    public RoadLane toLane;
}
