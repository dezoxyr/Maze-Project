using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScenesManager : EditorWindow
{
    [MenuItem("Tools/Scenes manager")]
    public static void ShowWindow()
    {
        GetWindow<ScenesManager>(false, "Scenes manager", true);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.Label("hello world!");
        if (GUILayout.Button("click"))
        {
            Debug.Log("The cake is a Lie");
        }

        GUILayout.EndVertical();
    }

    public class SceneData {

    }
}
