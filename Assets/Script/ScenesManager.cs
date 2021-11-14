using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;
using System.Linq;


#if UNITY_EDITOR
public class ScenesManager : UnityEditor.EditorWindow
{

    private List<SceneData> m_listScene = new List<SceneData>();
    Vector2 m_scrollPosition;
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
        GUILayout.BeginScrollView(m_scrollPosition, GUILayout.Width(100), GUILayout.Height(100));
        foreach(SceneData scene in m_listScene)
        {
            if (scene.isActive = GUILayout.Toggle(scene.isActive, scene.name)) {
                if (scene.isInNextFrame)
                {
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scene.chemin, UnityEditor.SceneManagement.OpenSceneMode.Additive);
                    scene.isInNextFrame = false;
                }
                
            } else
            {
                if (UnityEditor.SceneManagement.EditorSceneManager.sceneCount == 1 && !scene.isInNextFrame)
                {
                    GetWindow<ScenesManager>().ShowNotification(new GUIContent("STOP ERROR"));
                    scene.isActive = true;
                } else
                {
                    UnityEditor.SceneManagement.EditorSceneManager.CloseScene(UnityEditor.SceneManagement.EditorSceneManager.GetSceneByName(scene.name), true);
                    scene.isInNextFrame = true;
                }
            }
        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
    }

    private void RefreshContent()
    {
        m_listScene.Clear();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            m_listScene.Add(new SceneData(System.IO.Path.GetFileNameWithoutExtension(scene.path), scene.path, false, true));
        }
        int index;
        for (int i =0; i< UnityEditor.SceneManagement.EditorSceneManager.sceneCount; i++)
        {
            index = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i).buildIndex;
            m_listScene[index].isActive = true;
            m_listScene[index].isInNextFrame = false;
        }
    }

    [MenuItem("Assets/Scene/Add to build", false)]
    public static void AddToBuild()
    {
        var sceneSelect = Selection.activeObject as SceneAsset;
        if(EditorBuildSettings.scenes.ToList().Any(s => s.path == AssetDatabase.GetAssetPath(sceneSelect))){
            GetWindow<ScenesManager>().ShowNotification(new GUIContent("STOP ERROR"));
        } else
        {
            List<EditorBuildSettingsScene> buildScene = EditorBuildSettings.scenes.ToList();
            buildScene.Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(sceneSelect), true));
            EditorBuildSettings.scenes = buildScene.ToArray();
        }
    }

    [MenuItem("Assets/Scene/Add to build", true)]
    static bool AddToBuildValidate()
    {
        if (Selection.activeObject is SceneAsset)
        {
            return true;
        } else
        {
            return false;
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
#endif