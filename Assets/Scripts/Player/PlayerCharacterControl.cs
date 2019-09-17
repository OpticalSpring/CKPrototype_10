using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    GameManager gameManager;
    PlayerState playerState;
    public Vector2 inputValue;
    public GameObject mainCam;
    public Vector3 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerState = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeResum();
    }

    void FixedUpdate()
    {
        Move();
        Shot();
    }

    void TimeResum()
    {
        if(gameManager.timeStopValue > 0 && gameManager.timeStopValue < 15)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                gameManager.timeStopValue = 0;
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (gameManager.timeStopValue <= 0)
                {
                    if (playerState.weapon == null)
                {
                    playerState.weapon = other.gameObject;
                }
                else
                {
                    playerState.weapon.transform.position = other.gameObject.transform.position;
                    playerState.weapon.transform.parent = null;
                    playerState.weapon = other.gameObject;
                }
                other.transform.parent = playerState.weaponPoint;
                other.transform.localPosition = new Vector3(0, 0, 0);
                
                    gameManager.TimeStop();
                }
            }
        }
    }


    void Shot()
    {
        if (playerState.state == PlayerState.State.Aim && Input.GetMouseButtonUp(0))
        {
            playerState.weapon.transform.parent = null;
            playerState.weapon.transform.LookAt(playerState.weaponTargetPos);
            playerState.weapon.GetComponent<Weapon>().use = true;
            playerState.weapon.GetComponent<Rigidbody>().isKinematic = false;
            playerState.weapon.GetComponent<Rigidbody>().AddForce(playerState.weapon.transform.forward * playerState.shotPower, ForceMode.Impulse);
            playerState.weapon = null;
        }
    }
    void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;
        
            obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), playerState.rotateSpeed * Time.deltaTime);
        
    }

    void Move()
    {
        
        inputValue.x = Input.GetAxis("Horizontal");
        inputValue.y = Input.GetAxis("Vertical");
            mainCam.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);

        if (playerState.state == PlayerState.State.Idle)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                mainCam.transform.GetChild(1).transform.localPosition = new Vector3(inputValue.x * 10, 0, inputValue.y * 10);
                Vector3 targetY = mainCam.transform.GetChild(1).transform.position;
                targetY.y = gameObject.transform.position.y;
                mainCam.transform.GetChild(1).transform.position = targetY;
                targetPos = mainCam.transform.GetChild(1).transform.position;

                if (Input.GetKey(KeyCode.LeftShift)) {
                    gameObject.transform.Translate(Vector3.forward * playerState.runSpeed * Time.deltaTime);
                }
                else
                {
                gameObject.transform.Translate(Vector3.forward * playerState.normalSpeed * Time.deltaTime);
                }
            }
        }else if(playerState.state == PlayerState.State.Aim)
        {
            mainCam.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 10);
            Vector3 targetY = mainCam.transform.GetChild(1).transform.position;
            targetY.y = gameObject.transform.position.y;
            mainCam.transform.GetChild(1).transform.position = targetY;
            targetPos = mainCam.transform.GetChild(1).transform.position;
            
            gameObject.transform.Translate(new Vector3(inputValue.x*playerState.aimSpeed * Time.deltaTime, 0, inputValue.y * playerState.aimSpeed * Time.deltaTime));
        }

        Turn(gameObject, targetPos);
    }
}
