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
        var player = Managers.Resource.Instantiate("Slime_01.prefab");
        player.AddComponent<PlayerController>();

        var snake = Managers.Resource.Instantiate("Snake_01.prefab");
        var goblin = Managers.Resource.Instantiate("Goblin_01.prefab");
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";
        Camera.main.GetComponent<CameraController>().Target = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
