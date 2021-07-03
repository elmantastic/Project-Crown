using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    public void PlayLevel1(){
        SceneManager.LoadScene("Level1");
        GameManager.Instance.SetLevelCurrentMap(1);
    }
    public void PlayLevel2(){
        SceneManager.LoadScene("Level1");
        GameManager.Instance.SetLevelCurrentMap(2);
    }
    public void PlayLevel3(){
        SceneManager.LoadScene("Level1");
        GameManager.Instance.SetLevelCurrentMap(3);
    }

    public void SetEasyLevel(){
        GameManager.Instance.easyLevel();
    }
    public void SetMediumLevel(){
        GameManager.Instance.mediumLevel();
    }
    public void SetHardLevel(){
        GameManager.Instance.hardLevel();
    }
}
