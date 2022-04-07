using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public static class UpdateBuildSettigns
{
    [MenuItem("Example/UpdateBuildSettings")]
    public static void UpdateSettings()
    {
        // get current editor setup
        SceneSetup[] editorScenes = EditorSceneManager.GetSceneManagerSetup();

        // filter list e.g. get only scenes with isActive true
        var activeEditorScenes = editorScenes.Where(scene => scene.isLoaded);

        // set those scenes as the buildsettings
        List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
        foreach (var sceneAsset in activeEditorScenes)
        {
            string scenePath = sceneAsset.path;

            // ignore unsaved scenes
            if (!string.IsNullOrEmpty(scenePath)) continue;

            editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
        }

        // Set the Build Settings window Scene list
        EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
    }
    
    
    private static void OnSceneUnloaded(Scene current)
    {
        UpdateSettings();
    }

    private static void OnSceneLoaded(Scene current, OpenSceneMode mode)
    {
        UpdateSettings();
    }
    
    // flag to check if auto-updates are currently enabled
    private static bool isEnabled;

// disable the "EnableAutoUpdate" button if already enabled
    [MenuItem("Example/EnableAutoUpdate", true)]
    private static bool CanEnable()
    {
        return !isEnabled;
    }

// disable the "DisableAutoUpdate" button if already disabled
    [MenuItem("Example/DisableAutoUpdate", true)]
    private static bool CanDisable()
    {
        return isEnabled;
    }

// add callbacks
    [MenuItem("Example/EnableAutoUpdate")]
    private static void EnableAutoUpdate()
    {
        // it is always save to remove callbacks even if they are not there
        // makes sure they are always only added once
        EditorSceneManager.sceneOpened -= OnSceneLoaded;
        EditorSceneManager.sceneClosed -= OnSceneUnloaded;

        EditorSceneManager.sceneOpened += OnSceneLoaded;
        EditorSceneManager.sceneClosed += OnSceneUnloaded;

        isEnabled = true;
    }

// remove callbacks
    [MenuItem("Example/DisableAutoUpdate")]
    private static void DisableAutoUpdate()
    {
        EditorSceneManager.sceneOpened -= OnSceneLoaded;
        EditorSceneManager.sceneClosed -= OnSceneUnloaded;

        isEnabled = false;
    }


}
#endif
