using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoad : MonoBehaviour
{
    // �Ѧ�: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // �Ѧ�: https://hackmd.io/@yashashin/By78mxp0F#Roads
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

    // �p�G���ݩ�Junction�h��null reference
    public JunctionNode parentJunction;

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

    private void Awake()
    {
        SceneManager.Instance.sceneRoads.Add(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(startNode.transform.position, endNode.transform.position);
        DrawArrow(startNode.transform.position, endNode.transform.position - startNode.transform.position);
    }

    public void DrawArrow(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Gizmos.DrawRay(pos, direction);
        DrawArrowEnd(true, pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle);
    }

    private void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 up = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back;
        Vector3 down = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back;
        if (gizmos)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, up * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, down * arrowHeadLength);
        }
        else
        {
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, up * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, down * arrowHeadLength, color);
        }
    }
}
