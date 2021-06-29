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
    public Color platformColor;

    private Color laserRed = new Vector4 (1.988f, 0.438f, 0.438f, 1.0f);
    private Color laserPink = new Vector4 (1.758f, 0.640f, 1.911f, 1.0f);
    private Color laserGreen = new Vector4 (0.825f, 2.108f, 0.829f, 1.0f);
    private Color laserGold = new Vector4 (2.09f, 1.27f, 0.6f, 1.0f);

    private void Awake() {
        instance = this;
        platformColor = laserGreen;
        DontDestroyOnLoad(this.gameObject);
    }

    public void addScore(int _score){
        score += _score;
    }

    public void easyLevel(){
        minutesLevel = 4;
        machinePrice = 2000;
        diamondPrice = 300;
        platformColor = laserGreen;
    }
    public void mediumLevel(){
        minutesLevel = 2;
        machinePrice = 4000;
        diamondPrice = 400;
        platformColor = laserRed;
    }
    public void hardLevel(){
        minutesLevel = 1;
        machinePrice = 6000;
        diamondPrice = 500;
        platformColor = laserGold;
    }
}
