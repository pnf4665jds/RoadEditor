using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // �p�G���ݩ�Junction�h��null reference
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