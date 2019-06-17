using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleJogo : MonoBehaviour
{
    public Text txtCronometro;
    public int tempo;
    private bool overlayVelocidade;
    public bool encontrouCentralComando;

    
    void Start()
    {
        txtCronometro.text = tempo.ToString();
        StartCoroutine(IniciarContagem());
    }

    private void AtualizarOverlayTempo()
    {
        if (!overlayVelocidade)
            txtCronometro.text = tempo.ToString();
        else
            txtCronometro.text = tempo.ToString() + "\n VELOCIDADE x2";
    }

    IEnumerator IniciarContagem()
    {
        while (!encontrouCentralComando)
        {
            yield return new WaitForSeconds(1);
            tempo--;
            AtualizarOverlayTempo();            
            VerificarSePerdeu();
        }
    }

    private void VerificarSePerdeu()
    {
        if (tempo <= 0)
        {
            SceneManager.LoadScene("Gameover");
        }
    }

    public void RemoverTempo()
    {
        tempo = tempo - 15;
        AtualizarOverlayTempo();
    }

    public void AdicionarTempo()
    {
        tempo = tempo + 15;
        AtualizarOverlayTempo();
    }

    public void AdicionarOverlayVelocidade()
    {
        overlayVelocidade = true;
        AtualizarOverlayTempo();
    }

    public void RemoverOverlayVelocidade()
    {
        overlayVelocidade = false;
        AtualizarOverlayTempo();
    }
}
