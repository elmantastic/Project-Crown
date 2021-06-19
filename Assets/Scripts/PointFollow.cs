using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFollow : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    public Transform unitGameObject;

    Vector3 mousePosition = new Vector3();
    Vector3 followPointerPosition = new Vector3();

    private void Update() {
        
        // calculate mouse position
        mousePosition = myCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // calculate position for follow pointer
        followPointerPosition = (mousePosition + unitGameObject.position) / 2; // mid point calculation

        transform.position = followPointerPosition;
        
    }
}
