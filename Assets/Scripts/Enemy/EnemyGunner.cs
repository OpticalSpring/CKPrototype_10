using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunner : Enemy
{
    // Start is called before the first frame update
    

    // Update is called once per frame
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
                break;
            case Enemy.State.Attack:
                break;
        }
                base.Search();
    }
    

    
}
