using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class EditManager : MonoSingleton<EditManager>
{
    /// <summary>
    /// �n�Q�Ψӥͦ����D��prefabs
    /// </summary>
    public List<GameObject> roadPrefabs;

    /// <summary>
    /// �Q������D������
    /// </summary>
    public Road selectedRoad;
}
