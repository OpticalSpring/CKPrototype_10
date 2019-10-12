using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public int state;
    public int type;
    public int distance;
    GameManager gameManager;
    public GameObject effect;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.timeStopValue > 0)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Outline>().OutlineWidth = 2;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Outline>().OutlineWidth = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == 2)
        {

            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit:" + other.name);
                Attack(other.gameObject);
                Hit(gameObject.transform.position);
            }
            else if (other.CompareTag("Wall"))
            {
                Debug.Log("Hit:" + other.name);
                Hit(gameObject.transform.position);
            }
        }
    }


    public void Hit(Vector3 hit)
    {
        Collider[] colliderHits = Physics.OverlapSphere(hit, distance);

        for (int i = 0; i < colliderHits.Length; i++)
        {
            if (colliderHits[i].CompareTag("Enemy"))
            {
                colliderHits[i].GetComponent<Enemy>().TrackingStart(hit);
            }
        }
        GameObject player = GameObject.Find("Player");
        GameObject temp = Instantiate(effect);
        temp.transform.position = gameObject.transform.position;
        temp.transform.position += new Vector3(0, 1f, 0);
        temp.transform.LookAt(player.transform.position + new Vector3(0, 1, 0));
        Destroy(temp, 10);
        Destroy(gameObject);
    }

    public void Attack(GameObject target)
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(1, 0, 0, 1);
        target.GetComponent<Enemy>().Hit(type);
    }
}
