using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    private void Update()
    {
        if(Target != null)
        {
            transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        }
    }
}
