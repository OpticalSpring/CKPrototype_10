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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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

}
