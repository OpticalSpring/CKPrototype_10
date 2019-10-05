using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeStopValue;
    public int playTime_a, playTime_b, playTime_c, playTime_d;
    float playTime;
    PauseManager pauseManager;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseManager = GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeStopDecrease();
        TimeFlow();
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
        playTime += Time.deltaTime*100;
        playTime_d += (int)playTime;
        playTime -= (int)playTime;
        if(playTime_d >= 100)
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
    


}
