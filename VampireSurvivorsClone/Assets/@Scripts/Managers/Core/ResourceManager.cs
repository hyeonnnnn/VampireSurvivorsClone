using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class ResourceManager
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if(_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            return resource as T;
        }

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>(key);
        if(prefab == null)
        {
            Debug.Log($"[{key}] is not loaded");
            return null;
        }

        // Pooling
        if (pooling)
        {
            return Managers.Pool.Pop(prefab);
        }

        GameObject go = UnityEngine.Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if(go == null)
        {   
            return;
        }

        if(Managers.Pool.Push(go))
        {
            return;
        }

        // ���������� ����
        GameObject.Destroy(go);
    }


    #region ��巹����
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        // ĳ�ÿ� �ִ��� Ȯ��
        if(_resources.TryGetValue(key, out UnityEngine.Object resources))
        {
            return;
        }

        // ���ҽ� �񵿱� �ε� ����
        var asyncOperation = Addressables.LoadAssetAsync<T>(key);

        // �ε��� ������ �� ������ �ݹ� ���
        asyncOperation.Completed += (op) =>
        {
            if(op.Status == AsyncOperationStatus.Succeeded)
            {
                _resources.Add(key, op.Result); // ĳ�ÿ� ����
                callback?.Invoke(op.Result); // ��� ����
            }
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            int loadCount = 0;
            int totalCount = op.Result.Count;

            foreach(var location in op.Result)
            {
                LoadAsync<T>(location.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(location.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }
    #endregion
}
