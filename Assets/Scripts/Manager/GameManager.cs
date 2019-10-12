using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeStopValue;
    public int playTime_a, playTime_b, playTime_c, playTime_d;
    float playTime;
    PauseManager pauseManager;
    public bool gameStart;

    public GameObject[] startingUI;
    public GameObject player;
    public GameObject mainCam;
    public Vector3 cameraStartingPoint; public bool gameOver;
    public bool gameClear;
    public GameObject gameOverUI;
    public GameObject gameClearUI;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseManager = GetComponent<PauseManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameClear == false && gameOver == false)
        {
            TimeStopDecrease();
            TimeFlow();
            GameStateCheck();
            if (gameStart == false && Input.GetKeyDown(KeyCode.Space))
            {
                gameStart = true;
                GameStart();
            }
        }
        else
        {
            ResultSelect();
        }
    }

    void GameStateCheck()
    {
        if (gameOver == false && player == null)
        {
            gameOver = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine("GameOverDelay");
        }
    }

    void ResultSelect()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;

            SceneManager.LoadSceneAsync(0);
        }
    }


    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(5f);
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
    void TimeStopDecrease()
    {
        if (pauseManager.pause == false)
        {
            if (timeStopValue > 0)
            {
                timeStopValue -= Time.fixedDeltaTime;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    void TimeFlow()
    {
        playTime += Time.deltaTime * 100;
        playTime_d += (int)playTime;
        playTime -= (int)playTime;
        if (playTime_d >= 100)
        {
            playTime_d = 0;
            playTime_c++;
        }
        if (playTime_c >= 60)
        {
            playTime_c = 0;
            playTime_b++;
        }
        if (playTime_b >= 60)
        {
            playTime_b = 0;
            playTime_a++;
        }
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
        timeStopValue = 15;
        Debug.Log("TimeStop");
    }


    void GameStart()
    {
        startingUI[0].SetActive(false);
        startingUI[1].SetActive(true);
        StartCoroutine("CameraSet");
    }

    IEnumerator CameraSet()
    {
        for (int i = 0; i < 300; i++)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, cameraStartingPoint, Time.deltaTime);
            Debug.Log(mainCam.transform.localEulerAngles);
            mainCam.transform.localEulerAngles = Vector3.Lerp(mainCam.transform.localEulerAngles, new Vector3(0, 360, 0), Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        player.GetComponent<PlayerCameraControl>().enabled = true;
        player.GetComponent<PlayerCharacterControl>().enabled = true;

    }
}
