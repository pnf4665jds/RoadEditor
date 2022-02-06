using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoad : MonoBehaviour
{
    // 參考: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // Road的reference line有方向性
    // (startNode)==Road==>(endNode)

    // 這條路的起始Node
    public RoadNode startNode;
    
    // 這條路的終點Node
    public RoadNode endNode;

    // 屬於這條路的車道
    public List<RoadLane> roadLanes;

    // 決定道路參考線的Geometry
    public Geometry roadGeometry;

    public class Geometry
    {
        // s-t平面的s軸座標
        public float s;

        // x-y平面的座標
        public float x;
        public float y;

        // s-t平面的旋轉(逆時針)
        public float hdg;

        // 道路長度
        public float length;
    }
}
