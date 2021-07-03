using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class Achivement : MonoBehaviour
{
    public TMP_Text machineDestroyedText;
    public TMP_Text machinePriceText;
    public TMP_Text machineTotalGoldText;
    public TMP_Text diamondGrabbedText;
    public TMP_Text diamondPriceText;
    public TMP_Text diamondTotalGoldText;
    public TMP_Text totalGoldAchivementText;
    private int machineDestroyed;
    private int machinePrice;
    private int machineTotalGold;
    private int diamondGrabbed;
    private int diamondPrice;
    private int diamondTotalGold;
    private int totalGoldAchivement;

    private bool canContinue;

    private void Awake() {
        GetPrices();
    }

    private void Start() {
        GetObjectiveAchivement();
        GetTotalAchivement();
        SaveGoldToPlayerData();
        canContinue = true;
    }

    private void Update() {
        ShowAchivementRecord();
        CheckInput();
    }

    private void CheckInput(){
        if(canContinue && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))){
            GameManager.Instance.SavePlayerData();
            LevelManager.instance.ExitLevel();
        }
    }

    private void ShowAchivementRecord(){
        machineDestroyedText.GetComponent<TextMeshProUGUI>().text = machineDestroyed.ToString();
        machinePriceText.GetComponent<TextMeshProUGUI>().text = "x "+ machinePrice.ToString();
        machineTotalGoldText.GetComponent<TextMeshProUGUI>().text = machineTotalGold.ToString();

        diamondGrabbedText.GetComponent<TextMeshProUGUI>().text = diamondGrabbed.ToString();
        diamondPriceText.GetComponent<TextMeshProUGUI>().text = "x "+ diamondPrice.ToString();
        diamondTotalGoldText.GetComponent<TextMeshProUGUI>().text = diamondTotalGold.ToString();

        totalGoldAchivementText.GetComponent<TextMeshProUGUI>().text = totalGoldAchivement.ToString();

    }

    private void GetPrices(){
        machinePrice = LevelManager.instance.GetMachinePrice();
        diamondPrice = LevelManager.instance.GetDiamondPrice();
    }

    private void GetObjectiveAchivement(){
        machineDestroyed = LevelManager.instance.GetMachineDestroyed();
        diamondGrabbed = LevelManager.instance.GetDiamondGrabbed();
    }

    private void GetTotalAchivement(){
        machineTotalGold = machineDestroyed * machinePrice;
        diamondTotalGold = diamondGrabbed * diamondPrice;
        totalGoldAchivement = machineTotalGold + diamondTotalGold;
    }

    private void SaveGoldToPlayerData(){
        LevelManager.instance.SetPlayerGoldAchivement(totalGoldAchivement);
    }
}
