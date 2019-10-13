using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAniControl : MonoBehaviour
{
    public Animator ani;
    NavMeshAgent agent;
    public int aniState;
    public float movement;
    public float movement_s;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Blending();
        AnimationSet();
    }


    void Blending()
    {
        movement = agent.speed;
        movement_s = Mathf.Lerp(movement_s, movement, Time.deltaTime * 5);
    }

    void AnimationSet()
    {
        ani.SetInteger("AniState", aniState);
        ani.SetFloat("Movement", movement_s);
    }
}
