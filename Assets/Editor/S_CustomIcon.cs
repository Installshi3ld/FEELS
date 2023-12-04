using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchy window game object icon.
/// http://diegogiacomelli.com.br/unitytips-hierarchy-window-gameobject-icon/
/// </summary>
[InitializeOnLoad]
public static class HierarchyWindowGameObjectIcon
{
    const string IgnoreIcons = "d_GameObject Icon, d_Prefab Icon, sv_label_0";

    static HierarchyWindowGameObjectIcon()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;

        //EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCallback;
    }

    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID);

        var content = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);

        if (content.image != null && !IgnoreIcons.Contains(content.image.name))
        {
            GUI.DrawTexture(new Rect(selectionRect.xMax - 16, selectionRect.yMin, 16, 16), content.image);
        }
        else if (content.image != null && content.image.name == "sv_label_0")
        {
            Rect offsetRect = new Rect(selectionRect.position, selectionRect.size);
            EditorGUI.DrawRect(selectionRect, Color.white);
            EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = Color.black },
                fontStyle = FontStyle.Bold
            });
        }

    }

}