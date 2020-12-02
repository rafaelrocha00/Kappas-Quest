using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUI_Inventario : MonoBehaviour
{
    [SerializeField] ItemBotao selecionado;
    [SerializeField] Inventario inv;
    [SerializeField] Image CapaceteIcon;
    [SerializeField] Image ArmaduraIcon;
    [SerializeField] Image CalcaIcon;
    [SerializeField] Image BraceleteIcon;
    [SerializeField] Image AnelIcon;
    [SerializeField] TextMeshProUGUI Dinheiro;
    [SerializeField] TextMeshProUGUI Nome;
    [SerializeField] TextMeshProUGUI ItemDescricao;
    [SerializeField] TextMeshProUGUI precoItem;
    [SerializeField] Button comprar;
    [SerializeField] Transform posicao;
    [SerializeField] GameObject Kappa;
    [SerializeField] GameObject Nekko;
    [SerializeField] GameObject Wanko;
    [SerializeField] ItemBotao prefab;
    [SerializeField] List<Item> itensKappa;
    [SerializeField] List<Item> itensWanko;
    [SerializeField] List<Item> itensNeko;
    [SerializeField] public GameObject Grid;

    GameObject invocado;

    void Start()
    {
        inv = C_Jogo.instancia.GetComponent<Inventario>();
        comprar.onClick.AddListener(Comprar);
        UpdateObjeto();
        Nome.text = "";
        ItemDescricao.text = "";
        precoItem.text = "";
        SpawnarItens();
        UpdateUI();
    }

    public void SpawnarItens()
    {
        int tipo = C_Jogo.instancia.TipoDePersonagem;
        if (tipo == 0)
        {
            SpawnarItens(itensKappa);
        }
        if (tipo == 1)
        {
            SpawnarItens(itensNeko);

        }
        if (tipo == 2)
        {
            SpawnarItens(itensWanko);

        }
    }

    public void SpawnarItens(List<Item> itens)
    {
        for (int i = 0; i < itens.Count; i++)
        {
            ItemBotao b = Instantiate(prefab, Grid.transform);
            b.itemRelacionado = itens[i];
        }
    }

    void UpdateUI()
    {
        UpdateIcon(ArmaduraIcon, inv.armadura);
        UpdateIcon(CapaceteIcon, inv.capacete);
        UpdateIcon(CalcaIcon, inv.calca);
        UpdateIcon(BraceleteIcon, inv.bracelete);
        UpdateIcon(AnelIcon, inv.anel);
        Dinheiro.text = inv.GetDinheiro().ToString();
    }

    void UpdateObjeto()
    {
        int tipo = C_Jogo.instancia.TipoDePersonagem;
        Destroy(invocado); 
        if(tipo == 0)
        {
            invocado = Instantiate(Kappa, posicao.position, posicao.rotation);
        }
        if(tipo == 1)
        {
            invocado = Instantiate(Nekko, posicao.position, posicao.rotation);
        }
        if(tipo == 2)
        {
            invocado = Instantiate(Wanko, posicao.position, posicao.rotation);
        }
    }

    public void UpdateIcon(Image image, Equipavel equip)
    {
        if (equip != null)
        {
            image.enabled = true;
            image.sprite = equip.icon;
        }
        else
        {
            image.enabled = false;
        }
    }

    public void Selecionar(ItemBotao item)
    {
        selecionado = item;
        precoItem.text = item.itemRelacionado.preco.ToString();
        ItemDescricao.text = item.itemRelacionado.descricao;
        Nome.text = item.itemRelacionado.nome;
    }

    void Comprar() {
        if(selecionado != null)
            selecionado.Comprar();
        UpdateUI();
    }


}
