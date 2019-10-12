using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SoundPlay(0, 0);
    }
    public void GameStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
