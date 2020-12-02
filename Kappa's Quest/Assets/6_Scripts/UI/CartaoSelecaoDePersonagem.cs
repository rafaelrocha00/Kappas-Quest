using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartaoSelecaoDePersonagem : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] GameObject Personagem;
    [SerializeField] Image icon;
    [SerializeField] GameObject painelAtributos;
    [SerializeField] Animator anim;
    [SerializeField] GameObject atributos_TXT;
    [SerializeField] TextMeshProUGUI atributos;
    [SerializeField] CharacterToken token;
    [SerializeField] float NewPosition;
    [SerializeField] int TipoDePersonagem;

    private void Start()
    {
        if(token != null)
        AtualizarAtributos();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(LidarComEstados);
        if (toggle.isOn) StartCoroutine(Selecionar());

    }

    public void LidarComEstados(bool estado)
    {
        if (estado)
            StartCoroutine(Selecionar());
        else
          StartCoroutine(Deselecionar());
    }

    public void AtualizarAtributos()
    {
        atributos.text = "Vitality: ";
        atributos.text += token.Vitality.ToString();
        atributos.text += "\n";
        atributos.text += "Strenght: ";
        atributos.text += token.Strenght.ToString();
        atributos.text += "\n";
        atributos.text += "Resistance: ";
        atributos.text += token.Resistance.ToString();
        atributos.text += "\n";
        atributos.text += "Intelligence: ";
        atributos.text += token.Intelligence.ToString();
        atributos.text += "\n";
        atributos.text += "Agility: ";
        atributos.text += token.Agility.ToString();
        atributos.text += "\n";
    }

    IEnumerator Selecionar()
    {
        C_Jogo.instancia.personagemPrincipalPrefab = Personagem;
        C_Jogo.instancia.TipoDePersonagem = TipoDePersonagem;
        anim.gameObject.SetActive(false);
        icon.rectTransform.anchoredPosition = new Vector2(icon.rectTransform.anchoredPosition.x, NewPosition);
        anim.gameObject.SetActive(true);
        anim.SetBool("Selecionado", true);
        yield return new WaitForSeconds(0.2f);
        atributos_TXT.SetActive(true);
    }

    IEnumerator Deselecionar()
    {
        Debug.Log("a");
        StopAllCoroutines();
        icon.rectTransform.anchoredPosition = Vector2.zero;
        atributos_TXT.SetActive(false);
        anim.SetBool("Selecionado", false);
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Deselecionou");
        anim.gameObject.SetActive(false);
    }
}
