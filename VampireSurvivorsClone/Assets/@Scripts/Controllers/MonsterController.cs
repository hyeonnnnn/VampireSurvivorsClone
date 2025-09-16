using System.Collections;
using UnityEngine;

public class MonsterController : CreatureController
{

    public override bool Init()
    {
        if(base.Init())
            return false;

        ObjectType = Define.ObjectType.Monster;
        
        return true;
    }

    void FixedUpdate()
    {
        PlayerController pc = Managers.Object.Player;

        if(pc == null)
            return;

        Vector3 dir = (pc.transform.position - transform.position).normalized;
        Vector3 newPos =  transform.position + dir * _speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(newPos);

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();

        if (target == null)
            return;

        if(_coDotDamage != null)
            StopCoroutine(_coDotDamage);

        _coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target == null)
            return;

        if(_coDotDamage != null)
            StopCoroutine(_coDotDamage);
        _coDotDamage = null;
    }

    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target)
    {
        while(true)
        {
            target.OnDamaged(this, 1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();

        if (_coDotDamage != null)
            StopCoroutine(_coDotDamage);
        _coDotDamage = null;

        Managers.Object.Despawn(this);
    }
}
