using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager1 : MonoBehaviour
{
    public GameObject[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int count = 0;
            for (int i = 0; i < enemy.GetLength(0); i++)
            {
                if(enemy[i] == null)
                {
                    count++;
                }
            }

            if (count >= 5)
            {
                GameObject.Find("GameManager").GetComponent<ResultManager>().SetRusult();
                SceneManager.LoadSceneAsync(2);
            }
        }
    }
}
