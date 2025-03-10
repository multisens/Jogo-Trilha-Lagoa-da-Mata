using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Extracts all materials from the selected GameObjects and saves them as individual assets.
/// </summary>
public class ExtractMaterials : EditorWindow
{
    private string extractionFolder = "Assets/Lab/Extract";
    private string materialFolder = "Materials";
    private Dictionary<int, string> materialPaths = new Dictionary<int, string>();

    /// <summary>
    /// Adds a menu item to the Unity Editor.
    /// </summary>
    [MenuItem("Optimization/Analysis/Extract Materials")]
    public static void ShowWindow()
    {
        GetWindow<ExtractMaterials>("Extract Materials");
    }

    /// <summary>
    /// Draws the Editor Window GUI.
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Extract Materials", EditorStyles.boldLabel);

        EditorGUILayout.HelpBox("This tool extracts all Materials from the selected GameObjects and saves them as individual assets. " +
            "The extracted assets are saved in the specified folder within the Assets directory.", MessageType.Info);

        materialFolder = EditorGUILayout.TextField("Material Path", materialFolder);

        if (GUILayout.Button("Extract and Reassign"))
        {
            ExtractAndReassign();
        }
    }

    /// <summary>
    /// Extracts all Materials from the selected GameObjects and saves them as individual assets.
    /// </summary>
    private void ExtractAndReassign()
    {
        materialFolder = $"{extractionFolder}/{materialFolder}";

        if (!Directory.Exists(materialFolder))
            Directory.CreateDirectory(materialFolder);

        MeshRenderer[] meshRenderers = FindObjectsOfType<MeshRenderer>();
        SkinnedMeshRenderer[] skinnedMeshRenderers = FindObjectsOfType<SkinnedMeshRenderer>();

        Debug.Log($"Found {meshRenderers.Length} MeshRenderers and {skinnedMeshRenderers.Length} SkinnedMeshRenderers");

        try
        {
            foreach (MeshRenderer renderer in meshRenderers)
            {
                Debug.Log($"Processing MeshRenderer: {renderer.name}");
                ProcessRenderer(renderer, materialFolder);
            }

            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
            {
                Debug.Log($"Processing SkinnedMeshRenderer: {renderer.name}");
                ProcessRenderer(renderer, materialFolder);
            }

            EditorUtility.ClearProgressBar();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error during organization: {ex.Message}");
            EditorUtility.ClearProgressBar();
        }
    }

    /// <summary>
    /// Processes a MeshRenderer or SkinnedMeshRenderer by extracting its materials.
    /// </summary>
    private void ProcessRenderer(Renderer renderer, string materialFolder)
    {
        Undo.RecordObject(renderer, "Extract Materials");

        Material[] newMaterials = new Material[renderer.sharedMaterials.Length];
        for (int i = 0; i < renderer.sharedMaterials.Length; i++)
        {
            Material material = renderer.sharedMaterials[i];
            if (material != null)
            {
                int materialID = material.GetInstanceID();
                if (!materialPaths.TryGetValue(materialID, out string materialPath))
                {
                    materialPath = $"{materialFolder}/{material.name}_{materialID}.mat";
                    Debug.Log($"Creating Material Asset: {materialPath}");
                    Material newMaterial = Object.Instantiate(material);
                    AssetDatabase.CreateAsset(newMaterial, materialPath);
                    EditorUtility.SetDirty(newMaterial);
                    materialPaths[materialID] = materialPath;
                    newMaterials[i] = newMaterial;
                }
                else
                {
                    Debug.Log($"Material already exists: {materialPath}");
                    newMaterials[i] = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
                }
            }
        }
        renderer.sharedMaterials = newMaterials;
    }
}
