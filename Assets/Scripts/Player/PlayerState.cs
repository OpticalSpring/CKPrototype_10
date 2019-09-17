using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float normalSpeed;
    public float runSpeed;
    public float aimSpeed;
    public float rotateSpeed;
    public float shotPower;
    public Vector3 velocity;
    public GameObject weapon;
    public Transform weaponPoint;
    public Transform weaponTargetPos;
    
    public enum State
    {
        Idle,
        Attack,
        Aim
    }
    public State state;
    
}
