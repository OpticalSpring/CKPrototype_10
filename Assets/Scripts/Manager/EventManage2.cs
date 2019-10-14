using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManage2 : MonoBehaviour
{
    public GameObject boss;
    public GameObject clearUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            clearUI.SetActive(true);
            GameObject.Find("GameManager").GetComponent<GameManager>().gameClear = true;
            GameObject.Find("GameManager").GetComponent<ResultManager>().Clear();
        }
    }
}
