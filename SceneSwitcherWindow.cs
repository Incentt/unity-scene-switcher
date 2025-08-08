using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneSwitcherWindow : EditorWindow
{
    // Array to hold all scene paths in the build settings
    private string[] scenePaths;
    // Array to hold the names of all scenes
    private string[] sceneNames;

    [MenuItem("Window/Scene Switcher")]
    public static void ShowWindow()
    {
        // Show the window
        GetWindow<SceneSwitcherWindow>("Scene Switcher");
    }

    private void OnEnable()
    {
        // Load all the scenes included in the build settings
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        scenePaths = new string[sceneCount];
        sceneNames = new string[sceneCount];

        // Get the scene paths and names
        for (int i = 0; i < sceneCount; i++)
        {
            scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenePaths[i]);
        }
    }

    private void OnGUI()
    {
        // Display a button for each scene
        EditorGUILayout.LabelField("Scenes in Build", EditorStyles.boldLabel);
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (GUILayout.Button(sceneNames[i]))
            {
                // Check for unsaved changes
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    // Open the selected scene
                    EditorSceneManager.OpenScene(scenePaths[i]);
                }
            }
        }
    }
}