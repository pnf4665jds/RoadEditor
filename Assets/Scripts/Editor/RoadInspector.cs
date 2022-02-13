using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CustomEditor(typeof(Road))]
public class RoadInspector : Editor
{
    private Road _targetRoad;

    void OnDisable()
    {
        Tools.hidden = false;
    }

    /// <summary>
    /// 覆寫Road的Inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _targetRoad = target as Road;

        GUILayout.Label("---------------Preview Settings---------------");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referenceLineWrapper"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("roadRenderer"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lanePrefab"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("laneParent"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("drawControlPoints"), true);

        GUILayout.Label("---------------Add & Remove Lane---------------");

        if(GUILayout.Button("Add Left Lane"))
        {
            _targetRoad.AddLeftLane();
        }

        if (GUILayout.Button("Remove Left Lane"))
        {
            _targetRoad.RemoveLeftLane();
        }

        if (GUILayout.Button("Add Right Lane"))
        {
            _targetRoad.AddRightLane();
        }

        if (GUILayout.Button("Remove Right Lane"))
        {
            _targetRoad.RemoveRightLane();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        _targetRoad = target as Road;
        EditManager.Instance.selectedRoad = _targetRoad;

        // 計算現在指到的是哪個控制點
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        _targetRoad.pointerKnobIndex = 0;
        // 這裡的min對應畫出來的控制點圓球半徑
        float min = 0.2f;
        int index = 0;
        // 找最近的點
        float temp = MyMathf.DistanceRay2Point(ray, _targetRoad.referenceLineWrapper.GetStartControlPointPos());
        if (min > temp)
        {
            min = temp;
            index = 1;
        }

        temp = MyMathf.DistanceRay2Point(ray, _targetRoad.referenceLineWrapper.GetEndControlPointPos());
        if (min > temp)
        {
            min = temp;
            index = -1;
        }

        _targetRoad.pointerKnobIndex = index;

        // 如果按下滑鼠將目前指到的變為選到的
        if (Event.current.type == EventType.MouseDown)
        {
            _targetRoad.selectedKnobIndex = index;
        }

        // 重新繪製scene view
        SceneView.lastActiveSceneView.Repaint();
    }
}
