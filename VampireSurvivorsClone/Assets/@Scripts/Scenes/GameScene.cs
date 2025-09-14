using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private GameObject _joystickPrefab;
    [SerializeField] private GameObject _slimePrefab;

    GameObject _joystick;
    GameObject _slime;

    void Start()
    {
        _joystick = Instantiate(_joystickPrefab);
        _slime = Instantiate(_slimePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
