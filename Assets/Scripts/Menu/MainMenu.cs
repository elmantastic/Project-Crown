using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    public Button[] Maps;
    private void Start() {
        CheckLevel();    
    }
    private void Update() {
        CheckLevel();
    }

    private void CheckLevel(){
        for(int i = 0; i < Maps.Length; i++){
            if(i <= GameManager.Instance.GetPlayerLevelCompleted()){
                Maps[i].interactable = true;
                Maps[i].transform.GetChild(0).gameObject.SetActive(false);
            } else {
                Maps[i].transform.GetChild(0).gameObject.SetActive(true);
                Maps[i].interactable = false;
            }
        }
    }

     public void QuitGame(){
        Debug.Log("QUIT");
        Application.Quit();
    }


}
