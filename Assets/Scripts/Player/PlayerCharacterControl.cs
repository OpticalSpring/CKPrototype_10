using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    PlayerState playerState;
    public Vector2 inputValue;
    public GameObject mainCam;
    public Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
                targetPos = mainCam.transform.GetChild(1).transform.position - gameObject.transform.position;

                
                gameObject.transform.Translate(Vector3.forward * playerState.normalSpeed * Time.deltaTime);
            }
        }else if(playerState.state == PlayerState.State.Aim)
        {
            mainCam.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 10);
            Vector3 targetY = mainCam.transform.GetChild(1).transform.position;
            targetY.y = gameObject.transform.position.y;
            mainCam.transform.GetChild(1).transform.position = targetY;
            targetPos = mainCam.transform.GetChild(1).transform.position - gameObject.transform.position;
            
            gameObject.transform.Translate(new Vector3(inputValue.x*playerState.aimSpeed * Time.deltaTime, 0, inputValue.y * playerState.aimSpeed * Time.deltaTime));
        }
    gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(targetPos), Time.deltaTime* 10);
    }
}
