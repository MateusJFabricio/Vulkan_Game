using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverInimigo : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while(target != null)
        {
            
            //navMeshAgent.SetDestination(target.position);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
