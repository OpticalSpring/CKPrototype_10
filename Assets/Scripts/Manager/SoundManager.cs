using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject target;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    

    public void SoundPlay(int i, int j)
    {
        gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Play();
    }

    public void SoundStop(int i, int j)
    {
        StartCoroutine(I1(i, j));
    }

    public void SoundPause(int i, int j)
    {
        gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().Pause();
    }

    public void RandomPlay(int i, int x, int y)
    {
        int j = Random.Range(x, y + 1);
        SoundPlay(i, j);
    }

    public void SoundVolumeSet(int i, int j, float k)
    {
        gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = k;
    }



    IEnumerator I1(int i, int j)
    {
        for (int k = 100; k > 0; --k)
        {
            gameObject.transform.GetChild(i).GetChild(j).gameObject.GetComponent<AudioSource>().volume = (float)k / 100;

            yield return new WaitForSeconds(0.01f);
        }
    }
}