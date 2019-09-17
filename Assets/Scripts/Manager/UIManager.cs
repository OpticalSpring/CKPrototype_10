using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    public Image TimeUI;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        TimeUIUpdate();
    }

    void TimeUIUpdate()
    {
        if(gameManager.timeStopValue > 0)
        {
            TimeUI.gameObject.SetActive(true);
            TimeUI.fillAmount = gameManager.timeStopValue / 15;
        }
        else
        {
            TimeUI.gameObject.SetActive(false);
        }
    }
}
