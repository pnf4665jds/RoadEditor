using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditRoadUI : MonoBehaviour
{
    public Button addLeftLaneButton;
    public Button removeLeftLaneButton;
    public Button addRightLaneButton;
    public Button removeRightLaneButton;

    // Start is called before the first frame update
    void Start()
    {
        addLeftLaneButton.onClick.AddListener(() => AddLeftLane());
        removeLeftLaneButton.onClick.AddListener(() => RemoveLeftLane());
        addRightLaneButton.onClick.AddListener(() => AddRightLane());
        removeRightLaneButton.onClick.AddListener(() => RemoveRightLane());
    }

    private void AddLeftLane()
    {
        EditManager.Instance.clickObject.GetComponent<Road>().AddLeftLane();
    }

    private void AddRightLane()
    {
        EditManager.Instance.clickObject.GetComponent<Road>().AddRightLane();
    }

    private void RemoveLeftLane()
    {
        EditManager.Instance.clickObject.GetComponent<Road>().RemoveLeftLane();
    }

    private void RemoveRightLane()
    {
        EditManager.Instance.clickObject.GetComponent<Road>().RemoveRightLane();
    }
}
