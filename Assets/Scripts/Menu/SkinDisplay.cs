using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinDisplay : MonoBehaviour
{
    private Material skinMaterial;
    private Color skinMatColor;
    
    // Start is called before the first frame update
    void Start()
    {
        skinMaterial = GetComponent<Image>().material;
        skinMatColor = GameManager.Instance.GetPlayerCurrentSkin();
        skinMaterial.SetColor("_Color", skinMatColor);
    }

    public void SetSkinDisplay(Color _color){
        skinMaterial.SetColor("_Color", _color);
    }
}
