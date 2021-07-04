using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private Material skinMaterial;
    private Color skinMatColor;
    
    // Start is called before the first frame update
    void Start()
    {
        skinMaterial = GetComponent<SpriteRenderer>().sharedMaterial;
        skinMatColor = LevelManager.instance.GetPlayerCurrentSkin();
        skinMaterial.SetColor("_Color", skinMatColor);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
