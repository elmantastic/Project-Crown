using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DiamondCounter : MonoBehaviour
{
    public TMP_Text textDisplay;
    private int diamond = 0;

    private void Start() {
        textDisplay.GetComponent<TextMeshProUGUI>().text = diamond.ToString();
    }

    private void Update() {
        textDisplay.GetComponent<TextMeshProUGUI>().text = diamond.ToString();
    }

    public void AddDiamond(){
        diamond++;
    }

    public int GetDiamondCount(){
        return diamond;
    }

}
