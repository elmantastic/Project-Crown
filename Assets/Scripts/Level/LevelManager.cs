using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform respownPoint;
    public Transform canvasPosition;
    public GameObject playerPrefab;
    public GameObject countDownPrefab;
    public GameObject completeLevelUI;
    public DiamondCounter diamondCounter;
    public CinemachineVirtualCamera cam;
    public PointFollow followPoint;
    public int diamondGrabed = 0;
    public int machineDestroyed = 0;

    private int levelTime = 4;
    private int diamondPrice = 300;
    private int machinePrice = 2000;

    private Color platfromColor;

    private void Awake() {
        instance = this;
        levelTime = GameManager.instance.minutesLevel;
        diamondPrice = GameManager.instance.diamondPrice;
        machinePrice = GameManager.instance.machinePrice;
        platfromColor = GameManager.instance.platformColor;
    }

    public void PlayerDie(){
        ShowCountDown();
    }

    public void Respawn(){
        GameObject player = Instantiate(playerPrefab, respownPoint.position, Quaternion.identity);

        cam.Follow = player.transform;
        followPoint.unitGameObject = player.transform;
    }

    private void ShowCountDown(){
        GameObject countDown = Instantiate(countDownPrefab, canvasPosition.position, Quaternion.identity);

        countDown.transform.parent = canvasPosition;
    }

    public int GetMinute(){
        return levelTime;
    }

    public void AddDiamond(){
        diamondCounter.AddDiamond();
    }

    public void CompleteLevel(){
        diamondGrabed = diamondCounter.GetDiamondCount();
        completeLevelUI.SetActive(true);
    }
    public void GameOverLevel(){
        completeLevelUI.SetActive(true);
    }

    public void ExitLevel(){
        SceneManager.LoadScene("MainMenu");
    }

    public int GetMachinePrice(){
        return machinePrice;
    }
    public int GetDiamondPrice(){
        return diamondPrice;
    }

    public int GetMachineDestroyed(){
        return machineDestroyed;
    }

    public int GetDiamondGrabbed(){
        return diamondGrabed;
    }

    public Color GetPlatformColor(){
        return platfromColor;
    }
}
