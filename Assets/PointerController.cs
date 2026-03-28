using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public float lagValue;

    [Header("Mouse Values")]
    float mouseXVel;
    float mouseYVel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseXVel = Input.GetAxis("Mouse X");
        mouseYVel = Input.GetAxis("Mouse Y");

        transform.Translate(mouseXVel * lagValue, mouseYVel * lagValue, 0);

        if(lagValue <= 1 && lagValue >= 0.1)
            lagValue += (float)(Input.GetAxis("Mouse ScrollWheel"));
        if (lagValue < 0.1)
            lagValue = 0.1f;
        if (lagValue > 1)
            lagValue = 1;

        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.visible)
                Cursor.visible = false;
        }
    }
}
