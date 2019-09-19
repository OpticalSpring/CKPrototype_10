using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float normalSpeed;
    public float runSpeed;
    public float rotateSpeed;

    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Enemy.State.Idle:
                break;
            case Enemy.State.Patrol:
                break;
            case Enemy.State.Chase:
                break;
            case Enemy.State.Attack:
                break;
        }
    }

    public void Hit()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
        gameObject.transform.GetChild(0).parent = null;
        Destroy(gameObject);
    }
}
