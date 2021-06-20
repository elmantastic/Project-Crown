using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RespawnCoundown : MonoBehaviour
{
    private TMP_Text respawnCooldown;
    //public Panel respawnBg;
    private int secondsLeft = 3;
    private bool takingAway = false;

    private void Start() {
        //respawnBg.SetActive = true;
        respawnCooldown = GetComponent<TextMeshProUGUI>();
        respawnCooldown.text = secondsLeft.ToString();
    }

    private void Update() {
        if(takingAway == false && secondsLeft >= 0){
            StartCoroutine(TimerTake());
        } else if(secondsLeft == 0) {
            Destroy(gameObject);
            LevelManager.instance.Respawn();
        }
    }

    IEnumerator TimerTake(){
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft--;
        respawnCooldown.GetComponent<TextMeshProUGUI>().text = secondsLeft.ToString();
        takingAway = false;
    }
}
