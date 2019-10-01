using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int speed;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit:" + other.name);
        if (other.CompareTag("Player"))
            {
                Attack(other.gameObject);
                Hit(gameObject.transform.position);
            }
        if (other.CompareTag("Wall"))
        {
            Hit(gameObject.transform.position);
        }
    }


    void Hit(Vector3 hit)
    {
        Destroy(gameObject.transform.GetChild(0).gameObject,5);
        gameObject.transform.GetChild(0).parent = null;
        Destroy(gameObject);

    }

    void Attack(GameObject target)
    {
        target.GetComponent<PlayerCharacterControl>().Hit();
    }
}
