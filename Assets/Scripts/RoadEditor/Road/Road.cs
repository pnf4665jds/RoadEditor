using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Road : MonoBehaviour
{
    // �Ѧ�: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // �Ѧ�: https://hackmd.io/@yashashin/By78mxp0F#Roads
    // Road��reference line����V��

    public GameObject lanePrefab;
    public GameObject laneParent;

    // �e�@�Ӭ۳s��Road
    public Road predecessorRoad { get; set; }

    // ��@�Ӭ۳s��Road
    public Road successorRoad { get; set; }

    // �Ѧҽu���������D
    public List<Lane> leftLanes { get; set; }

    // �������D
    public Lane centerLane { get; set; }

    // �Ѧҽu�k�������D
    public List<Lane> rightLanes { get; set; }

    // �Ѧҽuwrapper
    public ReferenceLineWrapper referenceLineWrapper;

    // ø�sRoad��Component
    public RoadRenderer roadRenderer;

    // �O�_ø�s�����I
    public bool drawControlPoints = true;

    // �O�_ø�s���DNode
    public bool drawLaneNode = true;

    public int pointerKnobIndex = 0;
    public int selectedKnobIndex = 0;
    public int pointerLaneIndex = 0;
    public int selectedLaneIndex = 0;

    // �p�G���ݩ�Junction�h��null reference
    public JunctionNode parentJunction { get; set; }

    private void Awake()
    {
        UnityEditor.Tools.hidden = true;
        SceneManager.Instance.sceneRoads.Add(this);
        leftLanes = new List<Lane>();
        rightLanes = new List<Lane>();

        // ��l�Ʈɥ��k���U�s�W�@�����D
        AddLeftLane();
        AddRightLane();
    }

    private void Update()
    {
        roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

    /// <summary>
    /// �s�W�����D
    /// </summary>
    public void AddLeftLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, laneParent.transform);
        Lane lane = laneObject.GetComponent<Lane>();
        leftLanes.Add(lane);
        lane.parentRoad = this;
        Vector3 forward = referenceLineWrapper.GetReferenceLineWorldTangent(0.5f);
        Vector3 left = Vector3.Cross(Vector3.up, -forward);
        laneObject.transform.position = referenceLineWrapper.GetReferenceLineWorldPos(0.5f) + left * 1.0f * leftLanes.Count;
        laneObject.name = "LeftLane" + leftLanes.Count;
    }

    /// <summary>
    /// ���������D
    /// </summary>
    public void RemoveLeftLane()
    {
        if (leftLanes.Count <= 0)
        {
            Debug.Log("No left lane exists");
            return;
        }
        int removeIndex = leftLanes.Count - 1;
        Lane lane = leftLanes[removeIndex];
        leftLanes.RemoveAt(removeIndex);
        DestroyImmediate(lane.gameObject);
    }

    /// <summary>
    /// �s�W�k���D
    /// </summary>
    public void AddRightLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, laneParent.transform);
        Lane lane = laneObject.GetComponent<Lane>();
        rightLanes.Add(lane);
        lane.parentRoad = this;
        Vector3 forward = referenceLineWrapper.GetReferenceLineWorldTangent(0.5f);
        Vector3 right = Vector3.Cross(Vector3.up, forward);
        laneObject.transform.position = referenceLineWrapper.GetReferenceLineWorldPos(0.5f) + right * 1.0f * leftLanes.Count;
        laneObject.name = "RightLane" + rightLanes.Count;
    }

    /// <summary>
    /// �����k���D
    /// </summary>
    public void RemoveRightLane()
    {
        if (rightLanes.Count <= 0)
        {
            Debug.Log("No right lane exists");
            return;
        }
        int removeIndex = rightLanes.Count - 1;
        Lane lane = rightLanes[removeIndex];
        rightLanes.RemoveAt(removeIndex);
        DestroyImmediate(lane.gameObject);
    }

    /// <summary>
    /// �]�w�e�@�Ӭ۳s���D��
    /// </summary>
    /// <param name="road"></param>
    public void SetPredecessorRoad(Road road)
    {
        predecessorRoad = road;
        road.successorRoad = this;

        // �p�����q�A�T�O�Y�����¦V���T
        Vector3 thisStartTangent = referenceLineWrapper.GetReferenceLineTangent(0);
        Vector3 anotherEndTangent = road.referenceLineWrapper.GetReferenceLineTangent(1);
        road.transform.rotation = transform.rotation * Quaternion.FromToRotation(anotherEndTangent, thisStartTangent);

        // �p��첾�q�A�T�O�Y���۳s
        Vector3 thisStartPos = referenceLineWrapper.GetStartControlPointPos();
        Vector3 anotherEndPos = road.referenceLineWrapper.GetEndControlPointPos();
        road.transform.position += thisStartPos - anotherEndPos;
    }

    /// <summary>
    /// �]�w��@�Ӭ۳s���D��
    /// </summary>
    /// <param name="road"></param>
    public void SetSuccessorRoad(Road road)
    {
        successorRoad = road;
        road.predecessorRoad = this;

        // �p�����q�A�T�O�Y�����¦V���T
        Vector3 thisEndTangent = referenceLineWrapper.GetReferenceLineTangent(1);
        Vector3 anotherStartTangent = road.referenceLineWrapper.GetReferenceLineTangent(0);
        road.transform.rotation = transform.rotation * Quaternion.FromToRotation(anotherStartTangent, thisEndTangent);

        // �p��첾�q�A�T�O�Y���۳s
        Vector3 thisEndPos = referenceLineWrapper.GetEndControlPointPos();
        Vector3 anotherStartPos = road.referenceLineWrapper.GetStartControlPointPos();
        road.transform.position += thisEndPos - anotherStartPos;
    }

    void OnDrawGizmos()
    {
        // �e�����I��T
        if (drawControlPoints)
        {
            Gizmos.color = Color.gray;
            for (int i = -1; i <= 1; i+=2)
            {
                Vector3 p;
                if (i == 1)
                {
                    if (predecessorRoad != null)
                        continue;
                    p = referenceLineWrapper.GetStartControlPointPos();
                }
                else
                {
                    if (successorRoad != null)
                        continue;
                    p = referenceLineWrapper.GetEndControlPointPos();
                }

                if (i == pointerKnobIndex)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(p, 0.2f);
                    Gizmos.color = Color.gray;
                }
                else if (i == selectedKnobIndex)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(p, 0.2f);
                    Gizmos.color = Color.gray;
                }
                else
                {
                    Gizmos.DrawSphere(p, 0.2f);
                }
            }
        }
    }
}