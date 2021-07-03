using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimeCountdown : MonoBehaviour
{
    public TMP_Text textDisplay;
    private int minutesLeft = 4;
    private int secondsLeft = 59;
    public bool takingAway = false;
    private string timeDisplay = "";

    private void Start() {
        GetTime();
        textDisplay.GetComponent<TextMeshProUGUI>().text = "0" + minutesLeft +":"+ timeDisplay + secondsLeft;
    }

    private void Update() {
        CheckStopCountdown();
        if(takingAway == false && minutesLeft >= 0){
            StartCoroutine(TimerTake());
        }
    }

    IEnumerator TimerTake(){
        takingAway = true;
        yield return new WaitForSeconds(1);
        if(secondsLeft == 0){
            if(minutesLeft == 0){
                print("GAME OVER");
                LevelManager.instance.GameOverLevel();
            } else {
                minutesLeft--;
                secondsLeft= 59;
            }

        } else {
            secondsLeft--;

        }
        if(secondsLeft <10){
            timeDisplay = "0"; 
        } else {
            timeDisplay = "";
        }
        textDisplay.GetComponent<TextMeshProUGUI>().text = "0" + minutesLeft+ ":"+ timeDisplay + secondsLeft;
        takingAway = false;
    }

    private void GetTime(){
        minutesLeft = LevelManager.instance.GetMinute();
    }

    public void CheckStopCountdown(){
        if(LevelManager.instance.StopCountDownTime()){
            minutesLeft = 99;

        }
    }
}
