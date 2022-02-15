using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class EditManager : MonoSingleton<EditManager>
{
    /// <summary>
    /// 要被用來生成的道路prefabs
    /// </summary>
    public List<GameObject> roadPrefabs;

    /// <summary>
    /// 被選取的道路物件
    /// </summary>
    public Road selectedRoad;
}
