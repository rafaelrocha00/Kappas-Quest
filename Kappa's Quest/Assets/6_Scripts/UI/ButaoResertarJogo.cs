using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButaoResertarJogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Ativar);
    }

    void Ativar()
    {
        C_Jogo.instancia.Perder();
    }
}
