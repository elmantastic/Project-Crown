using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public int minutesLevel = 4;
    public int diamondPrice = 300;
    public int machinePrice = 2000;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void addScore(int _score){
        score += _score;
    }

    public void easyLevel(){
        minutesLevel = 4;
        machinePrice = 2000;
        diamondPrice = 300;
    }
    public void mediumLevel(){
        minutesLevel = 2;
        machinePrice = 4000;
        diamondPrice = 400;
    }
    public void hardLevel(){
        minutesLevel = 1;
        machinePrice = 6000;
        diamondPrice = 500;
    }
}
