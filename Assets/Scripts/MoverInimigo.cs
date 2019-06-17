using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverInimigo : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private ControleJogo controleJogo;
    Animator animator;
    Rigidbody rb;
    int vida = 2;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        controleJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleJogo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(UpdatePath());
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((rb.velocity.x != 0)||(rb.velocity.y != 0))
        {
            animator.SetBool("Correr", true);
            animator.SetBool("Descansar", false);
        }else
        {
            animator.SetBool("Correr", false);
            animator.SetBool("Descansar", true);
        }
    }

    //animator.SetBool("Atacar", false);
    //animator.SetBool("Correr", false);
    //animator.SetBool("Ameacar", false);
    //animator.SetBool("Apanhar", false);
    //animator.SetBool("Morrer", false);
    //animator.SetBool("Descansar", false);

    public void Animacao_Apanhar(bool apanhar)
    {
        animator.SetBool("Apanhar", apanhar);
        vida -= 1;
        if (vida <= 0)
            Animacao_Morrer();
    }

    void Animacao_Morrer()
    {
        animator.SetBool("Morrer", true);
        GetComponent<MoverInimigo>().enabled = false;
        navMeshAgent.Stop();
    }

    private IEnumerator UpdatePath()
    {
        while(target != null)
        {
            navMeshAgent.SetDestination(target.position);
            if (navMeshAgent.remainingDistance != 0 && navMeshAgent.remainingDistance <= 8)
            {
                animator.SetBool("Atacar", true);
                controleJogo.RemoverTempo();
                yield return new WaitForSeconds(1f);
            }else
                animator.SetBool("Atacar", false);
            yield return new WaitForSeconds(0.25f);
        }
    }
}
