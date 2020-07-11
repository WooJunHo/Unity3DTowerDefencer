using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;
    public Color notEnoughMoneyColor;
    private GameObject turret;

    private Renderer rend;
    private Color startColor;
    
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
       
	}
    private void Update()
    {
        if(GameManager.Pause)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
            gameObject.GetComponent<BoxCollider>().enabled = true;
        if(GameObject.Find("MiddleBossAlamText").GetComponent<Text>().enabled|| GameObject.Find("FinalBossAlamText").GetComponent<Text>().enabled || GameObject.Find("ClearUI")!=null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
            gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    void OnMouseDown()
    {
        if (!GameManager.Pause && !GameManager.Clear() && !GameManager.GameOver())
        {
            if (10 <= GameManager.GetBalance())
            {
                GameManager.Buy(10);
                GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
                turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
            }
            else
            {
                GameManager.SetBalance(0);
                Debug.Log("돈이 없어서 더이상 지을수가 없습니다.");
                return;
            }
        }
        else if (GameManager.Pause && GameManager.Clear() && GameManager.GameOver())
            return;
        else
            return;
    }
    void OnMouseEnter()
    {
        if (!GameManager.Pause && !GameManager.Clear() && !GameManager.GameOver())
        {
            if (10 <= GameManager.GetBalance() && !GameManager.Pause)
            {
                rend.material.color = hoverColor;
            }
            else if (GameManager.Pause)
                return;
            else
            {
                rend.material.color = notEnoughMoneyColor; ;
            }
        }
    }
    void OnMouseExit() {
        rend.material.color = startColor;
    }
    // Update is called once per frame
 
}
