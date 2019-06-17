using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirColisao : MonoBehaviour
{

    public ParticleSystem particle;
    private AnimationScript animationScript;
    public float tempoEsperaAntesDestruir;

    // Start is called before the first frame update
    void Start()
    {
        animationScript = GetComponent<AnimationScript>();
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
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
