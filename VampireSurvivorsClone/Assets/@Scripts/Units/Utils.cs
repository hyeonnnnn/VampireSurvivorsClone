using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if ((name == null || child.name == name) && child.GetComponent<T>() is T result)
                    return result;
            }
        }
        else
        {
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(go.transform);
            while (queue.Count > 0)
            {
                Transform parent = queue.Dequeue();
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i);
                    if ((name == null || child.name == name) && child.GetComponent<T>() is T result)
                        return result;
                    queue.Enqueue(child);
                }
            }
        }
        return null;
    }
}
