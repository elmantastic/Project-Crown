using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTerrain : MonoBehaviour
{
    [SerializeField] private float moveDistances = 4.0f;
    [SerializeField] private float speed = 3.0f;
    public MachineCounter machineCounter;
    private bool isOpen;
    private float edge;
    // Start is called before the first frame update
    void Awake()
    {
        edge = transform.position.y + moveDistances;
    }

    // Update is called once per frame
    void Update()
    {
        if(machineCounter.GetMachineDestroyed() >= 5 && !isOpen){
            OpenDoor();
        }
    }

    private void OpenDoor(){
        if(transform.position.y < edge){
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        } else{
            isOpen = true;
        }
    }
}
