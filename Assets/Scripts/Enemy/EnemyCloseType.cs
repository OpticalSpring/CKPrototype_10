using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseType : Enemy
{

    void Update()
    {
        base.TimeStopState();
        switch (state)
        {
            case Enemy.State.Idle:
                break;
            case Enemy.State.Patrol:
                base.Patrol();
                break;
            case Enemy.State.Chase:
                base.Chase();
                break;
            case Enemy.State.Attack:
                break;
            case Enemy.State.Tracking:
                base.Tracking();
                break;
        }
        base.Search();
    }


    protected override void Attack()
    {
        if(Vector3.Distance(gameObject.transform.position,player.transform.position) <= attackDistance)
        {
            player.GetComponent<PlayerCharacterControl>().Hit();
        }
    }
}
