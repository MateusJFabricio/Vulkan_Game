using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Player : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 3.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float controleVelocidadeTemporaria;

    Animator animator;
    int animator_attack, animator_move, animator_hit, animator_die, animator_idle;

    public ControleJogo controleJogo;
    private MoverInimigo moverInimigo;
    private MoverInimigo localMoverInimigo;

    // Start is called before the first frame update
    void Start()
    {
        controleJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleJogo>();
        controleVelocidadeTemporaria = 0;
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        moverInimigo = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<MoverInimigo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        if ((controller.velocity.y != 0) || (controller.velocity.x != 0) && controller.isGrounded)
        {
            animator.SetBool("Mover", true);
            animator.SetBool("Descansar", false);
        }else
        {
            animator.SetBool("Mover", false);
            animator.SetBool("Descansar", true);
        }

        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Descansar", false);
            animator.SetBool("Atacar", true);
            if (localMoverInimigo != null)
            {
                localMoverInimigo.Animacao_Apanhar(true);
                localMoverInimigo = null;
            }

        }
        else
        {
            animator.SetBool("Atacar", false);
            moverInimigo.Animacao_Apanhar(false);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        //Rotate Player
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        //Animator
        //animator.SetBool("Mover", false);
        //animator.SetBool("Atacar", false);
        //animator.SetBool("Apanhar", false);
        //animator.SetBool("Descansar", false);
        //animator.SetBool("Morrer", false);

    }

    void FixedUpdate()
    {
        animator.SetBool("Apanhar", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            animator.SetBool("Apanhar", true);
            localMoverInimigo = other.gameObject.GetComponent<MoverInimigo>();
        }
    }

    public void AdicionarVelocidade()
    {
        controleVelocidadeTemporaria += 10;
        speed *= 2;
        controleJogo.AdicionarOverlayVelocidade();
        StartCoroutine(ControlarVelocidadeTemporaria());
    }

    private IEnumerator ControlarVelocidadeTemporaria()
    {
        while (controleVelocidadeTemporaria >= 0)
        {
            yield return new WaitForSeconds(1);
            controleVelocidadeTemporaria--;
        }
        speed = speed / 2;
        controleJogo.RemoverOverlayVelocidade();
    }
}