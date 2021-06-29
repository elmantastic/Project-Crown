using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformMat : MonoBehaviour
{
    private Material platformMaterial;
    private Color platformMatColor;
    
    // Start is called before the first frame update
    void Start()
    {
        platformMaterial = GetComponent<TilemapRenderer>().sharedMaterial;
        platformMatColor = LevelManager.instance.GetPlatformColor();
        platformMaterial.SetColor("_Color", platformMatColor);
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
