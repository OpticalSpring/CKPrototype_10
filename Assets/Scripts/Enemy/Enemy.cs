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
    public float attackDistance;
    public Vector3 patrolPoint;
    public float idleStateTime, idleStateTimeMax;
    public float patrolStateTime, patrolStateTimeMax;
    public float chaseStateTime, chaseStateTimeMax;
    public float attackStateTime, attackStateTimeMax;
    public float attackDelayTime, attackDelayTimeMax;

    Vector3 oldPoint, target;
    int patrolState;
    GameManager gameManager;
    GameObject player;
    Camera searchCam;
    CapsuleCollider playerCap;
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Tracking
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerCap = player.GetComponent<CapsuleCollider>();
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
        gameObject.transform.GetChild(1).LookAt(player.transform.position + new Vector3(0, playerCap.center.y, 0));
        if (viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            Debug.DrawRay(gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).forward * searchDistance, Color.red);
            RaycastHit rayHit;
            if (Physics.Raycast(gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).forward * searchDistance, out rayHit))
            {
                if (rayHit.collider.CompareTag("Player"))
                {
                    switch (state)
                    {
                        case State.Idle:
                            idleStateTime += Time.deltaTime;
                            if (idleStateTime > idleStateTimeMax)
                            {
                                idleStateTime = 0;
                                state = State.Chase;
                            }
                            break;
                        case State.Patrol:
                            patrolStateTime += Time.deltaTime;
                            if (patrolStateTime > patrolStateTimeMax)
                            {
                                patrolStateTime = 0;
                                state = State.Chase;
                            }
                            break;
                        case State.Chase:
                            if (chaseStateTime > 0)
                            {
                                chaseStateTime -= Time.deltaTime;
                            }
                            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < attackDistance)
                            {
                                chaseStateTime = 0;
                                state = State.Attack;
                            }
                            break;
                        case State.Attack:
                            gameObject.transform.LookAt(player.transform.position);
                            if (attackStateTime > 0)
                            {
                                attackStateTime -= Time.deltaTime;
                            }
                            attackDelayTime += Time.deltaTime;

                            if (attackDelayTime > attackDelayTimeMax)
                            {
                                attackDelayTime = 0;
                                Attack();
                            }
                            break;
                    }
                }
                else StateChange();
                
            }
            else StateChange();
        }
        else StateChange();
    }

    void StateChange()
    {

        switch (state)
        {
            case State.Idle:
                if (idleStateTime > 0)
                {
                    idleStateTime -= Time.deltaTime;
                }
                break;
            case State.Patrol:
                if (patrolStateTime > 0)
                {
                    patrolStateTime -= Time.deltaTime;
                }
                break;
            case State.Chase:
                chaseStateTime += Time.deltaTime;
                if (chaseStateTime > chaseStateTimeMax)
                {
                    chaseStateTime = 0;
                    state = State.Patrol;
                }
                break;
            case State.Attack:
                attackStateTime += Time.deltaTime;
                if (attackStateTime > attackStateTimeMax)
                {
                    state = State.Patrol;
                }
                if (attackDelayTime > 0)
                {
                    attackDelayTime -= Time.deltaTime;
                }
                break;
        }
    }

    public void TrackingStart(Vector3 target)
    {
        state = State.Tracking;
        this.target = target;
    }

    protected virtual void Attack()
    {
        Debug.Log("Attack");
    }

    protected void Tracking()
    {
        if (Vector3.Distance(gameObject.transform.position, target) < 1)
        {
            state = State.Patrol;
        }
            GetComponent<NavMeshAgent>().SetDestination(target);
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

    protected void Chase()
    {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
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
