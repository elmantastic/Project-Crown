using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool MainMenuContinue;

    public int score;
    public int levelCurrentMap = 1;
    public int minutesLevel = 4;
    public int diamondPrice = 300;
    public int machinePrice = 2000;

    public Color platformColor;
    private Color laserRed = new Vector4 (1.988f, 0.438f, 0.438f, 1.0f);
    private Color laserPink = new Vector4 (1.758f, 0.640f, 1.911f, 1.0f);
    private Color laserGreen = new Vector4 (0.825f, 2.108f, 0.829f, 1.0f);
    private Color laserGold = new Vector4 (2.09f, 1.27f, 0.6f, 1.0f);

    public Color currentSkin = new Vector4(0.106f, 1.380f, 2.462f, 0.5f);
    public Color defaultSkin = new Vector4(0.0f, 0.0f, 0.0f, 0.5f);
    public List<Color> AvailableSkins = new List<Color>();

    [Header("Player Data")]
    public int playerGold = 0;
    public int playerLevelCompleted = 0;
    public float[] playerCurrentSkin;
    public float[,] playerSkinAvailable;

    private void Awake() {
        instance = this;        
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        platformColor = laserGreen;
        //AvailableSkins[0] = defaultSkin;
        //SetAvailableSkins();
    }

    public static GameManager Instance {
        get {
            if(instance==null) {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
 
            return instance;
        }
    }

    public void isContinue(){
        MainMenuContinue = true;
    }

    public void addScore(int _score){
        score += _score;
    }

    public void SetLevelCurrentMap(int _mapLevel){
        levelCurrentMap = _mapLevel;
        if(_mapLevel == 1){
            platformColor = laserGreen;
        } else if( _mapLevel == 2){
            platformColor = laserRed;
        } else {
            platformColor = laserGold;
        }
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

    private void CurrentSkinToFloat(){
        Vector4 currentSkinColor = currentSkin;
        playerCurrentSkin = new float[4];
        playerCurrentSkin[0] = currentSkinColor[0];
        playerCurrentSkin[1] = currentSkinColor[1];
        playerCurrentSkin[2] = currentSkinColor[2];
        playerCurrentSkin[3] = currentSkinColor[3];
    }

    private void LoadCurrentSkin(){
        Vector4 skinColor = new Vector4();
        skinColor[0] = playerCurrentSkin[0];
        skinColor[1] = playerCurrentSkin[1];
        skinColor[2] = playerCurrentSkin[2];
        skinColor[3] = playerCurrentSkin[3];

        currentSkin = skinColor;
    }

    private void LoadAvailableSkin(){
        if(playerSkinAvailable.GetLength(0) <= 0){
            AvailableSkins.Add(defaultSkin);
        } else {
            AvailableSkins.Clear();
            for(int i = 0; i < playerSkinAvailable.GetLength(0); i++){
                Vector4 skinColor = new Vector4();
                skinColor[0] = playerSkinAvailable[i, 0];
                skinColor[1] = playerSkinAvailable[i, 1];
                skinColor[2] = playerSkinAvailable[i, 2];
                skinColor[3] = playerSkinAvailable[i, 3];

                AvailableSkins.Add(skinColor);
            }

        }

    }

    private void SetAvailableSkins(){
        AvailableSkins.Add(defaultSkin);
        AvailableSkins.Add(laserGreen);
        AvailableSkins.Add(laserPink);
        AvailableSkins.Add(laserRed);
        AvailableSkins.Add(laserGold);
    }

    public void AddSkinToAvailableSkin(Color _color){
        AvailableSkins.Add(_color);
    }

    private Vector4 ConvertColorToFloat(Color color){
        Vector4 tempColor = color;
        // float[] skinColor = {0.0f};
        // skinColor[0] = tempColor.x;
        // skinColor[1] = tempColor.y;
        // skinColor[2] = tempColor.z;
        // skinColor[3] = tempColor.w;

        return tempColor;
    }

    private void GetAllAvailableSkinsToFloat(){
        playerSkinAvailable = new float[AvailableSkins.Count,4];
        //get all saved color
        for (int i = 0; i < AvailableSkins.Count; i++){
            Vector4 tempColor;
            tempColor = ConvertColorToFloat(AvailableSkins[i]);

            //add to player data
            playerSkinAvailable[i,0] = tempColor.x;
            playerSkinAvailable[i,1] = tempColor.y;
            playerSkinAvailable[i,2] = tempColor.z;
            playerSkinAvailable[i,3] = tempColor.w;
        }
    }

    private void ConvertColorToFloatSkins(){
        //convert current skin
        CurrentSkinToFloat();
        //convert the available skins
        GetAllAvailableSkinsToFloat();
    }

    public void SetPlayerGold(int _gold){
        playerGold += _gold;
    }
    public int GetPlayerGold(){
        return playerGold;
    }
    public void CalculatePlayerPayment(int _gold){
        playerGold = _gold;
    }

    public void SetPlayerLevelCompleted(int _level){
        if(playerLevelCompleted < _level){

            playerLevelCompleted = _level;
        }
    }
    public int GetPlayerLevelCompleted(){
        return playerLevelCompleted;
    }
    public List<Color> GetAvailablePlayerSkins(){
        return AvailableSkins;
    }

    public void SetPlayerCurrentSkin(Color _colorSkin){
        currentSkin = _colorSkin;
        SavePlayerData();
    }

    public Color GetPlayerCurrentSkin(){
        return currentSkin;
    }


    public void SavePlayerData(){
        //get player gold   (ada di bagian Achivement)

        //get player level completed (ada di bagian Level Complete)

        //get player current skin and available skin
        ConvertColorToFloatSkins();

        SaveSystem.SavePlayerData(playerGold, playerLevelCompleted, playerCurrentSkin, playerSkinAvailable);

        Debug.Log("Saved");
    }

    public void LoadPlayerData(){
        //load data from Save System

        PlayerData data = SaveSystem.LoadPlayerData();

        if(data != null){

            playerGold = data.gold;
            playerLevelCompleted = data.level;
            playerCurrentSkin = data.currentSkin;
            //playerSkinAvailable = new float[data.mySkin.Lenght][4];
            playerSkinAvailable = data.mySkins;

            LoadCurrentSkin();
            //Load AvailableSkin (mungkin di main menu saja?)

            LoadAvailableSkin();
            Debug.Log("Data Loaded");
        } else {
            currentSkin = defaultSkin;
            AvailableSkins.Add(currentSkin);
        }
    }
}
