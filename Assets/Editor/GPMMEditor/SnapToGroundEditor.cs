using UnityEngine;
using UnityEditor;

public class SnapToGround : EditorWindow
{
    [MenuItem("Tools/Snap to Ground %e")]
    static void SnapSelectedObjectsToGround()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Collider[] colliders = obj.GetComponentsInChildren<Collider>();
            if (colliders.Length == 0)
            {
                Debug.LogWarning($"'{obj.name}' and its children have no colliders, skipping.");
                continue;
            }

            // Calculate combined bounds
            Bounds combinedBounds = colliders[0].bounds;
            for (int i = 1; i < colliders.Length; i++)
            {
                combinedBounds.Encapsulate(colliders[i].bounds);
            }

                // Temporarily disable colliders to avoid self-intersection
                bool[] prevEnabled = new bool[colliders.Length];
                for (int i = 0; i < colliders.Length; i++)
                {
                    prevEnabled[i] = colliders[i].enabled;
                    colliders[i].enabled = false;
                }

                Vector3 rayOrigin = new Vector3(combinedBounds.center.x, combinedBounds.max.y + 1f, combinedBounds.center.z);
                bool hitGround = Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 1000f);

                // Restore colliders
                for (int i = 0; i < colliders.Length; i++)
                {
                    colliders[i].enabled = prevEnabled[i];
                }

                if (hitGround)
                {
                    Undo.RecordObject(obj.transform, "Snap to Ground");
                    Vector3 newPosition = obj.transform.position;
                    // Move so the bottom of the combined bounds sits at the hit point
                    newPosition.y = obj.transform.position.y + (hit.point.y - combinedBounds.min.y);
                    obj.transform.position = newPosition;
                    Debug.Log($"Snapped '{obj.name}' to ground at {hit.point}.");
                }
                else
                {
                    Debug.LogWarning($"No surface found below '{obj.name}'.");
                }
        }
    }
}
