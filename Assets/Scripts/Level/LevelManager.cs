using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform respownPoint;
    public Transform canvasPosition;
    public GameObject playerPrefab;
    public GameObject countDownPrefab;
    public CinemachineVirtualCamera cam;
    public PointFollow followPoint;

    private void Awake() {
        instance = this;
    }

    public void PlayerDie(){
        ShowCountDown();
    }

    public void Respawn(){
        GameObject player = Instantiate(playerPrefab, respownPoint.position, Quaternion.identity);

        cam.Follow = player.transform;
        followPoint.unitGameObject = player.transform;
    }

    private void ShowCountDown(){
        GameObject countDown = Instantiate(countDownPrefab, canvasPosition.position, Quaternion.identity);

        countDown.transform.parent = canvasPosition;
    }
}
