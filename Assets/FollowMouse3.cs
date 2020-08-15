using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse3 : MonoBehaviour
{
    bool holded;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            holded = true;
        }
    }

    void Update()
    {
        if (holded)
        {
            transform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1);
            if (Input.GetMouseButtonUp(1))
                holded = false;
        }
    }
}
