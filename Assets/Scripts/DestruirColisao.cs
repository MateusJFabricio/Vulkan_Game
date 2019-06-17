using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirColisao : MonoBehaviour
{
    public TipoColetavel tipoColetavel;
    public ParticleSystem particle;

    public ControleJogo controleJogo;
    public Mover_Player moverPlayer;

    private AnimationScript animationScript;
    public float tempoEsperaAntesDestruir;

    
    void Start()
    {
        animationScript = GetComponent<AnimationScript>();
        moverPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Mover_Player>();
        controleJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleJogo>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch (tipoColetavel)
            {
                case TipoColetavel.Tempo:
                    controleJogo.AdicionarTempo();
                    break;
                case TipoColetavel.Velocidade:
                    moverPlayer.AdicionarVelocidade();
                    break;
            }

            StartCoroutine(Destruir());            
        }
    }

    private IEnumerator Destruir()
    {
        animationScript.rotationSpeed = 75;
        particle.Play();
        yield return new WaitForSeconds(tempoEsperaAntesDestruir);
        Destroy(gameObject);
    }
}
