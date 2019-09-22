using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    public int colorType;
    public GameObject colorObj;
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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    protected void TimeStopState()
    {
        if(gameManager.timeStopValue>0)
        {
            switch (colorType)
            {
                case 0:
                    colorObj.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
                    break;
                case 1:
                    colorObj.GetComponent<MeshRenderer>().material.color = new Vector4(0, 0, 1, 1);
                    break;
                case 2:
                    colorObj.GetComponent<MeshRenderer>().material.color = new Vector4(0, 1, 0, 1);
                    break;
            }
        }
        else
        {
            colorObj.GetComponent<MeshRenderer>().material.color = new Vector4(1, 1, 1, 1);
        }
    }
}
