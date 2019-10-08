using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerState playerState;
    public Image timeUI;
    public Text[] timeText;
    public Image enemyType;
    public GameObject weaponType;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
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
            timeUI.gameObject.SetActive(true);
            timeUI.fillAmount = gameManager.timeStopValue / 15;
        }
        else
        {
            timeUI.gameObject.SetActive(false);
        }
        
        timeText[0].text = string.Format("{0:D2} : {1:D2} : {2:D2} : {3:D2}", gameManager.playTime_a, gameManager.playTime_b, gameManager.playTime_c, gameManager.playTime_d);
        timeText[1].text = string.Format("{0:D2} : {1:D2} : {2:D2} : {3:D2}", gameManager.playTime_a, gameManager.playTime_b, gameManager.playTime_c, gameManager.playTime_d);
        if (playerState.weapon != null)
        {
            weaponType.SetActive(true);
            switch (playerState.weapon.GetComponent<Weapon>().type)
            {
                case 0:
                    enemyType.color = new Color(1, 0, 0, 1);
                    weaponType.transform.GetChild(0).gameObject.SetActive(true);
                    weaponType.transform.GetChild(1).gameObject.SetActive(false);
                    weaponType.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 1:
                    enemyType.color = new Color(0, 1, 0, 1);
                    weaponType.transform.GetChild(0).gameObject.SetActive(false);
                    weaponType.transform.GetChild(1).gameObject.SetActive(true);
                    weaponType.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 2:
                    enemyType.color = new Color(0, 0, 1, 1);
                    weaponType.transform.GetChild(0).gameObject.SetActive(false);
                    weaponType.transform.GetChild(1).gameObject.SetActive(false);
                    weaponType.transform.GetChild(2).gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            enemyType.color = new Color(1, 1, 1, 1);
            weaponType.SetActive(false);
        }
    }


}
