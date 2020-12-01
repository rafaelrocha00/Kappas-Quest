using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBotao : MonoBehaviour
{
    int id;
    [SerializeField] GUI_Inventario guiInv;
    public Item itemRelacionado;
    [SerializeField] bool desbloqueado;
    [SerializeField] Image icon;
    [SerializeField] Button botaoRespectivo;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Comprar);
        guiInv = GameObject.Find("Canvas").GetComponent<GUI_Inventario>();
        id = itemRelacionado.id;
        icon.sprite = itemRelacionado.icon;
        botaoRespectivo = GetComponent<Button>();
        botaoRespectivo.onClick.AddListener(Selecionar);
    }

    public void Comprar()
    {
        Inventario inv = C_Jogo.instancia.GetComponent<Inventario>();
        bool resposta = inv.AdicionarItem(itemRelacionado);
        if (resposta)
        {
            inv.Pagar(itemRelacionado.preco);
            if(itemRelacionado is Equipavel)
            {
                Equipavel equi = (Equipavel)itemRelacionado;
                inv.TentarEquipar(equi);
            }
        }
    }

    void Selecionar()
    {
        guiInv.Selecionar(this);
    }
}
