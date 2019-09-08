using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float normalSpeed;
    public float runSpeed;
    public float aimSpeed;
    public float rotateSpeed;
    public float jumpPower;
    public Vector3 velocity;
    public enum State
    {
        Idle,
        Attack,
        Aim
    }
    public State state;

    private void Update()
    {
        velocity = GetComponent<Rigidbody>().velocity;
        if (velocity.y > 5)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, 5, velocity.z);
            Debug.Log(velocity);
        }
    }
}
