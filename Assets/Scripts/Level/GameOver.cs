using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private bool canContinue;
    public GameObject continueUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput(){
        if(canContinue && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))){
            GameManager.Instance.StopSoundGameOver();
            LevelManager.instance.ExitLevel();
        }
    }

    public void ShowContinue(){
        canContinue = true;
        continueUI.SetActive(true);
    }
}
