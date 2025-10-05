using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class PrefabReplacer : EditorWindow
{
    [MenuItem("Tools/Create Prefab And Replace Matches %#r")] // Ctrl/Cmd + Shift + R
    static void ReplaceWithPrefab()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        GameObject original = Selection.activeGameObject;
        string cleanName = StripCloneSuffix(original.name);
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> matchingObjects = new List<GameObject>();

        // Find all objects matching the cleaned name
        foreach (GameObject obj in allObjects)
        {
            if (obj.scene.IsValid() && StripCloneSuffix(obj.name) == cleanName)
            {
                matchingObjects.Add(obj);
            }
        }

        if (matchingObjects.Count == 0)
        {
            Debug.Log("No matching objects found.");
            return;
        }

        if (!EditorUtility.DisplayDialog(
            "Replace Objects With Prefab",
            $"This will create a prefab from '{original.name}' and replace {matchingObjects.Count} matching objects in the scene. Proceed?",
            "Yes", "Cancel"))
        {
            return;
        }

        // Create the prefab
        string prefabFolder = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(prefabFolder))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }

        string prefabPath = $"{prefabFolder}/{cleanName}.prefab";

        GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(original, prefabPath, InteractionMode.UserAction);

        int replacedCount = 0;

        foreach (GameObject obj in matchingObjects)
        {
            // Skip the original — it's already replaced
            if (obj == original)
                continue;

            Transform t = obj.transform;
            GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, obj.scene);

            newObj.transform.SetParent(t.parent);
            newObj.transform.position = t.position;
            newObj.transform.rotation = t.rotation;
            newObj.transform.localScale = t.localScale;

            Undo.RegisterCreatedObjectUndo(newObj, "Replace with Prefab");
            Undo.DestroyObjectImmediate(obj);

            replacedCount++;
        }

        Debug.Log($"Prefab created at '{prefabPath}'. Replaced {replacedCount} objects in the scene.");
    }

    // Removes Unity's clone suffix: " (1)", " (2)", etc.
    static string StripCloneSuffix(string name)
    {
        return Regex.Replace(name, @" \(\d+\)$", "");
    }
}
