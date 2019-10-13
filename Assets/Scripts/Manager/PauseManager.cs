using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool pause;
    public GameObject pauseObject;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseSet();
        PauseSelect();
    }

    void PauseSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.timeStopValue <= 0)
            {
                if (pause == false)
                {
                    pause = true;
                    pauseObject.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    pause = false;
                    pauseObject.SetActive(false);
                    Time.timeScale = 1;
                }
            }
        }
    }

    void PauseSelect()
    {
        if(pause == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
                { 
                SceneManager.LoadSceneAsync(1);
            }else if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
}
