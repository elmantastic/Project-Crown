using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public GameObject MainMenuUI;
    public GameObject SelectLevelUI;
    public GameObject GoldCounter;

    private int playerGold;
    private int playerLevelCompleted;

    private void Awake() {
        instance = this;
        //Load Player Data
    }

    private void Start() {
        GameManager.Instance.LoadPlayerData();
        // get data that we need
        playerGold = GameManager.Instance.GetPlayerGold();
        playerLevelCompleted = GameManager.Instance.GetPlayerLevelCompleted();

        if(GameManager.Instance.MainMenuContinue){
            MainMenuUI.SetActive(false);
            SelectLevelUI.SetActive(true);
        }
        GameManager.Instance.isContinue();
    }

    public int getPlayerGold(){
        return playerGold;
    }

    public void CalculatePlayerGold(int _payment){
        playerGold -= _payment;
        GameManager.Instance.CalculatePlayerPayment(playerGold);
    }

    #region Sound Area
        public void SoundMenuHover(){
            GameManager.Instance.SoundMenuHover();
        }
        public void SoundOpenMenu(){
            GameManager.Instance.SoundOpenMenu();
        }
        public void SoundExitMenu(){
            GameManager.Instance.SoundExitMenu();
        }
        public void SoundSelectSkin(){
            GameManager.Instance.SoundSelectSkin();
        }
        public void SoundPurchaseSkin(){
            GameManager.Instance.SoundPurchaseSkin();
        }
        public void SoundEquipSkin(){
            GameManager.Instance.SoundEquipSkin();
        }
    #endregion
}
