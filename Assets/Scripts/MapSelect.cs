using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public void PlayLevel1(){
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2(){
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel3(){
        SceneManager.LoadScene("Level1");
    }

    public void SetEasyLevel(){
        GameManager.instance.easyLevel();
    }
    public void SetMediumLevel(){
        GameManager.instance.mediumLevel();
    }
    public void SetHardLevel(){
        GameManager.instance.hardLevel();
    }
}
