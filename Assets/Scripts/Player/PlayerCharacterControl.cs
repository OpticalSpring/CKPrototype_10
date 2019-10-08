using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    public Vector2 inputValue;
    public GameObject mainCam;
    public Vector3 targetPos;
    GameManager gameManager;
    PlayerState playerState;
    CapsuleCollider playerCap;
    PlayerAniControl playerAni;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerState = GetComponent<PlayerState>();
        playerCap = GetComponent<CapsuleCollider>();
        playerAni = GetComponent<PlayerAniControl>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeResum();
        if (playerState.state == PlayerState.State.Sit)
        {
            playerCap.center = new Vector3(0, 0.5f, 0);
            playerCap.height = 1;
        }
        else
        {
            playerCap.center = new Vector3(0, 1f, 0);
            playerCap.height = 2;
        }
    }

    void FixedUpdate()
    {
        Move();
        Shot();
    }

    void TimeResum()
    {
        if (gameManager.timeStopValue > 0 && gameManager.timeStopValue < 14.5f)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                gameManager.timeStopValue = 0;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon") && other.GetComponent<Weapon>().state == 0)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (gameManager.timeStopValue <= 0)
                {
                    if (playerState.weapon == null)
                    {
                        playerState.weapon = other.gameObject;
                        playerState.weapon.GetComponent<Weapon>().state = 1;
                    }
                    else
                    {
                        playerState.weapon.transform.position = other.gameObject.transform.position;
                        playerState.weapon.transform.parent = null;
                        playerState.weapon.GetComponent<Weapon>().state = 0;
                        playerState.weapon = other.gameObject;
                        playerState.weapon.GetComponent<Weapon>().state = 1;
                    }
                    other.transform.parent = playerState.weaponPoint;
                    other.transform.localPosition = new Vector3(0, 0, 0);
                    other.transform.localEulerAngles = new Vector3(0, 0, 0);


                    gameManager.TimeStop();
                }
            }
        }
    }
    public void AniShot()
    {
        playerState.weapon.transform.parent = null;
        playerState.weapon.transform.LookAt(playerState.weaponTargetPos);
        playerState.weapon.GetComponent<Weapon>().state = 2;
        playerState.weapon.GetComponent<Rigidbody>().isKinematic = false;
        playerState.weapon.GetComponent<Rigidbody>().AddForce(playerState.weapon.transform.forward * playerState.shotPower, ForceMode.Impulse);
        playerState.weapon = null;
    }

    IEnumerator DelayShot()
    {
        yield return new WaitForSeconds(0.2f);
        AniShot();

    }

    void Shot()
    {
        if (playerState.state == PlayerState.State.Aim && Input.GetMouseButtonUp(0))
        {
            playerAni.aniState = 3;
            StartCoroutine("DelayShot");
        }
        else if (Input.GetMouseButtonUp(0) && playerState.weapon != null)
        {
            playerAni.aniState = 4;
            Collider[] colliderHits = Physics.OverlapSphere(playerState.weaponPoint.position, 3);
            int count = 0;
            for (int i = 0; i < colliderHits.Length; i++)
            {
                if (colliderHits[i].CompareTag("Enemy"))
                {
                    playerState.weapon.GetComponent<Weapon>().Attack(colliderHits[i].gameObject);
                    count++;
                }
            }
            if (count > 0)
            {
                playerState.weapon.GetComponent<Weapon>().Hit(playerState.weaponPoint.position);
            }
        }
    }
    void Turn(GameObject obj, Vector3 target, float speed)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), speed * Time.deltaTime);

    }

    void Move()
    {

        inputValue.x = Input.GetAxis("Horizontal");
        inputValue.y = Input.GetAxis("Vertical");
        mainCam.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
        playerAni.movement = 0;
        playerAni.aniState = 0;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            playerAni.aniState = 1;
            playerState.state = PlayerState.State.Sit;
        }
        else if (playerState.state == PlayerState.State.Idle)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                mainCam.transform.GetChild(1).transform.localPosition = new Vector3(inputValue.x * 10, 0, inputValue.y * 10);
                Vector3 targetY = mainCam.transform.GetChild(1).transform.position;
                targetY.y = gameObject.transform.position.y;
                mainCam.transform.GetChild(1).transform.position = targetY;
                targetPos = mainCam.transform.GetChild(1).transform.position;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerAni.movement = 2;
                    // gameObject.transform.Translate(Vector3.forward * playerState.runSpeed * Time.deltaTime);
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.runSpeed * Time.deltaTime);
                }
                else
                {
                    playerAni.movement = 1;
                    // gameObject.transform.Translate(Vector3.forward * playerState.normalSpeed * Time.deltaTime);
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.normalSpeed * Time.deltaTime);
                }
            }
        }
        else if (playerState.state == PlayerState.State.Aim)
        {
            playerAni.aniState = 2;
            mainCam.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 10);
            Vector3 targetY = mainCam.transform.GetChild(1).transform.position;
            targetY.y = gameObject.transform.position.y;
            mainCam.transform.GetChild(1).transform.position = targetY;
            targetPos = mainCam.transform.GetChild(1).transform.position;
            //Vector3 newAimPos = inputValue.x * playerState.aimSpeed * Time.deltaTime, 0, inputValue.y * playerState.aimSpeed * Time.deltaTime;
            //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, playerState.aimSpeed * Time.deltaTime);
            gameObject.transform.Translate(new Vector3(-inputValue.x * playerState.aimSpeed * Time.deltaTime, 0, -inputValue.y * playerState.aimSpeed * Time.deltaTime));
        }
        else if (playerState.state == PlayerState.State.Sit)
        {
            playerState.state = PlayerState.State.Idle;
        }
        Turn(gameObject, mainCam.transform.GetChild(0).transform.position, 10000);
        Turn(gameObject.transform.GetChild(0).gameObject, targetPos, playerState.rotateSpeed);
    }

    public void Hit()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
        gameObject.transform.GetChild(0).parent = null;
        Destroy(gameObject);
    }
}
