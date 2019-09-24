using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int colorType;
    public GameObject colorObj;
    public float normalSpeed;
    public float runSpeed;
    public float rotateSpeed;
    public float searchDistance;
    public Vector3 patrolPoint;


    Vector3 oldPoint;
    int patrolState;
    GameManager gameManager;
    GameObject player;
    Camera searchCam;

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
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        searchCam = GetComponent<Camera>();
        oldPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Search()
    {
        Vector3 viewPos = searchCam.WorldToViewportPoint(player.transform.position);
        gameObject.transform.GetChild(1).LookAt(player.transform.position + new Vector3(0, 1f, 0));
        if (viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            Debug.DrawRay(gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).forward * searchDistance, Color.red);
            
        }
    }

    protected void Patrol()
    {
        if(patrolState == 0)
        {
            if (Vector3.Distance(gameObject.transform.position, patrolPoint) < 1)
            {
                patrolState = 1;
            }
            GetComponent<NavMeshAgent>().SetDestination(patrolPoint);
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, oldPoint) < 1)
            {
                patrolState = 0;
            }
            GetComponent<NavMeshAgent>().SetDestination(oldPoint);
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
