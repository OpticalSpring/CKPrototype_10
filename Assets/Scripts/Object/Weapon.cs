using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool use;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
      
            if (use == true)
            {
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
            }
        
    }
}
