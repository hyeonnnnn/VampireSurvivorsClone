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

        // 인위적으로 삭제
        GameObject.Destroy(go);
    }


    #region 어드레서블
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        // 캐시에 있는지 확인
        if(_resources.TryGetValue(key, out UnityEngine.Object resources))
        {
            return;
        }

        // 리소스 비동기 로딩 시작
        var asyncOperation = Addressables.LoadAssetAsync<T>(key);

        // 로딩이 끝났을 때 실행할 콜백 등록
        asyncOperation.Completed += (op) =>
        {
            if(op.Status == AsyncOperationStatus.Succeeded)
            {
                _resources.Add(key, op.Result); // 캐시에 저장
                callback?.Invoke(op.Result); // 결과 전달
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
