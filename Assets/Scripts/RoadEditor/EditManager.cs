using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditManager : MonoSingleton<EditManager>
{
    public GameObject clickObject;
    public GameObject editRoadUI;
    public bool isPreviewMode = false;

    private GameObject _currentActiveUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �������O�_�I��UI�ϰ�
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                Debug.Log("Click UI!");
                return;
            }

            // �A�����I�쪺����
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log(hit.collider.gameObject.name);
                clickObject = hit.collider.gameObject;
            }
            else
            {
                Debug.Log("Click nothing");
                clickObject = null;
            }

            CheckOpenUI();
        }
    }

    private void CheckOpenUI()
    {
        if (_currentActiveUI != null)
            _currentActiveUI.SetActive(false);

        if (clickObject == null)
            return;

        if(clickObject.tag == "Road")
        {
            editRoadUI.SetActive(true);
            _currentActiveUI = editRoadUI;
        }
    }
}
