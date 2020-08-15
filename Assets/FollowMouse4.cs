using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse4 : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        faceMouse();
    }

    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToViewportPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.x - transform.position.y);
        transform.up = direction;
    }

    
}
