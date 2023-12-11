using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_StaticFunc
{
    public static void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public static List<List<T>> Create2DimensionalList<T>(int size, Func<T> createInstance)
    {
        List<List<T>> dimensionalList = new List<List<T>>();

        // Create new 2D list
        for (int x = 0; x < size; x++)
        {
            List<T> tmpGrid = new List<T>();

            for (int y = 0; y < size; y++)
                tmpGrid.Add(createInstance());

            dimensionalList.Add(tmpGrid);
        }
        return dimensionalList;
    }
    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
