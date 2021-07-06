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
    public GameObject gameOverUI;
    public DiamondCounter diamondCounter;
    public MachineCounter machineCounter;
    public CinemachineVirtualCamera cam;
    public PointFollow followPoint;

    public bool isCompleteLevel;
    public int diamondGrabed = 0;
    public int machineDestroyed = 0;

    private int levelTime = 4;
    private int diamondPrice = 300;
    private int machinePrice = 2000;

    private int levelCurrentMap = 1;
    //private int playerGold = 0;

    private Color platfromColor = new Vector4 (0.825f, 2.108f, 0.829f, 1.0f);
    private Color playerCurrentSkin = new Vector4 (0.106f, 1.380f, 2.462f, 0.5f);

    private void Awake() {
        instance = this;
        levelCurrentMap = GameManager.Instance.levelCurrentMap;
        levelTime = GameManager.Instance.minutesLevel;
        diamondPrice = GameManager.Instance.diamondPrice;
        machinePrice = GameManager.Instance.machinePrice;
        platfromColor = GameManager.Instance.platformColor;
        playerCurrentSkin = GameManager.Instance.GetPlayerCurrentSkin();
    }

    private void Start() {
        //Load Player Data // Sudah di load di main menu
        // GameManager.Instance.LoadPlayerData(); 

        //Set Data that we need
        //playerGold = GameManager.Instance.GetPlayerGold();
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
    public void AddMachineDestroyed(){
        machineCounter.AddMachineDestroyed();
    }

    public void CompleteLevel(){
        diamondGrabed = diamondCounter.GetDiamondCount();
        machineDestroyed = machineCounter.GetMachineDestroyed();
        GameManager.Instance.SetPlayerLevelCompleted(levelCurrentMap);
        isCompleteLevel = true;
        completeLevelUI.SetActive(true);
    }
    public void GameOverLevel(){
        gameOverUI.SetActive(true);
    }

    public bool StopCountDownTime(){
        return isCompleteLevel;
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
    public Color GetPlayerCurrentSkin(){
        return playerCurrentSkin;
    }

    public void SetPlayerGoldAchivement(int _gold){
        //playerGold = _gold;
        GameManager.Instance.SetPlayerGold(_gold);
    }

    public void SetLevelMap(int _levelMap){
        levelCurrentMap = _levelMap;
    }

}
