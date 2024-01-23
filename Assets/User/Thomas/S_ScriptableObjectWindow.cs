using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_ScriptableObjectWindow : EditorWindow
{
    List<ScriptableObject> currenciesList = new List<ScriptableObject>();
    Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Tools/SO Window")]
    static void InitWindow()
    {
        //Create a new wodow or get existing opened window of type "S_ScriptableObjectWindow"
        S_ScriptableObjectWindow window = GetWindow<S_ScriptableObjectWindow>();

        //change window title
        window.titleContent = new GUIContent("SO Window");

        //Display window
        window.Show();
    }


    void OnEnable()
    {
        // Clear the list when the window is enabled to avoid duplicates
        currenciesList.Clear();

        // Find all instances of S_Currencies in the project
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject currencies = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (currencies != null)
            {
                currenciesList.Add(currencies);
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label("S_Currencies Objects in Project:");

        // Define the scroll position
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Display the list of S_Currencies objects
        foreach (ScriptableObject currencies in currenciesList)
        {
            EditorGUILayout.ObjectField(currencies, typeof(S_Currencies), false);
        }

        // End the scroll view
        EditorGUILayout.EndScrollView();
    }
}
