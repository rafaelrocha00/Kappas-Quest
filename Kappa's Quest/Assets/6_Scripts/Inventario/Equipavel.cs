using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipavel", fileName = "New Equipavel")]
public class Equipavel : Item
{
    public CharacterStats statsToAdd;
    public TipoDeEquipaveis tipo;

    public void Equipar(CharacterToken personagem)
    {
        personagem.AddStats(statsToAdd);
    }

    public void Desequipar(CharacterToken personagem)
    {
        personagem.TakeStats(statsToAdd);
    }
}
