using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    [Header("General")]
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    static float speed = 100;
    public float hp = 100;
    public float max_hp = 800;
    static float Def = 30;
    [Header("Unity Setup Fields")]
    public Image healthbar;
    public string PlayerTag = "Player";
    public float range = 15f;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Transform Target;
    private Transform target;
    public float TurnSpeed = 10f;
    public int DeadEnemyCnt = 0;
    private int wavepointIndex = 0;
    static float str = 100;
    bool Die = false;
    // Use this for initialization
    public float STR
    {
        get
        {
            return str;
        }
        set
        {
            str = value;
        }
    }
    public float DEF
    {
        get
        {
            return Def;
        }
        set
        {
            Def = value;
        }
    }
    public float SPEED
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    public Enemys enemys;
    void Start()
    {
        InvokeRepeating("EnemyUpdateTarget", 0f, 0.5f);
        target = WayPoints.points[0];
        switch (enemys)
        {
            case Enemys.ENEMY:
                str = 100;
                Def = 45;
                max_hp = 150;
                hp = max_hp;
                speed = 10;
                break;
            case Enemys.MIDDLEBOSS:
                max_hp = 200;
                hp = max_hp;
                str = 150;
                Def = 100;
                speed = 15;
                break;
            case Enemys.FINALBOSS:
                max_hp = 300;
                hp = max_hp;
                str = 200;
                Def = 105;
                speed = 25;
                break;
            default:
                break;
        }
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
            Debug.Log("NEXTPOSITION");
        }
        HeadMove();
        Vector3 vec = target.position - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        transform.rotation = q;
        if (hp <= 0)
        {
            Die = true;
            GameManager.GiveBalance(GameManager.EnemyKillreward);
            Destroy(gameObject);
        }
    }
    void HeadMove()
    {
        if (Target == null)
        {
            return;
        }
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        if (fireCountDown <= 0f && Target != null)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }
    void GetNextWaypoint()
    {
        if (wavepointIndex >= WayPoints.points.Length - 1)
        {
            DeadEnemyCnt++;
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    }
    public void Hurt(float damage)
    {
        if (!Die)
        {
            hp -= (damage - Def);
            healthbar.fillAmount = hp / max_hp;
        }
    }
    void EnemyUpdateTarget()
    {
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Turret");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in Player)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            Target = nearestEnemy.transform;
        }
        else
        {
            Target = null;
        }
    }
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.name = "EnemyBullet";
        if (bullet != null)
            bullet.Seek(Target, str);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnemyTriggerEnter");
        switch (other.name)
        {
            case "R1":
                transform.Rotate(0, 90f, 0);
                break;
            case "R2":
                transform.Rotate(0, 90f, 0);
                break;
            case "R3":
                transform.Rotate(0, -90f, 0);
                break;
            case "R4":
                transform.Rotate(0, -90f, 0);
                break;
            case "R5":
                transform.Rotate(0, -90f, 0);
                break;
            case "R6":
                transform.Rotate(0, 90f, 0);
                break;
            case "R7":
                transform.Rotate(0, 90f, 0);
                break;
            case "R8":
                transform.Rotate(0, -90f, 0);
                break;
            case "R9":
                transform.Rotate(0, 90f, 0);
                break;
            case "R10":
                transform.Rotate(0, 90f, 0);
                break;
            case "R11":
                transform.Rotate(0, 90f, 0);
                break;
            case "R12":
                transform.Rotate(0, -90f, 0);
                break;
            case "R13":
                transform.Rotate(0, -90f, 0);
                break;
            case "R14":
                transform.Rotate(0, 90f, 0);
                break;
            case "R15":
                transform.Rotate(0, -90f, 0);
                break;
            case "R16":
                transform.Rotate(0, -90f, 0);
                break;
        }
    }
}
