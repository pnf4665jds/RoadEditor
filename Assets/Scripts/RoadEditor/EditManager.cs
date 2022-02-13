using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class EditManager : MonoSingleton<EditManager>
{
    public GameObject roadPrefab;
    public Road selectedRoad;
    public GameObject editRoadUI;
    public bool isPreviewMode = true;
}
