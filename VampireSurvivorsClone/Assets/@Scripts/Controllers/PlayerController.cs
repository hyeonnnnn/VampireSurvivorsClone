using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : CreatureController
{
    private Vector2 _moveDir = Vector2.zero;
    private float _speed = 5.0f;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

   void Start()
    {
        Managers.Game.OnMoveDirChanged += HandleOnMoveChanged;
    }

    void OnDestroy()
    {
        if(Managers.Game != null)
        {
            Managers.Game.OnMoveDirChanged -= HandleOnMoveChanged;
        }
    }

    void HandleOnMoveChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 dir = _moveDir * _speed * Time.deltaTime;
        transform.position += dir;
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }

}