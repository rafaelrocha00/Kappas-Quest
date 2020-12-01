using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MudarFaseCJogo : MonoBehaviour
{
    [SerializeField] int numeroDafase;
    [SerializeField] bool ativar;
    Button butao;
    private void Start()
    {
        butao = GetComponent<Button>();
        butao.onClick.AddListener(Mudar);
    }
    public void Mudar()
    {
        SFX.instancia.MostrarSomDeBotao();
        C_Jogo.instancia.MudarFase(numeroDafase, ativar);
    }
}
