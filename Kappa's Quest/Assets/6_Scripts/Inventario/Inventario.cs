using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventario : MonoBehaviour
{
    [SerializeField]List<Item> itens = new List<Item>();
    [SerializeField]float QuantidadeMaxima = 5;
    [SerializeField] float dinheiro = 0;
    Action<Item> itemAdicionado;
    Action<Item> itemRetirado;

    public Equipavel capacete;
    public Equipavel bracelete;
    public Equipavel armadura;
    public Equipavel calca;
    public Equipavel anel;

    public bool AdicionarItem(Item item)
    {
        if (itens.Count == QuantidadeMaxima - 1)
        {
            return false;
        }

        itens.Add(item);
        itemAdicionado?.Invoke(item);
        return true;
    }

    public void RemoverItem(Item item)
    {
        itens.Remove(item);
        itemRetirado?.Invoke(item);
    }

    public bool Pagar(float valor)
    {
        if(valor <= dinheiro)
        {
            dinheiro -= valor;
            return true;
        }

        return false;
    }

    public float GetDinheiro()
    {
        return dinheiro;
    }
    public  void AdicionarDinheiro(int Tdinheiro)
    {
        dinheiro += Tdinheiro;
    }

    /// <summary>
    /// Equipa apenas se já não houver algo equipado.
    /// </summary>
    /// <param name="equipavel"></param>
    public void TentarEquipar(Equipavel equipavel)
    {
        //Mal feito.
        switch (equipavel.tipo)
        {
            case TipoDeEquipaveis.Anel:
                if (anel == null) anel = equipavel;
                break;
            case TipoDeEquipaveis.Armadura:
                if (armadura == null) armadura = equipavel;
                break;
            case TipoDeEquipaveis.Bracelete:
                if (bracelete == null) bracelete = equipavel;
                break;
            case TipoDeEquipaveis.Calca:
                if (calca == null) calca = equipavel;
                break;
            case TipoDeEquipaveis.Capacete:
                if (capacete == null) capacete = equipavel;
                break;
        }
    }

}

public enum TipoDeEquipaveis
{
    Capacete,
    Bracelete,
    Armadura,
    Calca,
    Anel
}
