using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyColorTool
{
    private static bool isAlternateColors = true; // Changez la valeur pour l'activer ou la désactiver par défaut

    static HierarchyColorTool()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCallback;
    }

    [MenuItem("Tools/Toggle Hierarchy Colors")]
    public static void ToggleHierarchyColors()
    {
        isAlternateColors = !isAlternateColors;
        SceneView.RepaintAll();
    }

    private static void HierarchyItemCallback(int instanceID, Rect selectionRect)
    {
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go != null)
        {
            int row = (int)(selectionRect.y / selectionRect.height);

            if (isAlternateColors && row % 2 == 1)
            {
                EditorGUI.DrawRect(selectionRect, new Color(0.8f, 0.8f, 0.8f, 0.005f));
            }
        }
    }
}