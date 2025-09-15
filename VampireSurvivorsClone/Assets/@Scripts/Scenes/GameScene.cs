using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private GameObject _joystickPrefab;
    [SerializeField] private GameObject _slimePrefab;

    GameObject _joystick;
    GameObject _slime;

    void Start()
    {
        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"[{key}] {count}/{totalCount}");

            if (count == totalCount)
            {
                StartLoaded();
            }
        });

    }

    void StartLoaded()
    {
        GameObject prefab = Managers.Resource.Load<GameObject>("Slime_01.prefab");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
