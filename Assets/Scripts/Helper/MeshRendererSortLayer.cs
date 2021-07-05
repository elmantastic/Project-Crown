using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererSortLayer : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    void Start()
	{
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Player";
        meshRenderer.sortingOrder = 3;
    }
}
