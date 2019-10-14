using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    bool ss;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += new Vector3(0, Time.deltaTime * 100, 0);
        if(gameObject.transform.localPosition.y > 1900 && ss == false)
        {
            ss = true;
            SceneManager.LoadSceneAsync(0);
        }
    }
}
