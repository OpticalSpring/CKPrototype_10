using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeStopValue;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        TimeStopDecrease();
    }
    

    void TimeStopDecrease()
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
    
  public void TimeStop()
    {
        Time.timeScale = 0;
        timeStopValue = 15;
        Debug.Log("TimeStop");
    }
    
}
