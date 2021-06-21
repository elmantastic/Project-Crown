using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimeCountdown : MonoBehaviour
{
    public TMP_Text textDisplay;
    public int minutesLeft = 4;
    public int secondsLeft = 59;
    public bool takingAway = false;
    private string timeDisplay = "";

    private void Start() {
        textDisplay.GetComponent<TextMeshProUGUI>().text = "0" + minutesLeft +":"+ timeDisplay + secondsLeft;
    }

    private void Update() {
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

}
