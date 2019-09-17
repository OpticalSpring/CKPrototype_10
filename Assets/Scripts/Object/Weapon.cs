using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool use;
    


    private void OnCollisionEnter(Collision collision)
    {
      
            if (use == true)
            {
            Hit();
            }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (use == true)
        {

            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit:" + other.name);
                Hit();
                Attack();
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

    void Attack()
    {

    }
}
