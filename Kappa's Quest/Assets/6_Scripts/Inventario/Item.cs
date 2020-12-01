using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string nome;
    public Sprite icon;
    public float preco;
    public string descricao;
    public int id;

    public virtual void Ativar()
    {

    }

}
