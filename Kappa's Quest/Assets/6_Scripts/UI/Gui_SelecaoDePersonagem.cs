using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui_SelecaoDePersonagem : MonoBehaviour
{
    CartaoSelecaoDePersonagem SelecionadoAtual;

    public void Selecionar(CartaoSelecaoDePersonagem cartao)
    {
        if(cartao != SelecionadoAtual)
        {
            if(SelecionadoAtual != null)
            SelecionadoAtual.LidarComEstados(false);
            SelecionadoAtual = cartao;
        }    
    }
}
