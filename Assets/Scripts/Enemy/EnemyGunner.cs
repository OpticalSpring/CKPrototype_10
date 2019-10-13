using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunner : Enemy
{
    public GameObject bullet;
    public GameObject bulletStartPoint;
    

    protected override void Attack() 
    {
        enemyAni.aniState = 3;
        StartCoroutine("DelayShot");
    }

    IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(1f);
        GameObject temp = Instantiate(bullet);
        temp.transform.position = bulletStartPoint.transform.position;
        temp.transform.rotation = bulletStartPoint.transform.rotation;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundIns(4, 4, gameObject.transform.position);
    }
}
