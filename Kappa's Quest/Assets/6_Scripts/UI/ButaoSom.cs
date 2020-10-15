using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButaoSom : MonoBehaviour
{
    Button butao;
    [SerializeField] AudioClip som;
    void Start()
    {
        butao = GetComponent<Button>();
        butao.onClick.AddListener(Ativar);
    }

    void Ativar()
    {
        if(som != null)
        {
            SFX.instancia.MostrarSom(som);

        }
        else
        {
            SFX.instancia.MostrarSomDeVoltar();
        }
    }
    
}
