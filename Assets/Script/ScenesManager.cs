using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ScenesManager : EditorWindow
{

    private List<SceneData> listScene = new List<SceneData>();
    Vector2 scrollPosition;
    bool toggleScene = false;

    [MenuItem("Tools/Scenes manager")]
    public static void ShowWindow()
    {
        GetWindow<ScenesManager>(false, "Scenes manager", true);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Always check all your required scenes are in build");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("CheckBuild"))
        {
            GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Select Scenes to load");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Refresh list"))
        {
            RefreshContent();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(100), GUILayout.Height(100));
        foreach(SceneData scene in listScene)
        {
            if (scene.isActive = GUILayout.Toggle(scene.isActive, scene.name)) {
                if (scene.isInNextFrame)
                {
                    EditorSceneManager.OpenScene(scene.chemin, OpenSceneMode.Additive);
                    scene.isInNextFrame = false;
                }
                
            } else
            {
                if (EditorSceneManager.sceneCount == 1 && !scene.isInNextFrame)
                {
                    GetWindow<ScenesManager>().ShowNotification(new GUIContent("STOP ERROR"));
                    scene.isActive = true;
                } else
                {
                    EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(scene.name), true);
                    scene.isInNextFrame = true;
                }
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void RefreshContent()
    {
        listScene.Clear();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            listScene.Add(new SceneData(System.IO.Path.GetFileNameWithoutExtension(scene.path), scene.path, false, true));
        }
        int index;
        for (int i =0; i< EditorSceneManager.sceneCount; i++)
        {
            index = EditorSceneManager.GetSceneAt(i).buildIndex;
            listScene[index].isActive = true;
            listScene[index].isInNextFrame = false;
        }
    }

}
public class SceneData
{
    public string name;
    public string chemin;
    public bool isActive;
    public bool isInNextFrame;
    public SceneData(string n, string c, bool isAct, bool isIn)
    {
        name = n;
        chemin = c;
        isActive = isAct;
        isInNextFrame = isIn;
    }
}