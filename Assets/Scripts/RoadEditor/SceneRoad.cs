using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoad : MonoBehaviour
{
    // �Ѧ�: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // Road��reference line����V��
    // (startNode)==Road==>(endNode)

    // �o�������_�lNode
    public RoadNode startNode;
    
    // �o���������INode
    public RoadNode endNode;

    // �ݩ�o���������D
    public List<RoadLane> roadLanes;

    // �M�w�D���Ѧҽu��Geometry
    public Geometry roadGeometry;

    public class Geometry
    {
        // s-t������s�b�y��
        public float s;

        // x-y�������y��
        public float x;
        public float y;

        // s-t����������(�f�ɰw)
        public float hdg;

        // �D������
        public float length;
    }
}
