using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("General")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;
    public float hp = 120;
    public float maxhp = 120;
    [Header("Unity Setup Fields")]
    public Image healthbarbg;
    public Image healthbar;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float TurnSpeed = 10f;
    static float Def = 80;
    static float str = 120;
    public GameObject bulletPrefab;
    public Transform firePoint;
    bool Die = false;
    public static float STR
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
    public static float DEF
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
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
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
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    void Update()
    {
       
        if (target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
        if(hp<=0)
        {
            Die = true;
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bulletGO.name = "PlayerBullet";
        if (bullet != null)
            bullet.Seek(target,str);
    }
    public void Hurt(float damage)
    {
        if (!Die)
        {
            hp -= (damage - Def);
            healthbar.fillAmount = hp / maxhp;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(other.name);
        }
    }
}
