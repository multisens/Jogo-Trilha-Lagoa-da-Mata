using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SelectMatchingNameObjects : EditorWindow
{
    [MenuItem("Tools/Select Objects With Matching Name %#m")] // Ctrl/Cmd + Shift + M
    static void SelectObjectsWithMatchingName()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        string selectedName = StripCloneSuffix(Selection.activeGameObject.name);
        List<GameObject> matchingObjects = new List<GameObject>();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            string cleanName = StripCloneSuffix(obj.name);
            if (cleanName.Contains(selectedName))
            {
                matchingObjects.Add(obj);
            }
        }

        if (matchingObjects.Count > 0)
        {
            Selection.objects = matchingObjects.ToArray();
            Debug.Log($"Selected {matchingObjects.Count} objects matching '{selectedName}'.");
        }
        else
        {
            Debug.Log($"No objects found matching '{selectedName}'.");
        }
    }

    // Removes Unity's clone suffix: " (1)", " (2)", etc.
    static string StripCloneSuffix(string name)
    {
        return Regex.Replace(name, @" \(\d+\)$", "");
    }
}
