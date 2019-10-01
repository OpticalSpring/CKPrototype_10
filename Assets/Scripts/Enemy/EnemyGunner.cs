﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunner : Enemy
{
    public GameObject bullet;
    public GameObject bulletStartPoint;

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
        GameObject temp = Instantiate(bullet);
        temp.transform.position = bulletStartPoint.transform.position;
        temp.transform.rotation = bulletStartPoint.transform.rotation;
    }

    
}
