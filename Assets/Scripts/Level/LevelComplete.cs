using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public GameObject AchivementUI;
    
    public void ShowAchivement(){
        AchivementUI.SetActive(true);
    }
}
