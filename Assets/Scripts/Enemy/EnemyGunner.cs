using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunner : Enemy
{
    public GameObject bullet;
    public GameObject bulletStartPoint;
    

    protected override void Attack() 
    {
        GameObject temp = Instantiate(bullet);
        temp.transform.position = bulletStartPoint.transform.position;
        temp.transform.rotation = bulletStartPoint.transform.rotation;
    }

    
}
