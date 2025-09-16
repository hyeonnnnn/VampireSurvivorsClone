using Unity.VisualScripting;
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
        var player = Managers.Object.Spawn<PlayerController>();

        for (int i = 0; i < 10; i++)
        {
            MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
            mc.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        }

        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
