using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int state;
    

    private void OnCollisionEnter(Collision collision)
    {
      
            if (state == 2)
            {
            Hit();
            }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == 2)
        {

            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit:" + other.name);
                Hit();
                Attack(other.gameObject);
            }
            else if (other.CompareTag("Wall"))
            {
                Debug.Log("Hit:" + other.name);
                Hit();
            }

        }
    }


    void Hit()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
    }

    void Attack(GameObject target)
    {
        target.GetComponent<Enemy>().Hit();
    }
}
