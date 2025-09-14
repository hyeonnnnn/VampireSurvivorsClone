using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveDir = Vector2.zero;
    private float speed = 5.0f;

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 dir = _moveDir * speed * Time.deltaTime;
        transform.position += dir;
    }

}