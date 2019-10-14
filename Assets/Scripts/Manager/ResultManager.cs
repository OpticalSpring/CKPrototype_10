using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Text[] fText;
    public Text[] cText;

    public int[] rValue;
    GameObject player;
    Vector3 oldPos;

    public int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (sceneNumber == 1)
        {
            SetRusult();
        }
        if (sceneNumber == 2)
        {
            GetResult();
        }
    }

    // Update is called once per frame
    void Update()
    {
        R3Count();
    }

    public void R0Count()
    {
        rValue[0]++;
    }

    public void R1Count()
    {
        rValue[1]++;
    }

    public void R2Count()
    {
        rValue[2]++;
    }

    public void R3Count()
    {
        if (Vector3.Distance(player.transform.position, oldPos) > 1)
        {
            oldPos = player.transform.position;
            rValue[3]++;
        }
    }

    public void R4Count()
    {
        rValue[4]++;
    }

    public void R5Count()
    {
        rValue[5]++;
    }
    public void R6Count()
    {
        rValue[6]++;
    }
    public void R7Count()
    {
        rValue[7]++;
    }

    public void Clear()
    {
        for (int i = 0; i < 8; i++)
        {
            cText[i].text = ""+rValue[i];
        }
        cText[5].text = string.Format("<color=#ff007e>{0}</color> KILL", rValue[5]);
        cText[6].text = string.Format("<color=#ff007e>{0}</color> KILL", rValue[6]);
        cText[7].text = string.Format("<color=#ff007e>{0}</color> KILL", rValue[7]);
    }

    public void Fail()
    {
        for (int i = 0; i < 8; i++)
        {
            fText[i].text = "" + rValue[i];
        }
        fText[5].text = string.Format("<color=#ff007e>{0}</color> KILL", rValue[5]);
        fText[6].text = string.Format("<color=#ff007e>{0}</color> KILL", rValue[6]);
        fText[7].text = string.Format("<color=#ff007e>{0}</color> KILL", rValue[7]);
    }

    public void GetResult()
    {
        rValue[0] = PlayerPrefs.GetInt("R0");
        rValue[1] = PlayerPrefs.GetInt("R1");
        rValue[2] = PlayerPrefs.GetInt("R2");
        rValue[3] = PlayerPrefs.GetInt("R3");
        rValue[4] = PlayerPrefs.GetInt("R4");
        rValue[5] = PlayerPrefs.GetInt("R5");
        rValue[6] = PlayerPrefs.GetInt("R6");
        rValue[7] = PlayerPrefs.GetInt("R7");
        GetComponent<GameManager>().playTime_a = PlayerPrefs.GetInt("T1");
        GetComponent<GameManager>().playTime_b = PlayerPrefs.GetInt("T2");
        GetComponent<GameManager>().playTime_c = PlayerPrefs.GetInt("T3");
        GetComponent<GameManager>().playTime_d = PlayerPrefs.GetInt("T4");
    }

    public void SetRusult()
    {
        PlayerPrefs.SetInt("R0", rValue[0]);
        PlayerPrefs.SetInt("R1", rValue[1]);
        PlayerPrefs.SetInt("R2", rValue[2]);
        PlayerPrefs.SetInt("R3", rValue[3]);
        PlayerPrefs.SetInt("R4", rValue[4]);
        PlayerPrefs.SetInt("R5", rValue[5]);
        PlayerPrefs.SetInt("R6", rValue[6]);
        PlayerPrefs.SetInt("R7", rValue[7]);
        PlayerPrefs.SetInt("T1", GetComponent<GameManager>().playTime_a);
        PlayerPrefs.SetInt("T2", GetComponent<GameManager>().playTime_b);
        PlayerPrefs.SetInt("T3", GetComponent<GameManager>().playTime_c);
        PlayerPrefs.SetInt("T4", GetComponent<GameManager>().playTime_d);
    }

}
