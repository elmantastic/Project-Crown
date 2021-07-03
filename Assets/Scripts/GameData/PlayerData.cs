using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public int level;
    public float[] currentSkin;
    public float[,] mySkins;


    public PlayerData (int _gold, int _level, float[] _currentSkin, float[,] _mySkins){
        gold = _gold;
        level = _level;
        currentSkin = _currentSkin;
        mySkins = _mySkins;
    }
}
