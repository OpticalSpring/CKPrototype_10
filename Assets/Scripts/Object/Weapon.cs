using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int state;
    public int type;
    public int distance;
    
    private void OnTriggerEnter(Collider other)
    {
        if (state == 2)
        {

            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit:" + other.name);
                Hit(gameObject.transform.position);
                Attack(other.gameObject);
            }
            else if (other.CompareTag("Wall"))
            {
                Debug.Log("Hit:" + other.name);
                Hit(gameObject.transform.position);
            }

        }
    }


    void Hit(Vector3 hit)
    {
        Collider[] colliderHits = Physics.OverlapSphere(hit, distance);

        for(int i = 0; i < colliderHits.Length; i++)
        {
            if (colliderHits[i].CompareTag("Enemy"))
            {
                colliderHits[i].GetComponent<Enemy>().TrackingStart(hit);
            }
        }
        
    }

    void Attack(GameObject target)
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
        target.GetComponent<Enemy>().Hit(type);
    }
}
