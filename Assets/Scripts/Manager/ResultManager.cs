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

    public void Clear()
    {
        for (int i = 0; i < 5; i++)
        {
            cText[i].text = ""+rValue[i];
        }
    }

    public void Fail()
    {
        for (int i = 0; i < 5; i++)
        {
            fText[i].text = "" + rValue[i];
        }
    }

    public void GetResult()
    {
        rValue[0] = PlayerPrefs.GetInt("R0");
        rValue[1] = PlayerPrefs.GetInt("R1");
        rValue[2] = PlayerPrefs.GetInt("R2");
        rValue[3] = PlayerPrefs.GetInt("R3");
        rValue[4] = PlayerPrefs.GetInt("R4");
    }

    public void SetRusult()
    {
        PlayerPrefs.SetInt("R0", rValue[0]);
        PlayerPrefs.SetInt("R1", rValue[1]);
        PlayerPrefs.SetInt("R2", rValue[2]);
        PlayerPrefs.SetInt("R3", rValue[3]);
        PlayerPrefs.SetInt("R4", rValue[4]);
    }

}
