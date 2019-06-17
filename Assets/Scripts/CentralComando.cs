using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralComando : MonoBehaviour
{
    private ControleJogo controleJogo;

    // Start is called before the first frame update
    void Start()
    {
        controleJogo = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControleJogo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            controleJogo.encontrouCentralComando = true;
    }
}
