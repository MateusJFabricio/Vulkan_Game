using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Cronometro : MonoBehaviour
{
    public Text txtCronometro;
    public int tempo;

    // Start is called before the first frame update
    void Start()
    {
        txtCronometro.text = tempo.ToString();
        StartCoroutine(IniciarContagem());
    }

    IEnumerator IniciarContagem()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            tempo--;
            txtCronometro.text = tempo.ToString();
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

    public void AdicionarTempo()
    {
        tempo = tempo + 15;
        txtCronometro.text = tempo.ToString();
    }

    public void RemoverTempo()
    {
        tempo -= 30;
        txtCronometro.text = tempo.ToString();
        VerificarSePerdeu();
    }

}
