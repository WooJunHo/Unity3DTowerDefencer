using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    private GameObject turretToBuild;
    public GameObject standardTurretPrefab;
    // Use this for initialization
    void Awake()
    {
        if(instance!=null)
        {
            Debug.Log("현재 씬에 빌드매니저가 없습니다.");
            return;
        }
        instance = this;
    }
    
	void Start () { 
        turretToBuild = standardTurretPrefab;
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
