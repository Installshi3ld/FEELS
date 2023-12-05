
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(S_BuildingData))]
public class S_CustomInspector : Editor
{
    // https://learn.unity.com/tutorial/editor-scripting#5c7f8528edbc2a002053b5f9
    // https://docs.unity3d.com/ScriptReference/EditorWindow.html

    int gridSize = 5;

    List<List<bool>> gridBoolean = new List<List<bool>>();
    S_BuildingData myTarget;

    private void OnEnable()
    {
        myTarget = (S_BuildingData)target;

        // Create grid Boolean
        for (int x = 0; x < gridSize; x++)
        {
            List<bool> tmpGrid = new List<bool>();
            for (int y = 0; y < gridSize; y++)
            {
                // Check if element is already checked
                bool tmpObjectFind = false;
                for (int tile = 0; tile < myTarget.tilesCoordinate.Count; tile++)
                {
                    if (myTarget.tilesCoordinate[tile] == new Vector2Int(x - gridSize / 2, y - gridSize / 2))
                    {
                        tmpGrid.Add(true);
                        tmpObjectFind = true;
                    }
                }

                if (!tmpObjectFind)
                    tmpGrid.Add(false);
            }

            gridBoolean.Add(tmpGrid);
        }
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Table of check box
        EditorGUILayout.BeginHorizontal(GUILayout.Width(50));
        {
            for (int x = 0; x < gridSize; x++)
            {
                EditorGUILayout.BeginVertical();

                for (int y = 0; y < gridSize; y++)
                {
                    
                    if (x == 2 && y == 2)
                    {
                            GUI.color = new Vector4(0.98f,0.54f,0.05f,1);
                            EditorGUILayout.Toggle(true);
                    }

                    else
                    {
                        GUI.color = Color.grey;

                        var newState = EditorGUILayout.Toggle(gridBoolean[x][y]);

                        if (newState != gridBoolean[x][y])
                        {
                            gridBoolean[x][y] = newState;

                            if (newState)
                            {
                                myTarget.tilesCoordinate.Add(new Vector2Int(x - gridSize / 2, y - gridSize / 2));
                                EditorUtility.SetDirty(myTarget);
                            }

                            else
                            {

                                int tmpIndex = myTarget.tilesCoordinate.FindIndex(tile => tile.x == x - gridSize / 2 && tile.y == y - gridSize / 2);
                                myTarget.tilesCoordinate.RemoveAt(tmpIndex);
                                EditorUtility.SetDirty(myTarget);
                            }
                        }

                    }
                        
                }

                GUILayout.EndVertical();
            }
        }
        EditorGUILayout.EndVertical();

        

        GUILayout.Space(10); //Create separation

        GUILayout.FlexibleSpace();// create a space that will take all space

        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        {

        }
        EditorGUILayout.EndVertical();



    }
}
