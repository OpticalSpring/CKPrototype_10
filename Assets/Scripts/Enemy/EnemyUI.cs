using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    Enemy enemy;
    public GameObject UIObject;
    public GameObject warningObject1;
    public GameObject warningObject2;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemy.state)
        {
            case Enemy.State.Patrol:
                if (enemy.patrolStateTime > 0)
                {
                    UIObject.SetActive(true);
                    warningObject1.SetActive(true);
                    warningObject1.GetComponent<Image>().fillAmount = enemy.patrolStateTime / enemy.patrolStateTimeMax;
                }
                else
                {
                    UIObject.SetActive(false);
                    warningObject1.SetActive(false);
                    warningObject2.SetActive(false);
                }
                break;
            case Enemy.State.Chase:
                warningObject2.SetActive(true);
                    warningObject1.SetActive(false);
                    warningObject2.GetComponent<Image>().fillAmount = 1 - enemy.chaseStateTime / enemy.chaseStateTimeMax;
                
                break;
            case Enemy.State Attack:
                warningObject2.SetActive(true);
                    warningObject1.SetActive(false);
                    warningObject2.GetComponent<Image>().fillAmount = 1 - enemy.attackStateTime / enemy.attackStateTimeMax;
                
                break;
        }
       // Turn(UIObject, Camera.main.transform.position);
        UIObject.transform.LookAt(Camera.main.transform.position);
    }

    void Turn(GameObject obj, Vector3 target)
    {
        float dz = target.z - obj.transform.position.z;
        float dx = target.x - obj.transform.position.x;

        float rotateDegree = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg;

        obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, Quaternion.Euler(0, rotateDegree, 0), Time.deltaTime);

    }
}
