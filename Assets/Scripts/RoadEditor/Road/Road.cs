using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
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

    // 狦ぃ妮Junction玥null reference
    public JunctionNode parentJunction { get; set; }

    private void Awake()
    {
        SceneManager.Instance.sceneRoads.Add(this);
        leftLanes = new List<Lane>();
        rightLanes = new List<Lane>();
    }

    private void Update()
    {
        float offsetLeft = 0, offsetRight = 0;
        foreach(Lane lane in leftLanes)
        {
            lane.DrawLane(referenceLineWrapper, false, offsetLeft);
            offsetLeft += lane.laneWidth.a;
        }

        foreach (Lane lane in rightLanes)
        {
            lane.DrawLane(referenceLineWrapper, true, offsetRight);
            offsetRight += lane.laneWidth.a;
        }
    }

    public void AddLeftLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, laneParent.transform);
        leftLanes.Add(laneObject.GetComponent<Lane>());
    }

    public void RemoveLeftLane()
    {
        if (leftLanes.Count <= 0)
            return;
        int removeIndex = leftLanes.Count - 1;
        Lane lane = leftLanes[removeIndex];
        leftLanes.RemoveAt(removeIndex);
        Destroy(lane.gameObject);
    }

    public void AddRightLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, laneParent.transform);
        rightLanes.Add(laneObject.GetComponent<Lane>());
    }

    public void RemoveRightLane()
    {
        if (rightLanes.Count <= 0)
            return;
        int removeIndex = rightLanes.Count - 1;
        Lane lane = rightLanes[removeIndex];
        rightLanes.RemoveAt(removeIndex);
        Destroy(lane.gameObject);
    }

    public void SetVisibility(bool isVisible)
    {
        foreach (Lane lane in leftLanes)
        {
            lane.SetVisibility(isVisible);
        }

        foreach (Lane lane in rightLanes)
        {
            lane.SetVisibility(isVisible);
        }
    }
}