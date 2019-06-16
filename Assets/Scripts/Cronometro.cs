using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    public Text txtCronometro;
    private int tempo;
    public bool vivo;

    // Start is called before the first frame update
    void Start()
    {
        vivo = true;
        tempo = 60;
        txtCronometro.text = tempo.ToString();
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (vivo)
        {
            yield return new WaitForSeconds(1);
            tempo--;
            txtCronometro.text = tempo.ToString();
        }
    }
}
