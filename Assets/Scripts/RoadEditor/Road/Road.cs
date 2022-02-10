using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, INode
{
    // 把σ: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // 把σ: https://hackmd.io/@yashashin/By78mxp0F#Roads
    // Roadreference lineΤよ┦

    public GameObject lanePrefab;
    public GameObject laneParent;

    // 玡硈Road
    public Road predecessorRoad { get; set; }

    // 硈Road
    public Road successorRoad { get; set; }

    // 把σ絬オ凹ó笵
    public List<Lane> leftLanes { get; set; }

    // い丁ó笵
    public Lane centerLane { get; set; }

    // 把σ絬凹ó笵
    public List<Lane> rightLanes { get; set; }

    // 把σ絬wrapper
    public ReferenceLineWrapper referenceLineWrapper;

    // 酶籹RoadComponent
    public RoadRenderer roadRenderer;

    // 狦ぃ妮Junction玥null reference
    public JunctionNode parentJunction { get; set; }

    private MeshRenderer _nodeRenderer { get; set; }

    private void Awake()
    {
        SceneManager.Instance.sceneRoads.Add(this);
        SceneManager.Instance.sceneNodes.Add(this);
        leftLanes = new List<Lane>();
        rightLanes = new List<Lane>();

        _nodeRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        roadRenderer.UpdateRoad(referenceLineWrapper, leftLanes, rightLanes);
    }

    public void AddLeftLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, laneParent.transform);
        Lane lane = laneObject.GetComponent<Lane>();
        leftLanes.Add(lane);
        lane.OnNodeInit();
        laneObject.transform.localPosition = laneParent.transform.right * 1.0f * leftLanes.Count;
        laneObject.name = "LeftLane" + leftLanes.Count;
    }

    public void RemoveLeftLane()
    {
        if (leftLanes.Count <= 0)
            return;
        int removeIndex = leftLanes.Count - 1;
        Lane lane = leftLanes[removeIndex];
        leftLanes.RemoveAt(removeIndex);
        SceneManager.Instance.sceneNodes.Remove(lane);
        Destroy(lane.gameObject);
    }

    public void AddRightLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, laneParent.transform);
        Lane lane = laneObject.GetComponent<Lane>();
        rightLanes.Add(lane);
        lane.OnNodeInit();
        laneObject.transform.localPosition = -laneParent.transform.right * 1.0f * rightLanes.Count;
        laneObject.name = "RightLane" + rightLanes.Count;
    }

    public void RemoveRightLane()
    {
        if (rightLanes.Count <= 0)
            return;
        int removeIndex = rightLanes.Count - 1;
        Lane lane = rightLanes[removeIndex];
        rightLanes.RemoveAt(removeIndex);
        SceneManager.Instance.sceneNodes.Remove(lane);
        Destroy(lane.gameObject);
    }

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void OnNodeInit()
    {
        if (EditManager.Instance.isPreviewMode)
            OnPreviewMode();
        else
            OnEditMode();
    }

    public void OnPreviewMode()
    {
        roadRenderer.SetVisibility(true);
        _nodeRenderer.enabled = false;
    }

    public void OnEditMode()
    {
        roadRenderer.SetVisibility(false);
        _nodeRenderer.enabled = true;
    }
}