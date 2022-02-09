using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // 把σ: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // 把σ: https://hackmd.io/@yashashin/By78mxp0F#Roads
    // Roadreference lineΤよ┦

    public GameObject lanePrefab;
    public GameObject leftLaneParent;

    // 玡硈Road
    public Road predecessorRoad { get; set; }

    // 硈Road
    public Road successorRoad { get; set; }

    // 把σ絬オ凹ó笵
    public List<Lane> leftLanes { get; set; }

    // い丁ó笵
    public Lane centerLane { get; set; }

    // 把σ絬凹ó笵
    public List<Lane> rightLane { get; set; }

    // 把σ絬wrapper
    public ReferenceLineWrapper referenceLineWrapper;

    // 狦ぃ妮Junction玥null reference
    public JunctionNode parentJunction { get; set; }

    private void Awake()
    {
        SceneManager.Instance.sceneRoads.Add(this);
        leftLanes = new List<Lane>();
    }

    private void Update()
    {
        float offset = 0;
        foreach(Lane lane in leftLanes)
        {
            lane.DrawLane(referenceLineWrapper, false, offset);
            offset += lane.laneWidth.a;
        }
    }

    public void AddLeftLane()
    {
        GameObject laneObject = Instantiate(lanePrefab, leftLaneParent.transform);
        leftLanes.Add(laneObject.GetComponent<Lane>());
    }
}