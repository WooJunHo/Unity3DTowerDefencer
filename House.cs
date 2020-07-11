using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    public float HP = 300;
    public float Max_HP = 300;
    public Image healthbarbg;
    public Image healthbar;
    float Def = 30;
    public void Hurt(float damage)
    {
        if (HP <= 0)
        {
            HP = 0;
            Destroy(gameObject);
        }
        else
        {
            HP -= (damage - Def);
            healthbar.fillAmount = HP / Max_HP;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggeEnter");
        if (other.CompareTag("Enemy"))
        {
            Hurt(other.GetComponent<Enemy>().STR);
        }
        else if (other.CompareTag("MBoss"))
            Hurt(other.GetComponent<Enemy>().STR);
        else if(other.CompareTag("FBoss"))
            Hurt(other.GetComponent<Enemy>().STR);
    }
}
