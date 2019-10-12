using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniControl : MonoBehaviour
{
    Animator ani;
    public int aniState;
    public float movement;
    public float movement_s;
    public float horizontal;
    public float vertical;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Blending();
        AnimationSet();
    }


    void Blending()
    {
        movement_s = Mathf.Lerp(movement_s, movement, Time.deltaTime * 5);
    }

    void AnimationSet()
    {
        ani.SetInteger("AniState", aniState);
        ani.SetFloat("Movement", movement_s);
        ani.SetFloat("Horizontal",horizontal);
        ani.SetFloat("Vertical", vertical);
    }
}
