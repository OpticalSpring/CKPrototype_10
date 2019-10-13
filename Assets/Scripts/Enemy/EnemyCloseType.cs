using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloseType : Enemy
{
    


    protected override void Attack()
    {
        if(Vector3.Distance(gameObject.transform.position,player.transform.position) <= attackDistance)
        {
            player.GetComponent<PlayerCharacterControl>().Hit();
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundIns(4, 2, gameObject.transform.position);
            enemyAni.ani.SetInteger("AniState", 2);
        }
    }
}
