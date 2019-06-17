using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverInimigo : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private ControleJogo controleJogo;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        controleJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleJogo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        while(target != null)
        {
            navMeshAgent.SetDestination(target.position);
            if (navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance <= 8)
            {   
                controleJogo.RemoverTempo();
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
