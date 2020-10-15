using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debug_Vencer : MonoBehaviour
{
    Button butao;
    void Start()
    {
        butao = GetComponent<Button>();
        butao.onClick.AddListener(ChamarVencer);   
    }

    void ChamarVencer()
    {
        C_Jogo.instancia.Vencer();
    }

}
