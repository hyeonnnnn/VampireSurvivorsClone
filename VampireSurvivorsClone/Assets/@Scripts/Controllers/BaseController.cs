using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
    public ObjectType ObjectType { get; protected set; }

    private void Awake()
    {
        Init();
    }

    bool _init = false;
    public virtual bool Init()
    {
        if(_init)
            return false;

        _init = true;
        return true;
    }
}
