using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player"){
            LevelManager.instance.CompleteLevel();

        }
    }
}
