using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float normalSpeed;
    public float aimSpeed;
    public float runSpeed;
    public enum State
    {
        Idle,
        Attack,
        Aim
    }
    public State state;
}
