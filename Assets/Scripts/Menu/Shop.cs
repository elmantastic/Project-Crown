using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Shop : MonoBehaviour
{
    //public Button[] MyAvailableSkin;
    public Transform mySkinsPanel;
    public GameObject buttonPrefab;
    public SkinDisplay mySkinDisplay;
    public Button buttonUseSkin;

    
    public Button[] ShopSkinButtons;
    public SkinDisplay buySkinDisplay;
    public Button buttonBuySkin;
    public TMP_Text priceDisplay;


    [Header ("MySkin")]
    private Color myCurrentSkin;
    private List<Color> myAvailableSkin = new List<Color>();
    //private Color mySelectedSkin;

    [Header ("Shop")]
    private List<Color> shopSkins = new List<Color>();
    private Color shopSelectedSkin;
    private int shopSelectedPrice;
    [SerializeField] private int epicPrice = 50000;
    [SerializeField] private int exclusivePrice = 100000;

    [Header ("Epic Colors")]
    private Color green = new Vector4(0.156f, 1.327f, 0.167f, 0f);
    private Color purple = new Vector4(1.28f, 0.15f, 1.48f, 0f);
    private Color orange = new Vector4(0.738f, 0.254f, 0.054f, 0f);
    private Color red = new Vector4(1.62f, 0.06f, 0.06f, 0f);
    private Color yellow = new Vector4(0.982f, 1.059f, 0.2333f, 0f);
    private Color blue = new Vector4(0.1f, 0.421f, 2.118f, 0f);

    [Header ("Epic Colors")]
    private Color gold = new Vector4(2.09f, 1.27f, 0.6f, 1.0f);
    private Color magenta = new Vector4(2.09f, 0.42f, 0.774f, 1.0f);
    private Color turquoise = new Vector4(0.427f, 2.09f, 1.98f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        ResetAvailableSkin();
        SetUpShopColor();
        ResetShopAvailableSkin();
        buttonUseSkin.interactable = false;
        buttonBuySkin.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region MY INVENTORY SKIN
        
    
    private void SetUpMySkins(){
        myAvailableSkin = GameManager.Instance.GetAvailablePlayerSkins();

        for (int i = 0; i < myAvailableSkin.Count; i++){
            GameObject btn = Instantiate(buttonPrefab) as GameObject;
            Vector4 vecColor = myAvailableSkin[i];
            if(vecColor[3] <= 0.6f){
                vecColor[3] = 0.6f;
            }
            btn.GetComponent<Image>().color = vecColor;
            btn.transform.SetParent(mySkinsPanel, false);
            int index = i;
            btn.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(index));
        }
    }

    public void OnButtonClick(int colorIndex){
        //do something
        buttonUseSkin.interactable = true;
        MainMenuManager.instance.SoundSelectSkin();
        // set mycurrentskin to selected color
        for( int i = 0; i < myAvailableSkin.Count; i++){
            if(i == colorIndex){
                myCurrentSkin = myAvailableSkin[i];
                mySkinDisplay.SetSkinDisplay(myCurrentSkin);
            }
        }
    }

    public void SetSkinToPlayerCurrentSkin(){
        GameManager.Instance.SetPlayerCurrentSkin(myCurrentSkin);
    }

    
    private void ResetAvailableSkin(){
        // remove all the button color on available skin
        foreach (Transform child in mySkinsPanel) {
            GameObject.Destroy(child.gameObject);
        }
        // set it up again
        SetUpMySkins();
    }

    // private CreateButton(string _colorName){
    //     GameObject button = new GameObject();
    //     button.transform.parent = mySkinsPanel;
    //     button.AddComponent<RectTransform>();
    //     button.AddComponent<Button>();
    //     button.GetComponent<RectTransform>().SetSize(new Vector2(50.0f, 50.0f));
    //     button.GetComponent<Button>().onClick.AddListener(); // bingung
    // }
    #endregion

    #region SHOP SKIN
        
    
    public void SetBuyDisplaySkin(){
        buySkinDisplay.SetSkinDisplay(shopSelectedSkin);
        priceDisplay.GetComponent<TextMeshProUGUI>().text = "PURCHASE (" + shopSelectedPrice.ToString() + ")";
        
        if(MainMenuManager.instance.getPlayerGold() > shopSelectedPrice){
            buttonBuySkin.interactable = true;

        } else {
            buttonBuySkin.interactable = false;

        }
    }
    public void SetEpicPrice(){
        shopSelectedPrice = epicPrice;
    }
    public void SetExclusivePrice(){
        shopSelectedPrice = exclusivePrice;
    }

    private void SetUpShopColor(){
        shopSkins.Add(green);
        shopSkins.Add(orange);
        shopSkins.Add(blue);
        shopSkins.Add(red);
        shopSkins.Add(purple);
        shopSkins.Add(yellow);
        shopSkins.Add(gold);
        shopSkins.Add(magenta);
        shopSkins.Add(turquoise);
        for (int i = 0; i < ShopSkinButtons.Length; i++){
            Vector4 vecColor = shopSkins[i];
            if(vecColor[3] <= 0.6f){
                vecColor[3] = 0.6f;
            }
            ShopSkinButtons[i].GetComponent<Image>().color = vecColor;
        }
    }

    public void SetBuyGreenSkin(){
        shopSelectedSkin = green;
    }
    public void SetBuyPurpleSkin(){
        shopSelectedSkin = purple;
    }
    public void SetBuyOrangeSkin(){
        shopSelectedSkin = orange;
    }
    public void SetBuyRedSkin(){
        shopSelectedSkin = red;
    }
    public void SetBuyYellowSkin(){
        shopSelectedSkin = yellow;
    }
    public void SetBuyBlueSkin(){
        shopSelectedSkin = blue;
    }
    public void SetBuyGoldSkin(){
        shopSelectedSkin = gold;
    }
    public void SetBuyMagentaSkin(){
        shopSelectedSkin = magenta;
    }
    public void SetBuyTurquoiseSkin(){
        shopSelectedSkin = turquoise;
    }

    public void BuySkin(){
        // add skin to game manager player skins
        GameManager.Instance.AddSkinToAvailableSkin(shopSelectedSkin);
        // calculate the player gold
        MainMenuManager.instance.CalculatePlayerGold(shopSelectedPrice);
        // save data
        GameManager.Instance.SavePlayerData();
        // re render available skin
        ResetAvailableSkin();
        ResetShopAvailableSkin();
    }

    private void ResetShopAvailableSkin(){
        //check each color on shop that already on player available skin
        for(int i = 0; i < ShopSkinButtons.Length; i++){
            ShopSkinButtons[i].interactable = true;
            foreach (Color skin in myAvailableSkin){
                if(skin == shopSkins[i]){
                    ShopSkinButtons[i].interactable = false;
                }
            }
        }
        // if exists then interactable to false
    }


    #endregion
}
