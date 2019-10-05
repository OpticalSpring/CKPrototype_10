using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
