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

    protected EnemyAniControl enemyAni;
    Vector3 oldPoint, target;
    int patrolState;
    GameManager gameManager;
    protected GameObject player;
    Camera searchCam;
    CapsuleCollider playerCap;
    NavMeshAgent agent;
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
        agent = GetComponent<NavMeshAgent>();
        enemyAni = GetComponent<EnemyAniControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || colorType > 2)
        {
            return;
        }
        TimeStopState();
        switch (state)
        {
            case Enemy.State.Idle:
                break;
            case Enemy.State.Patrol:
                enemyAni.aniState = 0;
                Patrol();
                break;
            case Enemy.State.Chase:
                enemyAni.aniState = 0;
                Chase();
                break;
            case Enemy.State.Attack:
                Chase();
                break;
            case Enemy.State.Tracking:
                Tracking();
                break;
        }
        Search();
        gameObject.transform.GetChild(0).localEulerAngles = new Vector3(0, 0, 0);
        gameObject.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
    }

    protected void Search()
    {
        Vector3 viewPos = searchCam.WorldToViewportPoint(player.transform.position);
        gameObject.transform.GetChild(1).LookAt(player.transform.position + new Vector3(0, playerCap.center.y, 0));
        if (viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1)
        {
            Debug.DrawRay(gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).forward * searchDistance, Color.red);
            RaycastHit rayHit;
            if (Physics.Raycast(gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).forward * searchDistance, out rayHit, searchDistance))
            {
                if (rayHit.collider.CompareTag("Player"))
                {
                    switch (state)
                    {
                        case State.Patrol:
                        case State.Tracking:
                            agent.speed = normalSpeed;
                            patrolStateTime += Time.deltaTime;
                            if (patrolStateTime > patrolStateTimeMax)
                            {
                                patrolStateTime = 0;
                                state = State.Chase;
                            }
                            break;
                        case State.Chase:
                            agent.stoppingDistance = attackDistance-2;
                            agent.speed = runSpeed;
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
                            agent.speed = 0;
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
                                GameObject.Find("GameManager").GetComponent<ResultManager>().R2Count();
                            }

                            if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance)
                            {
                                attackStateTime = 0;
                                state = State.Chase;
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

                agent.stoppingDistance = 0;
        switch (state)
        {
            case State.Patrol:
            case State.Tracking:
                agent.speed = normalSpeed;

                if (patrolStateTime > 0)
                {
                    patrolStateTime -= Time.deltaTime;
                }
                break;
            case State.Chase:
                agent.speed = runSpeed;
                chaseStateTime += Time.deltaTime;
                if (chaseStateTime > chaseStateTimeMax)
                {
                    chaseStateTime = 0;
                    state = State.Patrol;
                }
                break;
            case State.Attack:
                agent.speed = runSpeed;

                attackStateTime += Time.deltaTime;
                if (attackStateTime > attackStateTimeMax)
                {
                    attackStateTime = 0;
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
        if (state == State.Patrol)
        {
            state = State.Tracking;
            target.y = 0;
            this.target = target;
            agent.SetDestination(target);
        }
    }

    protected virtual void Attack() { }

    protected void Tracking()
    {
        if (Vector3.Distance(gameObject.transform.position, agent.destination) < 4)
        {
            state = State.Patrol;
        }
    }

    protected void Patrol()
    {
        agent.stoppingDistance = 0;
        if(patrolState == 0)
        {
            if (Vector3.Distance(gameObject.transform.position, patrolPoint) < 1)
            {
                patrolState = 1;
            }
            agent.SetDestination(patrolPoint);
        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, oldPoint) < 1)
            {
                patrolState = 0;
            }
            agent.SetDestination(oldPoint);
        }
    }

    protected void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    public void Hit(int type)
    {
        if (type == colorType)
        {
            GameObject.Find("GameManager").GetComponent<ResultManager>().R0Count();
            Debug.Log("TRUETYPE");
            gameObject.transform.GetChild(0).parent = null;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundIns(4, 5,gameObject.transform.position);
            enemyAni.ani.SetInteger("AniState", 10);
            switch (colorType)
            {
                case 0:
                    GameObject.Find("GameManager").GetComponent<ResultManager>().R5Count();
                    break;
                case 1:
                    GameObject.Find("GameManager").GetComponent<ResultManager>().R6Count();
                    break;
                case 2:
                    GameObject.Find("GameManager").GetComponent<ResultManager>().R7Count();
                    break;
            }
            Destroy(gameObject);
        }
        else if (colorType == 3)
        {
            GameObject.Find("GameManager").GetComponent<ResultManager>().R0Count();
            Debug.Log("BossDie");
            gameObject.transform.GetChild(0).parent = null;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundIns(4, 5, gameObject.transform.position);
            Destroy(gameObject);
        }
        else
        {
            enemyAni.aniState = 1;
            enemyAni.ani.SetInteger("AniState", 1);
            Debug.Log("NOTYPE"+type+","+colorType);
            StartCoroutine("DelayBack");
        }
    }

    IEnumerator DelayBack()
    {
        yield return new WaitForSeconds(0.1f);
        enemyAni.aniState = 0;
    }

    protected void TimeStopState()
    {
            if (gameManager.timeStopValue > 0)
            {
                switch (colorType)
                {
                    case 0:
                        colorObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Vector4(1, 0, 0, 1));
                        colorObj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Vector4(1, 0, 0, 1));
                        break;
                    case 1:
                        colorObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Vector4(0, 1, 0, 1));
                        colorObj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Vector4(0, 1, 0, 1));
                        break;
                    case 2:
                        colorObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Vector4(0, 0, 1, 1));
                        colorObj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Vector4(0, 0, 1, 1));
                        break;
                }
            }
            else
            {
                colorObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Vector4(1, 1, 1, 1));
                colorObj.GetComponent<MeshRenderer>().material.SetColor("_Color", new Vector4(1, 1, 1, 1));
            }
        }
    
    
}
