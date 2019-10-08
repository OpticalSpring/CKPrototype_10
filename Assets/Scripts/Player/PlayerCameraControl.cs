using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    GameManager gameManager;
    PlayerState playerState;
    public GameObject mainCam;
    public float camDistance;
    public float nextFOV;
    public float rotationSpeed;
    public Vector3 rotateValue;
    public Quaternion targetRotation;
    public GameObject crossHair;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerState = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        CameraDistanceCheck();
        CameraDistance();
        Zoom();
    }

    void RotateCamera()
    {
        rotateValue.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x = Mathf.Clamp(rotateValue.x, -30f, 70f);
        targetRotation = Quaternion.Euler(rotateValue);

        Quaternion q = targetRotation;
        mainCam.transform.rotation = targetRotation;
    }

    void CameraDistance()
    {
        mainCam.transform.GetChild(0).localPosition = Vector3.Lerp(mainCam.transform.GetChild(0).localPosition, new Vector3(1,0, -camDistance),10* Time.fixedDeltaTime);
    }

    void CameraDistanceCheck()
    {
        RaycastHit rayHit;
        float maxDistance = 5;
        Debug.DrawRay(mainCam.transform.position, -mainCam.transform.forward * maxDistance, Color.red);
        int mask = 1 << 10;  
        mask = ~mask;
        if (Physics.SphereCast(mainCam.transform.position,  0.3f,-mainCam.transform.forward,out rayHit, maxDistance, mask)){
            Vector3 hitPoint = rayHit.point;
            camDistance = Vector3.Distance(hitPoint, mainCam.transform.position) - 1f;
        }
        else
        {
            camDistance = 5;
        }
    }

    void Zoom()
    {
        if (Input.GetMouseButtonDown(1) && playerState.weapon != null && gameManager.timeStopValue <= 0)
        {
            nextFOV = 30;
            playerState.state = PlayerState.State.Aim;
            crossHair.SetActive(true);
        }
        else if((Input.GetMouseButtonUp(1) || playerState.weapon == null) || playerState.state != PlayerState.State.Aim)
        {
            nextFOV = 60;
            playerState.state = PlayerState.State.Idle;
            crossHair.SetActive(false);
        }
        float nowFov = mainCam.transform.GetChild(0).gameObject.GetComponent<Camera>().fieldOfView;
        nowFov = Mathf.Lerp(nowFov, nextFOV, Time.deltaTime * 5);
        mainCam.transform.GetChild(0).gameObject.GetComponent<Camera>().fieldOfView = nowFov;
    }

    
}
