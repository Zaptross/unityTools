// Author: Matthew Price
// Created: 15/10/2019
// Licence: Standard MIT Licence

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class GroupControl : MonoBehaviour
{
    [MenuItem("GameObject/Group Selection %G", false, 0)]
    static void GroupSelection()
    {
        GameObject group = new GameObject();

        Transform avgParent = group.transform.parent;
        List<Transform> parents = new List<Transform>();
        List<int> parentCounts = new List<int>();
        Vector3 avgPos = Vector3.zero;

        foreach (GameObject obj in Selection.gameObjects)
        {
            if (parents.Count > 0)
            {
                // If it's not in the list, add it
                if (!parents.Contains(obj.transform.parent))
                {
                    parents.Add(obj.transform.parent);
                    parentCounts.Add(1);
                }
                else
                { // If it's there, tally it
                    parentCounts[parents.IndexOf(obj.transform.parent)]++;
                }
            }

            avgPos += obj.transform.position;
        }

        int highest = -1;
        for (int i = 0; i < parentCounts.Count; i++)
        {
            if (parentCounts[i] > highest)
            {
                highest = parentCounts[i];
                avgParent = parents[i];
            }
        }

        if (avgParent != group.transform.parent)
        {
            group.transform.parent = avgParent;
        }

        group.transform.position = avgPos / Selection.gameObjects.Length;

        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.parent = group.transform;
        }

        group.name = "New Group";
    }
}
