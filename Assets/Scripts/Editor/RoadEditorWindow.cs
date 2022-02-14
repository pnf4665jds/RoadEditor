using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoadEditorWindow : EditorWindow
{
    public string[] options = new string[] { "Road1", "Road2", "Road3" };
    private int index = 0;

    [MenuItem("Window/RoadEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(RoadEditorWindow));
    }

    void OnGUI()
    {
        if (GUILayout.Button("Create road object"))
        {
            var roadObj = PrefabUtility.InstantiatePrefab(EditManager.Instance.roadPrefabs[index]);
            Selection.activeObject = roadObj;
            Undo.RegisterCreatedObjectUndo(roadObj, "Create road");
        }
        if (GUILayout.Button("Add predecessor road"))
        {
            if (EditManager.Instance.selectedRoad != null && EditManager.Instance.selectedRoad.predecessorRoad == null)
            {
                var roadObj = PrefabUtility.InstantiatePrefab(EditManager.Instance.roadPrefabs[index]);
                Selection.activeObject = roadObj;
                EditManager.Instance.selectedRoad.SetPredecessorRoad(((GameObject)roadObj).GetComponent<Road>());
                Undo.RegisterCreatedObjectUndo(roadObj, "Create predecessor road");
            }
        }
        if (GUILayout.Button("Add successor road"))
        {
            if (EditManager.Instance.selectedRoad != null && EditManager.Instance.selectedRoad.successorRoad == null)
            {
                var roadObj = PrefabUtility.InstantiatePrefab(EditManager.Instance.roadPrefabs[index]);
                Selection.activeObject = roadObj;
                EditManager.Instance.selectedRoad.SetSuccessorRoad(((GameObject)roadObj).GetComponent<Road>());
                Undo.RegisterCreatedObjectUndo(roadObj, "Create successor road");
            }
        }

        index = EditorGUILayout.Popup(index, options);
    }
}
