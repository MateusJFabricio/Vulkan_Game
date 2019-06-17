using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_Player : MonoBehaviour
{
    public float speed = 6;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 3.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float controleVelocidadeTemporaria;

    public ControleJogo controleJogo;

    Animator anim;
    int jumpHash;
    int runStateHash;

    // Start is called before the first frame update
    void Start()
    {
        controleJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleJogo>();
        controleVelocidadeTemporaria = 0;
        anim = GetComponent<Animator>();
        jumpHash = Animator.StringToHash("Jump");
        runStateHash = Animator.StringToHash("Base Layer.Run");
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                anim.SetTrigger(jumpHash);
                moveDirection.y = jumpSpeed;
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //Rotate Player
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
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
        while(controleVelocidadeTemporaria >= 0)
        {
            yield return new WaitForSeconds(1);
            controleVelocidadeTemporaria--;
        }
        speed = speed / 2;
        controleJogo.RemoverOverlayVelocidade();
    }

}