using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform respownPoint;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera cam;
    public PointFollow followPoint;
    public HealthBar healthBar;

    private void Awake() {
        instance = this;
    }

    public void PlayerDie(){
        Invoke("Respawn", 3);
    }

    public void Respawn(){
        GameObject player = Instantiate(playerPrefab, respownPoint.position, Quaternion.identity);

        cam.Follow = player.transform;
        followPoint.unitGameObject = player.transform;
    }
}
