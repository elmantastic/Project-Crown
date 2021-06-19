using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    private BoxCollider2D col;

    private bool triggered;
    private float invisTime;

    private void Awake() {
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invisTime >= 0.2f){
            col.isTrigger = false;
        } else {
            invisTime += Time.deltaTime;
        }
    }

    public void SetTrigger(){
        col.isTrigger = true;
        invisTime = 0f;
    }


}
