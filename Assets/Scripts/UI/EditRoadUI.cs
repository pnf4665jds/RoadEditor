using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditRoadUI : MonoBehaviour
{
    public Button addLeftLaneButton;

    // Start is called before the first frame update
    void Start()
    {
        addLeftLaneButton.onClick.AddListener(() => AddLeftLane());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddLeftLane()
    {
        EditManager.Instance.clickObject.GetComponent<Road>().AddLeftLane();
    }
}
