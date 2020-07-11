using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour {
    [SerializeField]
    Transform _Destination;
    NavMeshAgent _NavMeshAgent;
	// Use this for initialization
	void Start () {
        _NavMeshAgent = this.GetComponent<NavMeshAgent>();
        if(_NavMeshAgent==null)
        {
            Debug.Log("NavMeshAgent is not added to "+gameObject.name);
        }
        else
        {
            SetDestination();
        }
	}

    private void SetDestination()
    {
        Vector3 targetVector = _Destination.transform.position;
        _NavMeshAgent.SetDestination(targetVector);
    }

    // Update is called once per frame
    void Update () {
        if (transform.position == WayPoints.points[0].position)
            transform.rotation = new Quaternion(transform.rotation.x, 90, transform.rotation.z,10);

    }
}
