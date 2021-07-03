using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class GoldCounter : MonoBehaviour
{
    public TMP_Text textDisplay;
    private int gold = 0;

    private void Start() {
        gold = MainMenuManager.instance.getPlayerGold();
        textDisplay.GetComponent<TextMeshProUGUI>().text = gold.ToString();
    }

    private void Update() {
        textDisplay.GetComponent<TextMeshProUGUI>().text = gold.ToString();
    }

    public void AddGold(){
        gold++;
    }

    public void SetGoldCount(int _gold){
        gold = _gold;
    }
}
