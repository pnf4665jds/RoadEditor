using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class EditManager : MonoSingleton<EditManager>
{
    public List<GameObject> roadPrefabs;
    public Road selectedRoad;
    public bool isPreviewMode = true;
}
