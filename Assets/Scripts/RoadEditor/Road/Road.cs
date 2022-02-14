using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Road : MonoBehaviour, INode
{
    // 參考: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // 參考: https://hackmd.io/@yashashin/By78mxp0F#Roads
    // Road的reference line有方向性

    // 前一個相連的Road
    public Road predecessorRoad { get; set; }

    // 後一個相連的Road
    public Road successorRoad { get; set; }

    // 參考線左側的車道
    public List<Lane> leftLanes;

    // 中間車道
    public Lane centerLane { get; set; }

    // 參考線右側的車道
    public List<Lane> rightLanes { get; set; }

    // 參考線wrapper
    public ReferenceLineWrapper referenceLineWrapper;

    // 繪製Road的Component
    public RoadRenderer roadRenderer;

    // 是否繪製控制點
    public bool drawControlPoints = true;

    // 是否繪製車道node
    public bool drawLaneNode = true;

    public int pointerKnobIndex = 0;
    public int selectedKnobIndex = 0;
    public int pointerLaneIndex = 0;
    public int selectedLaneIndex = 0;

    // 如果不屬於Junction則為null reference
    public JunctionNode parentJunction { get; set; }

    private void Start()
    {
        SceneManager.Instance.sceneRoads.Add(this);
        SceneManager.Instance.sceneNodes.Add(this);
        leftLanes = new List<Lane>();
        rightLanes = new List<Lane>();

        // 初始化時左右側各新增一條車道
        AddLeftLane();
        AddRightLane();
    }

    private void Update()
    {
        //roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

    public void AddLeftLane()
    {
        Lane lane = new Lane();
        leftLanes.Add(lane);
        lane.OnNodeInit();
        lane.parentRoad = this;
        roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

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
        SceneManager.Instance.sceneNodes.Remove(lane);
        roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

    public void AddRightLane()
    {
        Lane lane = new Lane();
        rightLanes.Add(lane);
        lane.OnNodeInit();
        lane.parentRoad = this;
        roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

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
        SceneManager.Instance.sceneNodes.Remove(lane);
        roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

    public void SetPredecessorRoad(Road road)
    {
        predecessorRoad = road;
        road.successorRoad = this;
        Vector3 thisStartPos = referenceLineWrapper.GetStartControlPointPos();
        Vector3 anotherEndPos = road.referenceLineWrapper.GetEndControlPointPos();
        road.transform.Translate(thisStartPos - anotherEndPos);
    }

    public void SetSuccessorRoad(Road road)
    {
        successorRoad = road;
        road.predecessorRoad = this;
        Vector3 thisEndPos = referenceLineWrapper.GetEndControlPointPos();
        Vector3 anotherStartPos = road.referenceLineWrapper.GetStartControlPointPos();
        road.transform.Translate(thisEndPos - anotherStartPos);
    }

    public void OnNodeInit()
    {

    }

    public void OnPreviewMode()
    {

    }

    public void OnEditMode()
    {

    }

    void OnDrawGizmos()
    {
        // 畫控制點資訊
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

        if (drawLaneNode)
        {
            Gizmos.color = Color.gray;
            for(int i = 0; i < leftLanes.Count; i++)
            {
                Vector3 p = transform.position + (-transform.right) * (i + 1) * leftLanes[i].laneWidth.a / 2;
                if ((i + 1) == pointerLaneIndex)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(p, 0.2f);
                    Gizmos.color = Color.gray;
                }
                else if ((i + 1) == selectedLaneIndex)
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

            Gizmos.color = Color.gray;
            for (int i = 0; i < rightLanes.Count; i++)
            {
                Vector3 p = transform.position + transform.right * (i + 1) * leftLanes[i].laneWidth.a / 2;
                if ((i + 1) == -pointerLaneIndex)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(p, 0.2f);
                    Gizmos.color = Color.gray;
                }
                else if ((i + 1) == -selectedLaneIndex)
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