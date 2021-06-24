using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingRight;
    private float leftEdge;
    private float rightEdge;
    
    private void Awake() {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update() {
        if(movingRight){
            if(transform.position.x < rightEdge){
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            } else {
                movingRight = false;
            }
        } else {
            if(transform.position.x > leftEdge){
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            } else {
                movingRight = true;
            }
        }
    }
}
