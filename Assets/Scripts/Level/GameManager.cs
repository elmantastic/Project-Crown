using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public int minutesLevel = 4;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void addScore(int _score){
        score += _score;
    }

    public void easyLevel(){
        minutesLevel = 4;
    }
    public void mediumLevel(){
        minutesLevel = 2;
    }
    public void hardLevel(){
        minutesLevel = 1;
    }
}
