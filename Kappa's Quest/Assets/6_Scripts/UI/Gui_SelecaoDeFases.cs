using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gui_SelecaoDeFases : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI nome;
    [SerializeField]TextMeshProUGUI vitalidade;
    [SerializeField] TextMeshProUGUI forca;
    [SerializeField]TextMeshProUGUI resistencia;
    [SerializeField]TextMeshProUGUI inteligencia;
    [SerializeField] TextMeshProUGUI agilidade;
    [SerializeField] Button batalhar;

    private void Start()
    {
 
       RandomizadorDeFases.rf.FasesGeradas += SetarInteracao;
       batalhar.onClick.AddListener(Batalhar);
        
        
    }

    public void Batalhar()
    {
        RandomizadorDeFases.rf.Batalhar();
    }

    public void SetarInteracao()
    {
        RandomizadorDeFases.rf.SetarActionEmCasoDeInteracao(UpdateHUD);
    }

    public void UpdateHUD(Fase fase)
    {
        vitalidade.text = "Vitalidade: " + fase.vitalidade.ToString();
        forca.text = "Força: " + fase.forca.ToString();
        resistencia.text = "Resistencia: " + fase.resistencia.ToString();
        inteligencia.text = "Inteligencia: " + fase.inteligencia.ToString();
        agilidade.text = "agilidade: " + fase.agilidade.ToString();
        nome.text = fase.nome;
    }
}
