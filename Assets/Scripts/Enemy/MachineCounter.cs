using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MachineCounter : MonoBehaviour
{
    public TMP_Text textDisplay;
    private int machine = 0;

    private void Start() {
        textDisplay.GetComponent<TextMeshProUGUI>().text = machine.ToString();
    }

    private void Update() {
        textDisplay.GetComponent<TextMeshProUGUI>().text = machine.ToString();
    }

    public void AddMachineDestroyed(){
        machine++;
    }

    public int GetMachineDestroyed(){
        return machine;
    }
}
