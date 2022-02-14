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
    /// �мgRoad��Inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _targetRoad = target as Road;

        GUILayout.Label("---------------Debug---------------");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pointerKnobIndex"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedKnobIndex"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pointerLaneIndex"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("selectedLaneIndex"), true);

        GUILayout.Label("---------------Preview Settings---------------");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("referenceLineWrapper"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("roadRenderer"), true);
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

        GUILayout.Label("---------------Lane Setting---------------");
        if(_targetRoad.selectedLaneIndex > 0)
        {
            // ���road���U��leftLanes��parentRoad property
            var lanesProperty = serializedObject.FindProperty("leftLanes");
            var lane = lanesProperty.GetArrayElementAtIndex(0);
            EditorGUILayout.PropertyField(lane.FindPropertyRelative("parentRoad"));
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        _targetRoad = target as Road;
        EditManager.Instance.selectedRoad = _targetRoad;

        // �p��{�b���쪺�O���ӱ����I
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        CheckKnob(ray);
        CheckLaneNode(ray);

        // ���sø�sscene view
        SceneView.lastActiveSceneView.Repaint();
    }

    private void CheckKnob(Ray ray)
    {
        _targetRoad.pointerKnobIndex = 0;
        // �o�̪�min�����e�X�Ӫ������I��y�b�|
        float min = 0.2f;
        int index = 0;
        // ��̪��I
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

        // �p�G���U�ƹ��N�ثe���쪺�ܬ���쪺
        if (Event.current.type == EventType.MouseDown)
        {
            _targetRoad.selectedKnobIndex = index;
        }
    }

    private void CheckLaneNode(Ray ray)
    {
        _targetRoad.pointerLaneIndex = 0;
        // �o�̪�min�����e�X�Ӫ������I��y�b�|
        float min = 0.2f;
        int index = 0;
        // ��̪��I
        for(int i = 0; i < _targetRoad.leftLanes.Count; i++)
        {
            Vector3 p = _targetRoad.transform.position + (-_targetRoad.transform.right) * (i + 1) * _targetRoad.leftLanes[i].laneWidth.a / 2;
            float temp = MyMathf.DistanceRay2Point(ray, p);
            if (min > temp)
            {
                min = temp;
                index = i + 1;
            }
        }
        for (int i = 0; i < _targetRoad.rightLanes.Count; i++)
        {
            Vector3 p = _targetRoad.transform.position + (_targetRoad.transform.right) * (i + 1) * _targetRoad.rightLanes[i].laneWidth.a / 2;
            float temp = MyMathf.DistanceRay2Point(ray, p);
            if (min > temp)
            {
                min = temp;
                index = -(i + 1);
            }
        }

        _targetRoad.pointerLaneIndex = index;

        // �p�G���U�ƹ��N�ثe���쪺�ܬ���쪺
        if (Event.current.type == EventType.MouseDown)
        {
            _targetRoad.selectedLaneIndex = index;
        }
    }
}
