using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private float thisstr;
    public float speed = 70f;
    public void Seek(Transform _target, float str)
    {
        target = _target;
        thisstr = str;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    void HitTarget()
    {
        Debug.Log(target.tag);
        if (target.tag == "Enemy")
        {
            target.GetComponent<Enemy>().Hurt(thisstr);
            Destroy(gameObject);
        }
        else if (target.tag == "Turret")
        {
            Debug.Log("EnemySStr" + thisstr);
            target.GetComponent<Turret>().Hurt(thisstr);
            Destroy(gameObject);
        }
        else
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(gameObject);
        }

    }
}
