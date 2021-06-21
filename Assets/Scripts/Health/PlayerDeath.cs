using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void playerDeath(){
        Destroy(gameObject);
        LevelManager.instance.PlayerDie();
        //LevelManager.instance.Respawn();

        //Invoke("LevelManager.instance.Respawn()", 3);
    }
}
