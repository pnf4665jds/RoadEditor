using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // �Ѧ�: https://blog.csdn.net/qq_36622009/article/details/107006508#OpenDrive_52
    // �Ѧ�: https://hackmd.io/@yashashin/By78mxp0F#Roads
    // Road��reference line����V��

    public GameObject lanePrefab;
    public GameObject leftLaneParent;

    // �e�@�Ӭ۳s��Road
    public Road predecessorRoad { get; set; }

    // ��@�Ӭ۳s��Road
    public Road successorRoad { get; set; }

    // �Ѧҽu���������D
    public List<Lane> leftLanes { get; set; }

    // �������D
    public Lane centerLane { get; set; }

    // �Ѧҽu�k�������D
    public List<Lane> rightLane { get; set; }

    // �Ѧҽuwrapper
    public ReferenceLineWrapper referenceLineWrapper;

    // �p�G���ݩ�Junction�h��null reference
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